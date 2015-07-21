(function () {
    'use strict';

    var owlCarouselDirective = function owlCarouselDirective() {
        return {
            restrict: 'A',
            templateUrl: '/app/project-details-page/owl-carousel-directive.html',
            scope: {
                images: '='
            },
            link: function (scope, element) {
                scope.$watch('images', function (images) {
                    if (images && images.length && images.length > 0) {
                        var slider = element.children(":first");

                        var options = slider.attr('data-plugin-options');

                        var defaults = {
                            items: 5,
                            itemsCustom: false,
                            itemsDesktop: [1199, 4],
                            itemsDesktopSmall: [980, 3],
                            itemsTablet: [768, 2],
                            itemsTabletSmall: false,
                            itemsMobile: [479, 1],
                            singleItem: true,
                            itemsScaleUp: false,

                            slideSpeed: 200,
                            paginationSpeed: 800,
                            rewindSpeed: 1000,

                            autoPlay: false,
                            stopOnHover: false,

                            navigation: false,
                            navigationText: [
                                                '<i class="fa fa-chevron-left"></i>',
                                                '<i class="fa fa-chevron-right"></i>'
                            ],
                            rewindNav: true,
                            scrollPerPage: false,

                            pagination: true,
                            paginationNumbers: false,

                            responsive: true,
                            responsiveRefreshRate: 200,
                            responsiveBaseWidth: window,

                            baseClass: "owl-carousel",
                            theme: "owl-theme",

                            lazyLoad: false,
                            lazyFollow: true,
                            lazyEffect: "fade",

                            autoHeight: false,

                            jsonPath: false,
                            jsonSuccess: false,

                            dragBeforeAnimFinish: true,
                            mouseDrag: true,
                            touchDrag: true,

                            transitionStyle: false,

                            addClassActive: false,

                            beforeUpdate: false,
                            afterUpdate: false,
                            beforeInit: false,
                            afterInit: false,
                            beforeMove: false,
                            afterMove: false,
                            afterAction: false,
                            startDragging: false,
                            afterLazyLoad: false
                        };

                        var config = jQuery.extend({}, defaults, options, slider.data("plugin-options"));
                        slider.owlCarousel(config).addClass("owl-carousel-init");
                    }
                });
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('owlCarousel', ['jQuery', owlCarouselDirective]);
}());