(function () {
    'use strict';

    var statisticsProjectsDirective = function statisticsProjectsDirective(jQuery) {
        return {
            restrict: 'A',
            templateUrl: 'statistics-page/statistics-projects-directive.html',
            scope: {
                projects: '='
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('statisticsProjects', ['jQuery', statisticsProjectsDirective]);
}());