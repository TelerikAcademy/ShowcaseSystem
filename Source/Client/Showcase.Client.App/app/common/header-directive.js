(function () {
    'use strict';

    angular
        .module('showcaseSystem')
        .directive('showcaseHeader', headerDirective);

    function headerDirective() {
        return {
            restrict: 'A',
            templateUrl: '/app/common/header-directive.html'
        }
    }
}());