(function () {
    'use strict';

    var filters = function filters() {
        return {
            restrict: 'A',
            templateUrl: '/app/search-page/filters-directive.html',
            scope: {
                filteroptions: '=',
                search: '&'
            }
        };
    };

    angular.module('showcaseSystem.directives')
        .directive('filters', [filters]);
}());