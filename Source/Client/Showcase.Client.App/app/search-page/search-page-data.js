(function () {
    'use strict';

    var searchPageData = function searchPageData(data, $routeParams) {
        var CONSTS = {
            DESC: 'desc'
        };

        function searchProjects(oData) {
            oData = oData || '/search';
            return data.get('odata' + oData);
        }

        function getFilterOptions() {
            var filterOptions = {
                options: [
                    { value: 'CreatedOn', name: 'Date' },
                    { value: 'Visits', name: 'Views' },
                    { value: 'Likes', name: 'Likes' },
                    { value: 'Comments', name: 'Comments' },
                    { value: 'Name', name: 'Name' }
                ],
                pageSizes: [4, 8, 16, 32, 64],
                page: $routeParams.$page || 0,
            };

            filterOptions.pageSize = $routeParams.$top || filterOptions.pageSizes[1];
            filterOptions.desc = (function getDesc() {
                var desc = $routeParams.$orderby && $routeParams.$orderby.indexOf(CONSTS.DESC) > 0;
                return desc || false;
            })();
            filterOptions.orderby = (function getOrderBy() {
                var orderBy = (filterOptions.options.filter(function (item) {
                    if (filterOptions.desc) {
                        return item.value + ' ' + CONSTS.DESC === $routeParams.$orderby;
                    } else {
                        return item.value === $routeParams.$orderby;
                    }
                })[0]);
                return orderBy || filterOptions.options[0];
            })();

            return filterOptions;
        }

        return {
            searchProjects: searchProjects,
            getFilterOptions: getFilterOptions
        };
    };

    angular
        .module('showcaseSystem.data')
        .factory('searchPageData', ['data', '$routeParams', searchPageData]);
}());