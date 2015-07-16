(function () {
    'use strict';

    angular
        .module('showcaseSystem.directives')
        .directive('showcaseFooter', [footerDirective]);

    function footerDirective() {
        return {
            restrict: 'A',
            templateUrl: '/app/common/footer-directive.html'
        };
    }
}());