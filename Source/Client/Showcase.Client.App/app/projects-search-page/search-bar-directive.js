(function () {
    'use strict';

    var searchBar = function searchBar() {
        return {
            restrict: 'A',
            templateUrl: 'projects-search-page/search-bar-directive.html',
            scope: {
                params: '=',
                seasons: '=',
                technologies: '=',
                languages: '='
            },
            link: function (scope, element) {
                var $searchButton = $('#search-button');
                
                scope.$watchGroup(['technologies', 'languages', 'seasons'], function (newValues) {
                    if (newValues[0] && newValues[0].length && newValues[0].length > 0 &&
                            newValues[1] && newValues[1].length && newValues[1].length > 0 &&
                            newValues[2] && newValues[2].length && newValues[2].length > 0) {

                        $('.selectpicker').selectpicker({
                            size: 10
                        });

                        $('.selectpicker').selectpicker('refresh');
                    }
                });
                
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