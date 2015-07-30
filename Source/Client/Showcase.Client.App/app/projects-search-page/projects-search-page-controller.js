(function () {
    'use strict';

    var projectsSearchPageController = function projectsSearchPageController($scope, $routeParams, $location, $window, searchPageData, projectsSearchService) {
        var vm = this,
            oDataQuery,
            canGetNext = true,
            initialProjectsLoaded = false,
            CONSTS = {
                DESC: 'desc'
            };

        vm.projects = [];
        vm.isLastPage = false;
        vm.loading = false;

        vm.filterOptions = projectsSearchService.getFilterOptions();
        vm.filterOptions.desc = $routeParams.desc === undefined ? true : $routeParams.desc;
        vm.searchParams = projectsSearchService.getSearchParams();

        $scope.currentPage = 1;

        vm.search = function (query) {
            $routeParams = {
                $orderby: vm.filterOptions.orderOption.value + (vm.filterOptions.desc ? ' ' + CONSTS.DESC : ''),
                $top: vm.filterOptions.pageSize,
                $skip: 0,
                $count: 'true',
                $filter: "createdOn ge " +
                    projectsSearchService.getODataUTCDateFilter(vm.searchParams.fromDate) +
                    " and createdOn le " +
                    projectsSearchService.getODataUTCDateFilter(vm.searchParams.toDate)
            };

            if (vm.searchParams.name || vm.searchParams.tags || vm.searchParams.collaborators || vm.searchParams.period) {
                $routeParams.$filter = (function getSeachParams() {
                    var args = [], index = 0;
                    if (vm.searchParams.name) {
                        args[index] = vm.searchParams.name
                            .split(',')
                            .map(function (name) {
                                return "contains(name,'" + name.trim() + "')";
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

            initialProjectsLoaded = false;
            getProjects();
        };

        vm.search();

        // not working if attached to vm
        $scope.changePage = function (newPage) {
            $scope.currentPage = newPage;
            vm.search({ $skip: (newPage - 1) * vm.filterOptions.pageSize });
        };

        vm.getNextProjects = function () {
            if (vm.isLastPage || !canGetNext) {
                return;
            }

            canGetNext = false;
            $routeParams.$skip = ($scope.currentPage - 1) * vm.filterOptions.pageSize;
            getProjects();
        };

        $scope.$watch('vm.filterOptions.desc', function (newValue, oldValue) {
            if (newValue === oldValue) {
                return;
            }

            vm.search();
        });

        $scope.$watch('vm.filterOptions.orderOption', function (newValue, oldValue) {
            if (newValue === oldValue) {
                return;
            }

            vm.search();
        });

        $scope.$watch('vm.filterOptions.pageSize', function (newValue, oldValue) {
            if (newValue === oldValue) {
                return;
            }

            vm.search();
        });

        $scope.$watch('vm.searchParams.fromDate', function (newValue, oldValue) {
            if (newValue === oldValue) {
                return;
            }

            vm.search();
        });

        $scope.$watch('vm.searchParams.toDate', function (newValue, oldValue) {
            if (newValue === oldValue) {
                return;
            }

            vm.search();
        });

        $scope.$watch('vm.filterOptions.scrolling', function (newValue, oldValue) {
            if (newValue === oldValue) {
                return;
            }

            if (newValue == true) {
                initialProjectsLoaded = false;
                canGetNext = false;
                $scope.currentPage = 1;
                vm.search();
            } else {
                $scope.currentPage--;
                $scope.changePage($scope.currentPage);
            }
        });

        function getProjects() {
            oDataQuery = projectsSearchService.getQuery($routeParams);
            var startTime = new Date().getTime();
            $routeParams.$count = true;

            if (!initialProjectsLoaded) {                
                vm.loading = true;
            }

            searchPageData.searchProjects(oDataQuery)
                .then(function (odata) {
                    // results data
                    vm.isLastPage = odata['@odata.count'] <= ($scope.currentPage) * vm.filterOptions.pageSize;

                    // searchbar data
                    vm.totalResultsCount = odata['@odata.count'];
                    vm.timeElapsed = (new Date().getTime() - startTime) / 1000;

                    // pager data
                    $scope.totalPages = Math.ceil(odata['@odata.count'] / vm.filterOptions.pageSize);

                    if (localStorage.scrolling == 'true' && initialProjectsLoaded) {
                        vm.projects = vm.projects.concat(odata.value);
                    }
                    else {
                        vm.projects = odata.value;
                    }

                    vm.loading = false;
                    
                    if (localStorage.scrolling == 'true') {
                        $scope.currentPage++;
                    }
                    else {
                        $scope.currentPage = !!$routeParams.$skip ? ($routeParams.$skip / $routeParams.$top) + 1 : 1;
                    }

                    canGetNext = true;
                    initialProjectsLoaded = true;
                });
        }
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('projectsSearchPageController', ['$scope', '$routeParams', '$location', '$window', 'projectsSearchPageData', 'projectsSearchService', projectsSearchPageController]);
}());