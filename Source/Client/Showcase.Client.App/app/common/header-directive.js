(function () {
    'use strict';

    var headerDirective = function headerDirective() {
        return {
            restrict: 'A',
            templateUrl: '/app/common/header-directive.html'
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('showcaseHeader', [headerDirective]);
}());