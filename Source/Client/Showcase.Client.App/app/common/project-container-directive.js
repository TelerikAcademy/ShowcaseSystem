(function() {
    'use strict';

    var projectContainer = function projectContainer() {
        return {
            restrict: 'A',
            templateUrl: '/app/common/project-container-directive.html',
            scope: {
                projects: '='
            }
        };
    };

    angular.module('showcaseSystem.directives')
        .directive('projectContainer', [projectContainer]);
}());
