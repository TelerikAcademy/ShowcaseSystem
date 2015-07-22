(function () {
    'use strict';

    var filters = function filters() {
        return {
            restrict: 'A',
            templateUrl: '/app/search-page/filters-directive.html',
            scope: {
                options: '=',
                search: '&'
            },
            link: function (scope) {
                scope.scrollChecked = function () {
                    localStorage.scrolling = scope.options.scrolling;
                }
            }
        };
    };

    angular.module('showcaseSystem.directives')
        .directive('filters', [filters]);
}());