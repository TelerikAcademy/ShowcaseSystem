(function () {
    'use strict';

    var footerDirective = function footerDirective() {
        return {
            restrict: 'A',
            scope: false,
            templateUrl: '/app/common/footer-directive.html'
        };
    };
    
    angular
        .module('showcaseSystem.directives')
        .directive('showcaseFooter', [footerDirective]);
}());