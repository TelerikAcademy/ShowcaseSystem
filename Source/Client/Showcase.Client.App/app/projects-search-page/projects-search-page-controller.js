(function () {
    'use strict';

    var projectsSearchPageController = function projectsSearchPageController($scope, $routeParams, $location, $window, searchPageData, projectsSearchService, identity) {
        var vm = this,
            oDataQuery,
            canGetNext = true,
            CONSTS = {
                DESC: 'desc'
            };

        vm.projects = [];
        vm.isLastPage = false;
        vm.loading = false;
        vm.initialProjectsLoaded = false;

        vm.filterOptions = projectsSearchService.getFilterOptions();
        vm.filterOptions.desc = $routeParams.desc === undefined ? true : $routeParams.desc;
        vm.searchParams = projectsSearchService.getSearchParams();

        $scope.currentPage = 1;

        identity.getUser()
            .then(function (user) {
                vm.isAdmin = user.isAdmin;

                if (vm.isAdmin) {
                    vm.filterOptions.orderOptions.push({
                        value: 'flags',
                        name: 'Flags'
                    });
                }
            });

        vm.search = function (query) {
            $routeParams = {
                $orderby: vm.filterOptions.orderOption.value + (vm.filterOptions.desc ? ' ' + CONSTS.DESC : ''),
                $top: vm.filterOptions.pageSize,
                $skip: 0,
                $count: 'true',
                $filter: "createdOn ge " + projectsSearchService.getODataUTCDateFilter(vm.searchParams.fromDate) +
                    " and createdOn le " + projectsSearchService.getODataUTCDateFilter(vm.searchParams.toDate)
            };

            if (vm.searchParams.title || vm.searchParams.tags || vm.searchParams.collaborators || vm.searchParams.period) {
                $routeParams.$filter = (function getSeachParams() {
                    var args = [], index = 0;
                    if (vm.searchParams.title) {
                        args[index] = vm.searchParams.title
                            .split(',')
                            .map(function (title) {
                                return "contains(title,'" + title.trim() + "')";
                            })
                            .join(' or ');
                        index += 1;
                    }

                    if (vm.searchParams.tags) {
                        args[index] = vm.searchParams.tags
                            .split(',')
                            .map(function (tag) {
                                return "tags/any(t:contains(t/name,'" + tag.trim() + "'))";
                            }).join(' or ');
                        index += 1;
                    }

                    if (vm.searchParams.collaborators) {
                        args[index] = vm.searchParams.collaborators
                            .split(',')
                            .map(function (collaborator) {
                                return "collaborators/any(c:contains(c, '" + collaborator + "'))";
                            }).join(' or ');
                        index += 1;
                    }

                    return args.join(' and ');
                })();
            }
            
            if (query) {
                Object.keys(query)
                    .forEach(function (key) {
                        $routeParams[key] = query[key];
                    });
            }

            Object.keys($routeParams)
                .forEach(function (key) {
                    $location.search(key, $routeParams[key]);
                });

            vm.initialProjectsLoaded = false;
            getProjects();
        };

        if ($routeParams.tag) {
            vm.searchParams.tags = $routeParams.tag;
            $location.search('tag', null);
        }

        if ($routeParams.term) {
            vm.searchParams.title = $routeParams.term;
            $location.search('term', null);
        }

        vm.search();

        $scope.changePage = function (newPage) {
            $scope.currentPage = newPage;
            vm.search({ $skip: (newPage - 1) * vm.filterOptions.pageSize });
        };

        vm.getNextProjects = function () {
            if (vm.isLastPage || !canGetNext) {
                return;
            }

            canGetNext = false;
            $routeParams.$skip = ($scope.currentPage) * vm.filterOptions.pageSize;
            getProjects();
        };

        $scope.$watch('vm.filterOptions.scrolling', function (newValue, oldValue) {
            if (newValue === oldValue) {
                return;
            }

            if (newValue === true) {
                vm.initialProjectsLoaded = false;
                canGetNext = false;
                $scope.currentPage = 1;
                vm.search();
            } else {
                $scope.changePage($scope.currentPage);
            }
        });

        watchProperty('vm.filterOptions.desc');
        watchProperty('vm.filterOptions.orderOption');
        watchProperty('vm.filterOptions.pageSize');
        watchProperty('vm.filterOptions.includeHidden');
        watchProperty('vm.searchParams.fromDate');
        watchProperty('vm.filterOptions.toDate');
        
        function watchProperty(property) {
            $scope.$watch(property, function (newValue, oldValue) {
                if (newValue === oldValue) {
                    return;
                }
                
                $scope.currentPage = 1;
                vm.search();
            });
        }

        function getProjects() {
            oDataQuery = projectsSearchService.getQuery($routeParams, vm.filterOptions.includeHidden);
            var startTime = new Date().getTime();
            $routeParams.$count = true;             
            vm.loading = true;

            searchPageData.searchProjects(oDataQuery)
                .then(function (odata) {
                    // results data
                    $scope.currentPage = !!$routeParams.$skip ? ($routeParams.$skip / $routeParams.$top) + 1 : 1;
                    vm.isLastPage = odata['@odata.count'] <= $scope.currentPage * vm.filterOptions.pageSize;

                    // searchbar data
                    vm.totalResultsCount = odata['@odata.count'];
                    vm.timeElapsed = (new Date().getTime() - startTime) / 1000;

                    // pager data
                    $scope.totalPages = Math.ceil(odata['@odata.count'] / vm.filterOptions.pageSize);

                    if ($window.localStorage.scrolling == 'true' && vm.initialProjectsLoaded) {
                        vm.projects = vm.projects.concat(odata.value);
                    }
                    else {
                        vm.projects = odata.value;
                    }

                    vm.loading = false;
                    canGetNext = true;
                    vm.initialProjectsLoaded = true;
                });
        }
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('projectsSearchPageController', ['$scope', '$routeParams', '$location', '$window', 'projectsSearchPageData', 'projectsSearchService', 'identity', projectsSearchPageController]);
}());