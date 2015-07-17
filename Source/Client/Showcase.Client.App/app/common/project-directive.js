(function() {
    'use strict';

    var project = function project() {
        return {
            restrict: 'A',
            templateUrl: '/app/common/project-directive.html',
            scope: {
                project: '='
            }
        };
    };

    angular.module('showcaseSystem.directives')
        .directive('project', [project]);
}());