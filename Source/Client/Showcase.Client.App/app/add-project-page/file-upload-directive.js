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
                        notifier.error('Please, select valid files')
                        return;
                    }

                    var self = this;
                    var element = e.target;

                    $q.all(slice.call(element.files, 0).map(readFile))
                        .then(function (values) {
                            self.parentNode.nextSibling.value = self.files.length + ' selected files: ' + values.map(function (el) { return el.originalName }).join(', ');

                            if (element.multiple) ngModel.$setViewValue(values);
                            else ngModel.$setViewValue(values.length ? values[0] : null);
                        });

                    function readFile(file) {
                        var deferred = $q.defer();

                        var reader = new FileReader();
                        reader.onload = function (e) {
                            var convertedFile = e.target.result;
                            var result = {
                                originalName: file.name.substr(0, file.name.lastIndexOf('.')),
                                fileExtension: file.name.substr(file.name.lastIndexOf('.') + 1),
                                base64Content:  convertedFile.substr(convertedFile.indexOf(',') + 1)
                            };

                            deferred.resolve(result);
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