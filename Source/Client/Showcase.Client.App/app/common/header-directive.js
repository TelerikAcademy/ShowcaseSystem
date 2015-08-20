(function () {
    'use strict';

    var headerDirective = function headerDirective() {
        return {
            restrict: 'A',
            scope: false,
            templateUrl: 'common/header-directive.html'
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('showcaseHeader', [headerDirective]);
}());