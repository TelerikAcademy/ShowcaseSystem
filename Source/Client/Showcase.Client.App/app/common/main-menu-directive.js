(function () {
    'use strict';

    var mainMenuDirective = function mainMenuDirective() {
        return {
            restrict: 'A',
            templateUrl: '/app/common/main-menu-directive.html',
            link: function (scope, element) {
                
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('mainMenu', [mainMenuDirective]);
}());