(function () {
    'use strict';

    var fileUploadDirective = function fileUploadDirective($q, jQuery, notifier) {
        var slice = Array.prototype.slice;

        return {
            restrict: 'A',
            require: '?ngModel',
            link: function (scope, element, attrs, ngModel) {
                if (!ngModel) return;
                var validationRegex;
                var validationRegexErrorMessage;
                if (attrs.validationRegex) {
                    validationRegex = new RegExp(attrs.validationRegex);
                    validationRegexErrorMessage = attrs.regexErrorMessage;
                }

                if (attrs.initialText) {
                    element[0].parentNode.nextSibling.value = attrs.initialText;
                }

                var minFiles = attrs.minFiles;
                var maxFiles = attrs.maxFiles;
                var minFileSize = attrs.minFileSize;
                var maxFileSize = attrs.maxFileSize;
                var loadingBar = attrs.loadingBar;
                if (loadingBar) {
                    var loadingBarElement = angular.element('#' + loadingBar);
                }

                ngModel.$render = function () { };

                element.bind('change', function (e) {
                    var element = e.target;
                    if (!element.files || element.files.length === 0) {
                        return;
                    }
                    else if (element.files && minFiles && element.files.length < minFiles) {
                        notifier.error('You must select at least ' + minFiles + ' files');
                        return;
                    }
                    else if (element.files && maxFiles && element.files.length > maxFiles) {
                        notifier.error('You must select maximum ' + maxFiles + ' files');
                        return;
                    }
                    else if (element.files && element.files.length > 0) {
                        for (var i = 0; i < element.files.length; i++) {
                            var currentFile = element.files[i];
                            if (validationRegex && !validationRegex.test(currentFile.name.toLowerCase())) {
                                var errorMessage = 'You must select valid files.';
                                if (validationRegexErrorMessage) {
                                    errorMessage = validationRegexErrorMessage;
                                }

                                var regexString = attrs.validationRegex.substr(attrs.validationRegex.lastIndexOf('(') + 1);
                                var allowedExtensions = regexString.substring(0, regexString.length - 2).split('|').join(', ');

                                notifier.error(errorMessage + ' Allowed file extensions: ' + allowedExtensions);
                                return;
                            }

                            if (minFileSize && currentFile.size < minFileSize) {
                                notifier.error('Each file must be with size bigger than ' + minFileSize / 1024 + 'KB');
                                return;
                            }

                            if (maxFileSize && currentFile.size > maxFileSize) {
                                notifier.error('Each file must be with size smaller than ' + maxFileSize / 1024 / 1024 + 'MB');
                                return;
                            }
                        }
                    }

                    if (loadingBar) {
                        loadingBarElement.show();
                    }

                    ngModel.$setViewValue(undefined);

                    var self = this;
                    $q.all(slice.call(element.files, 0).map(readFile))
                        .then(function (values) {
                            self.parentNode.nextSibling.value = self.files.length + ' selected ' + (self.files.length == 1 ? 'file' : 'files') + ': ' + values.map(function (el) { return el.originalFileName; }).join(', ');

                            if (element.multiple) ngModel.$setViewValue(values);
                            else ngModel.$setViewValue(values.length ? values[0] : null);

                            if (loadingBar) {
                                loadingBarElement.hide();
                            }
                        });

                    function readFile(file) {
                        var deferred = $q.defer();

                        var reader = new FileReader();
                        reader.onload = function (e) {
                            var convertedFile = e.target.result;
                            var result = {
                                originalFileName: file.name.substr(0, file.name.lastIndexOf('.')),
                                fileExtension: file.name.substr(file.name.lastIndexOf('.') + 1).toLowerCase(),
                                base64Content: convertedFile.substr(convertedFile.indexOf(',') + 1)
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