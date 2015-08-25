(function () {
    'use strict';

    var socialIconsDirective = function socialIconsDirective($location, $window) {
        return {
            restrict: 'A',
            templateUrl: 'project-details-page/social-icons-directive.html',
            scope: {
                name: '='
            },
            link: function (scope, element) {
                scope.popup = function (url, title, w, h, text, hashTags) {
                    url = url + $location.absUrl();
                    if (text !== undefined) {
                        url += '&text=' + text + ' - ' + name;
                    }

                    if (hashTags !== undefined) {
                        url += '&hashtags=' + hashTags;
                    }

                    var left = (screen.width / 2) - (w / 2);
                    var top = (screen.height / 2) - (h / 2);
                    $window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
                };
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('socialIcons', ['$location', '$window', socialIconsDirective]);
}());