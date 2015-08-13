(function () {
    'use strict';

    var dropdown = function dropdown() {
        return {
            restrict: 'A',
            templateUrl: '/app/common/dropdown-directive.html',
            scope: {
                options: '=',
                selected: '='
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('dropdown', [dropdown]);
}());