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
        function getFilterOptions(isAdmin) {
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
                { value: 'title', name: 'Name' }
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
                title: '',
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
            else {
                monthString = rawMonth;
            }

            var dateString;
            var rawDate = date.getUTCDate().toString();
            if (rawDate.length == 1) {
                dateString = "0" + rawDate;
            }
            else {
                dateString = rawDate;
            }

            var hourString = '23';
            var minuteString = '59';
            var secondString = '59';

            var DateFilter = "";
            DateFilter += date.getUTCFullYear() + "-";
            DateFilter += monthString + "-";
            DateFilter += dateString;
            DateFilter += "T" + hourString + ":";
            DateFilter += minuteString + ":";
            DateFilter += secondString + "Z";
            return DateFilter;
        }

        function getQuery(params, onlyHidden) {
            if (!!params && !Object.keys(params).length) {
                params = CONSTS.DEFAULT_QUERY;
            }

            var result = 'Search?';

            if (onlyHidden) {
                result += 'onlyHidden=true&';
            }

            result += Object.keys(params)
                .map(function (key) {
                    return key + '=' + params[key];
                })
                .join('&');

            return result;
        }

        function getSearchFilter(searchParams) {
            var args = [], index = 0;
            if (searchParams.title) {
                args[index] = searchParams.title
                    .split(',')
                    .map(function (title) {
                        return "contains(title,'" + title.trim() + "')";
                    })
                    .join(' or ');
                index += 1;
            }

            if (searchParams.tags || searchParams.season || searchParams.languagesAndTechnologies) {
                if (searchParams.tags) {
                    args[index] = searchParams.tags
                        .split(',')
                        .map(function (tag) {
                            return "tags/any(t:t/id eq " + tag.trim() + ")";
                        }).join(' or ');
                }

                if (searchParams.season) {
                    var seasonQuery = "tags/any(t:t/id eq " + searchParams.season.id + ")";
                    if (args[index]) {
                        args[index] += ' and ' + seasonQuery;
                    }
                    else {
                        args[index] = seasonQuery;
                    }
                }

                if (searchParams.languagesAndTechnologies && searchParams.languagesAndTechnologies.length > 0) {
                    var languagesAndTechnologiesQuery = '(' + searchParams
                        .languagesAndTechnologies
                        .map(function (tag) {
                            return "tags/any(t:t/id eq " + tag + ")";
                        })
                        .join(' or ') + ')';

                    if (args[index]) {
                        args[index] += ' and ' + languagesAndTechnologiesQuery;
                    }
                    else {
                        args[index] = languagesAndTechnologiesQuery;
                    }
                }

                index += 1;
            }

            if (searchParams.collaborators) {
                args[index] = searchParams.collaborators
                    .split(',')
                    .map(function (collaborator) {
                        return "collaborators/any(c:c/userName eq '" + collaborator + "')";
                    }).join(' or ');
                index += 1;
            }

            return args.join(' and ');
        }

        function isValidDate(date) {
            if (date && !isNaN(date.getTime())) {
                return true;
            }

            return false;
        }

        return {
            getFilterOptions: getFilterOptions,
            getSearchParams: getSearchParams,
            getQuery: getQuery,
            getODataUTCDateFilter: getODataUTCDateFilter,
            getSearchFilter: getSearchFilter,
            isValidDate: isValidDate
        };
    };

    angular
        .module('showcaseSystem.services')
        .factory('projectsSearchService', ['$routeParams', '$location', projectsSearchService]);
}());