(function () {
    'use strict';

    var animationDirective = function animationDirective(jQuery, $window) {
        return {
            restrict: 'A',
            link: function (scope, element) {
                element.addClass('appear-animation');

                if (jQuery($window).width() > 767) {
                    element.appear(function () {
                        var delay = (element.attr("data-animation-delay") || 1);
                        if (delay > 1) {
                            element.css("animation-delay", delay + "ms");
                        }

                        element.addClass(element.attr("animate"));

                        setTimeout(function () {
                            element.addClass("animation-visible");
                        }, delay);
                    }, { accX: 0, accY: -50 });
                } else {
                    element.addClass("animation-visible");
                }
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('animate', ['jQuery', '$window', animationDirective]);
}());