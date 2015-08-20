(function () {
    'use strict';

    var footerDirective = function footerDirective() {
        return {
            restrict: 'A',
            scope: false,
            templateUrl: 'common/footer-directive.html'
        };
    };
    
    angular
        .module('showcaseSystem.directives')
        .directive('showcaseFooter', [footerDirective]);
}());