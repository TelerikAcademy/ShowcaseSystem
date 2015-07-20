(function () {
    'use strict';

    var mainMenuDirective = function mainMenuDirective() {
        return {
            restrict: 'A',
            templateUrl: '/app/common/main-menu-directive.html',
            link: function (scope, element) {
                element.on('click', 'li', function () {
                    var $this = $(this);
                    $this.parent().children().removeClass('active');
                    $this.addClass('active');
                });
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('mainMenu', [mainMenuDirective]);
}());