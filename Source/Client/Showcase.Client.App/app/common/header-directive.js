(function () {
    'use strict';

    angular
        .module('showcaseSystem.directives')
        .directive('showcaseHeader', [headerDirective]);

    function headerDirective() {
        return {
            restrict: 'A',
            templateUrl: '/app/common/header-directive.html'
        }
    }
}());