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
                fromDate: new Date(2015, 4, 1),
                toDate: new Date()
            };
        }

        function getODataUTCDateFilter(date) {
            var monthString;
            var rawMonth = (date.getUTCMonth() + 1).toString();
            if (rawMonth.length == 1) {
                monthString = "0" + rawMonth;
            }
            else { monthString = rawMonth; }

            var dateString;
            var rawDate = date.getUTCDate().toString();
            if (rawDate.length == 1) {
                dateString = "0" + rawDate;
            }
            else { dateString = rawDate; }

            var hourString = '23';
            var minuteString = '59';
            var secondString = '59';

            //var hourString = date.getUTCHours().toString();
            //if (hourString.length == 1)
            //    hourString = "0" + hourString;

            //var minuteString = date.getUTCMinutes().toString();
            //if (minuteString.length == 1)
            //    minuteString = "0" + minuteString;

            //var secondString = date.getUTCSeconds().toString();
            //if (secondString.length == 1)
            //    secondString = "0" + secondString;

            var DateFilter = "";
            DateFilter += date.getUTCFullYear() + "-";
            DateFilter += monthString + "-";
            DateFilter += dateString;
            DateFilter += "T" + hourString + ":";
            DateFilter += minuteString + ":";
            DateFilter += secondString + "Z";
            return DateFilter;
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
            getODataUTCDateFilter: getODataUTCDateFilter
        };
    };

    angular
        .module('showcaseSystem.services')
        .factory('projectsSearchService', ['$routeParams', '$location', projectsSearchService]);
}());