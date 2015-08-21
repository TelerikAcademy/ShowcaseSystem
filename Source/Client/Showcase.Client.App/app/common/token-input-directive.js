(function () {
    'use strict';

    var tokenInputDirective = function tokenInputDirective() {
        return {
            restrict: 'A',
            scope: {
                userTags: '='
            },
            link: function (scope, element, attrs) {
                element.tokenInput(attrs.url, {
                    preventDuplicates: attrs.preventDuplicates || true,
                    theme: attrs.theme || 'showcase',
                    queryParam: attrs.queryParam,
                    minChars: attrs.minChars || 3,
                    maxChars: attrs.maxChars || 1000,
                    hintText: attrs.hintText || 'Start typing and select',
                    canAddNewTokens: attrs.canAddNewTokens || false,
                    placeholder: attrs.placeholder || '',
                    onSelectedCallback: function (values) {
                        scope.$apply(function () {
                            scope.userTags = values;
                        });
                    }
                });
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('tokenInput', [tokenInputDirective]);
}());