(function () {
    'use strict';

    var statisticsProjectsDirective = function statisticsProjectsDirective(jQuery) {
        return {
            restrict: 'A',
            templateUrl: '/app/statistics-page/statistics-projects-directive.html',
            scope: {
                projects: '='
            },
            link: function (scope, element) {
                scope.$watch('projects', function (projects) {
                    if (projects) {
                        
                    }
                });
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('statisticsProjects', ['jQuery', statisticsProjectsDirective]);
}());