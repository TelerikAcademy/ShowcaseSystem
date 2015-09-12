(function () {
    'use strict';

    var projectDescriptionDirective = function projectDescriptionDirective() {
        return {
            restrict: 'A',
            templateUrl: '/app/project-details-page/project-description-directive.html',
            scope: {
                description: '='
            },
            link: function (scope, element) {
                updateDescription();

                scope.$watch('description', function (newValue, oldValue) {
                    if (newValue === oldValue) {
                        return;
                    }

                    updateDescription();
                });

                function updateDescription() {
                    scope.descriptionParts = scope.description.match(/[^\r\n]+/g);
                }
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('projectDescription', [projectDescriptionDirective]);
}());