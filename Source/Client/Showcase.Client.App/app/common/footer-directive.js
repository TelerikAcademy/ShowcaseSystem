(function () {
    'use strict';

    var footerDirective = function footerDirective() {
        return {
            restrict: 'A',
            templateUrl: '/app/common/footer-directive.html'
        };
    };
    
    angular
        .module('showcaseSystem.directives')
        .directive('showcaseFooter', [footerDirective]);
}());