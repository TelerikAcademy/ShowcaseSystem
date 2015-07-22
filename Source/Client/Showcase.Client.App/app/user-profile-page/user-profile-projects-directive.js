(function () {
    'use strict';

    var userProfileProjectsDirective = function userProfileProjectsDirective() {
        return {
            restrict: 'A',
            templateUrl: '/app/user-profile-page/user-profile-projects-directive.html',
            scope: {
                projects: '='
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('userProfileProjects', ['jQuery', userProfileProjectsDirective]);
}());