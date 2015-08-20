(function () {
    'use strict';

    var projectTagsDirective = function projectTagsDirective() {
        return {
            restrict: 'A',
            templateUrl: 'project-details-page/project-tags-directive.html',
            scope: {
                tags: '='
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('projectTags', [projectTagsDirective]);
}());