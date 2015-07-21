(function () {
    'use strict';

    var mainMenuDirective = function mainMenuDirective($rootScope, $location) {
        return {
            restrict: 'A',
            templateUrl: '/app/common/main-menu-directive.html',
            link: function (scope, element) {
                updateMenu($location.path());

                element.on('click', 'li', function () {
                    var $this = $(this);
                    $this.parent().children().removeClass('active');
                    $this.addClass('active');
                });

                $rootScope.$on('$routeChangeStart', function (event, next, current) {
                    if (next && next.$$route) {
                        updateMenu(next.$$route.originalPath);
                    }
                });

                function updateMenu(route) {
                    $('#topMain .active').removeClass('active');
                    var el = angular.element('#topMain a[href="' + route + '"]');
                    if (el.length > 0) {
                        el.parent().addClass('active');
                    }
                }
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('mainMenu', ['$rootScope', '$location', mainMenuDirective]);
}());