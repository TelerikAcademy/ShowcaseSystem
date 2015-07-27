(function () {
    'use strict';

    var uploadedImagesContainerDirective = function uploadedImagesContainerDirective() {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs, ngModel) {
                attrs.$observe('ngModel', function (value) {
                    scope.$watch(value, function (newValue) {
                        console.log(newValue);
                    });
                });
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('uploadedImagesContainer', [uploadedImagesContainerDirective]);
}());