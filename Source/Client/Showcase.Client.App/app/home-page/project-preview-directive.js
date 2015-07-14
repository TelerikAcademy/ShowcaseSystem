(function() {
    'use strict';

    angular.module('showcaseSystem.directives')
        .directive('projectPreview', [projectPreview]);

    function projectPreview() {
        return {
            restrict: 'A',
            templateUrl: '/app/home-page/project-preview-directive.html',
            scope: {
                project: '=project'
            },
            link: function(scope, element) {
                element.addClass('isotope-item col-sm-6 col-md-3 development');
            }
        };
    }
}());
