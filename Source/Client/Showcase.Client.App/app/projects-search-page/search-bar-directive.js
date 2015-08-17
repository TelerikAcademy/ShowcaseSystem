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
                var $searchButton = $('#search-button');

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

                element.find('.search-field').keypress(function (e) {
                    if (e.which == 10 || e.which == 13) {
                        $searchButton.click();
                    }
                });
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('searchBar', [searchBar]);
}());