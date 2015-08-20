(function () {
    'use strict';

    var filters = function filters() {
        return {
            restrict: 'A',
            templateUrl: 'projects-search-page/filters-directive.html',
            scope: {
                options: '=',
                search: '&'
            },
            link: function (scope) {
                scope.scrollChecked = function () {
                    localStorage.scrolling = scope.options.scrolling;
                };

                scope.$watch('options', function (options, oldValue) {
                    $('.selectpicker').selectpicker({
                        size: 10
                    });
                });
            }
        };
    };

    angular.module('showcaseSystem.directives')
        .directive('filters', [filters]);
}());