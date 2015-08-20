(function () {
    'use strict';

    var uploadedImagesContainerDirective = function uploadedImagesContainerDirective() {
        return {
            restrict: 'A',
            templateUrl: 'add-project-page/uploaded-images-container-directive.html',
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
                    scope.$apply(function () {
                        scope.mainImage = imageName;
                    });
                });

                scope.$watch('images', function (images) {
                    if (images && images.length == 1) {
                        scope.mainImage = images[0].originalFileName;
                    }
                });
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('uploadedImagesContainer', [uploadedImagesContainerDirective]);
}());