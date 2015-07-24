(function () {
    'use strict';

    var projectsSearchPageController = function projectsSearchPageController($scope, $routeParams, $location, searchPageData) {
        var vm = this,
            oDataQuery,
            CONSTS = {
                DESC: 'desc'
            };

        vm.filterOptions = searchPageData.getFilterOptions();
        vm.searchParams = searchPageData.getSearchParams();

        vm.search = function (query) {
            $routeParams = {
                $orderby: vm.filterOptions.orderOption.value + (vm.filterOptions.desc ? ' ' + CONSTS.DESC : ''),
                $top: vm.filterOptions.pageSize,
                $skip: $scope.currentpage || 0,
                $count: 'true'
            };

            if (vm.searchParams.name || vm.searchParams.tags || vm.searchParams.collaborators || vm.searchParams.period) {
                $routeParams.$filter = (function getSeachParams() {
                    var args = [], index = 0;
                    if (vm.searchParams.name) {
                        args[index] = vm.searchParams.name
                            .split(',')
                            .map(function (name) {
                                return "contains(Name,'" + name.trim() + "')";
                            })
                            .join(' or ');
                        index += 1;
                    }
                    if (vm.searchParams.tags) {
                        args[index] = vm.searchParams.tags
                            .split(',')
                            .map(function (tag) {
                                return "Tags/any(t:contains(t/Name,'" + tag.trim() + "'))";
                            }).join(' or ');
                        index += 1;
                    }
                    if (vm.searchParams.collaborators) {
                        args[index] = vm.searchParams.collaborators
                            .split(',')
                            .map(function (collaborator) {
                                return "Collaborators/any(c:contains(c, '" + collaborator + "'))";
                            }).join(' or ');
                        index += 1;
                    }
                    if (vm.searchParams.period) {
                        args[index] = '';// TODO:
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
        };

        // not working if attached to vm
        $scope.changePage = function (newPage) {
            $scope.currentPage = newPage;
            vm.search({ $skip: newPage * vm.filterOptions.pageSize });
        };

        vm.getNextProjects = function () {
            console.log(vm.filterOptions);
        };

        oDataQuery = searchPageData.getQuery($routeParams);
        var stertTime = new Date().getTime();
        searchPageData.searchProjects(oDataQuery)
            .then(function (odata) {
                // results data
                vm.projects = odata.value;
                // searchbar data
                vm.totalResultsCount = odata['@odata.count'];
                vm.timeElapsed = (new Date().getTime() - stertTime) / 1000;
                // pager data
                $scope.totalPages = Math.ceil(odata['@odata.count'] / vm.filterOptions.pageSize);
                $scope.currentPage = !!$routeParams.$skip ? $routeParams.$skip / $routeParams.$top : 0;
            });
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('projectsSearchPageController', ['$scope', '$routeParams', '$location', 'projectsSearchPageData', projectsSearchPageController]);
}());