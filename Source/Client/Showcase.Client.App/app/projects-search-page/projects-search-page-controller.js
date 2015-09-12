(function () {
    'use strict';

    var projectsSearchPageController = function projectsSearchPageController($scope, $routeParams, $location, $window, searchPageData, projectsSearchService, identity, notifier) {
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

        if ($window.localStorage.scrolling === undefined) {
            $window.localStorage.scrolling = 'true';
            vm.filterOptions.scrolling = true;
        }

        if ($routeParams.tag) {
            vm.searchParams.tags = $routeParams.tag.toString();
            $location.search('tag', null);
        }

        if ($routeParams.term) {
            vm.searchParams.title = $routeParams.term;
            $location.search('term', null);
        }

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

        searchPageData.getSeasons()
            .then(function (seasons) {
                vm.seasons = seasons;
            });

        searchPageData.getTechnologies()
            .then(function (technologies) {
                vm.technologies = technologies;
            });

        searchPageData.getLanguages()
            .then(function (languages) {
                vm.languages = languages;
            });

        vm.getNextProjects = function () {
            if (vm.isLastPage || !canGetNext) {
                return;
            }

            canGetNext = false;
            $routeParams.$skip = ($scope.currentPage) * vm.filterOptions.pageSize;
            getProjects();
        };
        
        vm.search = function (query) {
            $routeParams = {
                $orderby: vm.filterOptions.orderOption.value + (vm.filterOptions.desc ? ' ' + CONSTS.DESC : ''),
                $top: vm.filterOptions.pageSize,
                $skip: 0,
                $count: 'true',
                $filter: "createdOn ge " + projectsSearchService.getODataUTCDateFilter(projectsSearchService.isValidDate(vm.searchParams.fromDate) ? vm.searchParams.fromDate : projectsSearchService.getSearchParams().fromDate) +
                    " and createdOn le " + projectsSearchService.getODataUTCDateFilter(projectsSearchService.isValidDate(vm.searchParams.toDate) ? vm.searchParams.toDate : new Date())
            };

            // TODO: get these from service
            if (vm.searchParams.title || vm.searchParams.tags || vm.searchParams.collaborators || vm.searchParams.period || vm.searchParams.season || (vm.searchParams.languagesAndTechnologies && vm.searchParams.languagesAndTechnologies.length > 0)) {
                if (vm.searchParams.languagesAndTechnologies && vm.searchParams.languagesAndTechnologies.length > 10) {
                    notifier.error('You can filter by no more than 10 Languages and Technologies.');
                    return;
                }
                
                $routeParams.$filter = projectsSearchService.getSearchFilter(vm.searchParams);
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

        $scope.changePage = function (newPage) {
            $scope.currentPage = newPage;
            vm.search({ $skip: (newPage - 1) * vm.filterOptions.pageSize });
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

        // TODO: remove watches
        watchProperty('vm.filterOptions.desc');
        watchProperty('vm.filterOptions.orderOption');
        watchProperty('vm.filterOptions.pageSize');
        watchProperty('vm.filterOptions.onlyHidden');
        watchProperty('vm.searchParams.title');
        watchProperty('vm.searchParams.fromDate');
        watchProperty('vm.searchParams.toDate');
        watchProperty('vm.searchParams.season');
        watchProperty('vm.searchParams.languagesAndTechnologies');
        watchProperty('vm.searchParams.tags');
        watchProperty('vm.searchParams.collaborators');
        
        if ($routeParams.$orderby) {
            getProjects();
        }
        else {
            vm.search();
        }

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
            oDataQuery = projectsSearchService.getQuery($routeParams, vm.filterOptions.onlyHidden);
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
        .controller('ProjectsSearchPageController', ['$scope', '$routeParams', '$location', '$window', 'projectsSearchPageData', 'projectsSearchService', 'identity', 'notifier', projectsSearchPageController]);
}());