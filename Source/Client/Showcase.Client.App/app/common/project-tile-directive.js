(function () {
    'use strict';

    var projectTile = function projectTile() {
        return {
            restrict: 'A',
            templateUrl: '/app/common/project-tile-directive.html',
            require: '^projectContainer',
            scope: {
                project: '='
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('projectTile', [projectTile]);
}());