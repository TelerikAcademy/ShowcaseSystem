(function () {
    'use strict';

    var modalDirective = function modalDirective($location) {
        return {
            restrict: 'A',
            link: function (scope, element) {
                element.on('show.bs.modal', function () {
                    var $clone = $(this).clone().css('display', 'block').appendTo('body');
                    var top = Math.round(($clone.height() - $clone.find('.modal-content').height()) / 2);
                    top = top > 0 ? top : 0;
                    $clone.remove();
                    $(this).find('.modal-content').css("margin-top", top);
                });

                var buttons = element.find('.modal-footer button');
                $(buttons[0]).on('click', function () {
                    debugger;
                    moveToRoute('previous-route');
                });

                $(buttons[buttons.length - 1]).on('click', function () {
                    moveToRoute('current-route');
                });

                scope.closeModal = function () {
                    element.modal('hide');
                };

                function moveToRoute(attr) {
                    var route = element.data(attr);
                    if (route) {
                        $location.path(route);
                    }
                }
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('modal', ['$location', modalDirective]);
}());