(function () {
    'use strict';

    var searchBar = function searchBar() {
        return {
            restrict: 'A',
            templateUrl: '/app/projects-search-page/search-bar-directive.html',
            scope: {
                params: '='
            },
            link: function (scope, element) {
                scope.fromDateIsOpen = false;

                scope.openFrom = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();

                    scope.fromDateIsOpen = true;
                };

                scope.toDateIsOpen = false;

                scope.openTo = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();

                    scope.toDateIsOpen = true;
                };
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('searchBar', [searchBar]);

}());