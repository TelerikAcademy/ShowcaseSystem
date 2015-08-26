(function () {
    'use strict';

    var footerDirective = function footerDirective(appSettings) {
        return {
            restrict: 'A',
            templateUrl: 'common/footer-directive.html',
            link: function (scope, element) {
                scope.version = appSettings.version;
            }
        };
    };
    
    angular
        .module('showcaseSystem.directives')
        .directive('showcaseFooter', ['appSettings', footerDirective]);
}());