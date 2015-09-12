(function () {
    'use strict';

    var projectTagsDirective = function projectTagsDirective() {
        return {
            restrict: 'A',
            templateUrl: 'project-details-page/project-tags-directive.html',
            scope: {
                tags: '=',
                edit: '=',
                deletedTags: '='
            },
            link: function (scope, element) {
                scope.deleteTag = function (tag) {
                    var indexOfTag = scope.tags.indexOf(tag);
                    scope.tags.splice(indexOfTag, 1);
                    scope.deletedTags = scope.deletedTags || [];
                    scope.deletedTags.push(tag.name);
                };
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('projectTags', [projectTagsDirective]);
}());