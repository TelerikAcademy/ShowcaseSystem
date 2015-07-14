(function() {
    'use strict';

    angular.module('showcaseSystem.directives')
        .directive('projectContainer', [projectContainer]);

    function projectContainer() {
        return {
            restrict: 'A',
            templateUrl: '/app/common/project-container-directive.html',
            scope: {
                projects: '='
            }
        };
    }
}());
