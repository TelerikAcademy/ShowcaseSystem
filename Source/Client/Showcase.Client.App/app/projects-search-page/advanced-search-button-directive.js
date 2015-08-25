(function () {
    'use strict';

    var advancedSearchButtonDirective = function advancedSearchButtonDirective() {
        return {
            restrict: 'A',
            templateUrl: '/app/projects-search-page/advanced-search-button-directive.html',
            link: function (scope, element) {
                scope.advancedSearch = false;

                scope.showAdvancedSearchOptions = function () {
                    if (scope.advancedSearch) {
                        $('.advanced-search-options').removeClass('hidden-xs');
                    } else {
                        $('.advanced-search-options').addClass('hidden-xs');
                    }
                };
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('advancedSearchButton', [advancedSearchButtonDirective]);
}());