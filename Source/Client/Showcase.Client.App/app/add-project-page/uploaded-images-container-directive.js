(function () {
    'use strict';

    var uploadedImagesContainerDirective = function uploadedImagesContainerDirective() {
        return {
            restrict: 'A',
            templateUrl: '/app/add-project-page/uploaded-images-container-directive.html',
            scope: {
                images: '=',
                mainImage: '='
            },
            link: function (scope, element, attrs) {
                element.on('click', '.preview-image', function () {
                    var $this = $(this);
                    $this.parent().parent().find('.image-selected').removeClass('image-selected');
                    $this.addClass('image-selected');
                    var imageName = $this.data('image-name');
                    scope.mainImage = imageName;
                });
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('uploadedImagesContainer', [uploadedImagesContainerDirective]);
}());