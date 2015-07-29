(function () {
    'use strict';

    var CONSTS = {
        DESC: 'desc',
        DEFAULT_QUERY: {
            $orderby: 'createdOn',
            $top: 8,
            $count: 'true'
        }
    };

    var projectsSearchService = function projectsSearchService($routeParams, $location) {
        function prepareSearchParams(query, vm, $scope, $routeParams) {
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

            $scope.currentPage = 0;
        }

        function getFilterOptions() {
            var findOrderOption = function findOrderOption(value) {
                return options.orderOptions.filter(function (option) {
                    if (options.desc) {
                        return option.value + ' ' + CONSTS.DESC === value;
                    } else {
                        return option.value === value;
                    }
                })[0];
            };

            var options = {};
            options.orderOptions = [
                { value: 'createdOn', name: 'Date' },
                { value: 'visits', name: 'Views' },
                { value: 'likes', name: 'Likes' },
                { value: 'comments', name: 'Comments' },
                { value: 'name', name: 'Name' }
            ];

            options.pageSizes = [8, 16, 32, 64];
            options.scrolling = localStorage.scrolling === 'true';
            options.pageSize = +$routeParams.$top || CONSTS.DEFAULT_QUERY.$top;
            options.desc = !!$routeParams.$orderby && $routeParams.$orderby.indexOf(CONSTS.DESC) > 0;
            options.orderOption = findOrderOption($routeParams.$orderby) || findOrderOption(CONSTS.DEFAULT_QUERY.$orderby);

            return options;
        }

        function getSearchParams() {
            return {
                name: '',
                tags: '',
                collaborators: '',
                period: ''
            };
        }

        function getQuery(params) {
            if (!!params && !Object.keys(params).length) {
                params = CONSTS.DEFAULT_QUERY;
            }

            var result = 'Search?' + Object.keys(params)
                .map(function (key) {
                    return key + '=' + params[key];
                })
                .join('&');

            return result;
        }

        return {
            getFilterOptions: getFilterOptions,
            getSearchParams: getSearchParams,
            getQuery: getQuery,
            prepareSearchParams: prepareSearchParams
        };
    };

    angular
        .module('showcaseSystem.services')
        .factory('projectsSearchService', ['$routeParams', '$location', projectsSearchService]);
}());