(function() {
    'use strict';

    var projectContainerDirective = function projectContainerDirective() {
        return {
            restrict: 'A',
            templateUrl: 'common/project-container-directive.html',
            scope: {
                projects: '='
            }
        };
    };

    angular.module('showcaseSystem.directives')
        .directive('projectContainer', [projectContainerDirective]);
}());
