(function () {
    'use strict';

    var fileUploadDirective = function fileUploadDirective($q, jQuery, notifier) {
        var slice = Array.prototype.slice;
        var validationRegex;

        return {
            restrict: 'A',
            require: '?ngModel',
            link: function (scope, element, attrs, ngModel) {
                if (!ngModel) return;
                if (attrs.validationRegex) {
                    validationRegex = new RegExp(attrs.validationRegex, 'g');
                }

                ngModel.$render = function () { };

                element.bind('change', function (e) {
                    if (validationRegex && !validationRegex.test(jQuery(this).val().toLowerCase())) {
                        notifier.error('Please, select valid image files')
                        return;
                    }

                    this.parentNode.nextSibling.value = this.files.length + ' selected images'

                    var element = e.target;

                    $q.all(slice.call(element.files, 0).map(readFile))
                        .then(function (values) {
                            if (element.multiple) ngModel.$setViewValue(values);
                            else ngModel.$setViewValue(values.length ? values[0] : null);
                        });

                    function readFile(file) {
                        var deferred = $q.defer();

                        var reader = new FileReader();
                        reader.onload = function (e) {
                            deferred.resolve(e.target.result);
                        };
                        reader.onerror = function (e) {
                            deferred.reject(e);
                        };
                        reader.readAsDataURL(file);

                        return deferred.promise;
                    }
                });

            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('fileUpload', ['$q', 'jQuery', 'notifier', fileUploadDirective]);
}());