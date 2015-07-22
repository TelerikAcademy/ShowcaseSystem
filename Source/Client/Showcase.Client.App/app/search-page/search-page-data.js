(function () {
    'use strict';

    var searchPageData = function searchPageData(data, $routeParams) {
        var CONSTS = {
            DESC: 'desc',
            DEFAULT_QUERY: {
                $orderby: 'CreatedOn',
                $top: 4,
                $count: 'true'
            }
        };

        function searchProjects(oData) {
            oData = oData || '/search';
            return data.get('projects' + oData);
        }

        function getSearchTerms() {
            return {
                options: [
                    { value: 'Title and Content', name: 'Title and Content' },
                    { value: 'Collaborator', name: 'Collaborator' },
                    { value: 'Comment', name: 'Comment' },
                    { value: 'Tag', name: 'Tag' }
                ]
            };
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
                    { value: 'CreatedOn', name: 'Date' },
                    { value: 'Visits', name: 'Views' },
                    { value: 'Likes', name: 'Likes' },
                    { value: 'Comments', name: 'Comments' },
                    { value: 'Name', name: 'Name' }
            ];
            options.pageSizes = [4, 8, 16, 32, 64];
            options.scrolling = localStorage.scrolling === 'true';
            options.pageSize = +$routeParams.$top || CONSTS.DEFAULT_QUERY.$top;
            options.desc = !!$routeParams.$orderby && $routeParams.$orderby.indexOf(CONSTS.DESC) > 0;
            options.orderOption = findOrderOption($routeParams.$orderby) || findOrderOption(CONSTS.DEFAULT_QUERY.$orderby);

            return options;
        }

        function getQuery(query) {
            if (!!query && !Object.keys(query).length) {
                query = CONSTS.DEFAULT_QUERY;
            }

            var result = '/search?' + Object.keys(query)
                .map(function (key) {
                    return key + '=' + query[key];
                })
                .join('&');

            return result;
        }

        return {
            searchProjects: searchProjects,
            getFilterOptions: getFilterOptions,
            getSearchTerms: getSearchTerms,
            getQuery: getQuery
        };
    };

    angular
        .module('showcaseSystem.data')
        .factory('searchPageData', ['data', '$routeParams', searchPageData]);
}());