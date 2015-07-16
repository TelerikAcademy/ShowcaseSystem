(function () {
    'use strict';

    angular
        .module('showcaseSystem.directives')
        .directive('owlCarousel', [owlCarouselDirective]);

    function owlCarouselDirective() {
        return {
            restrict: 'A',
            templateUrl: '/app/project-details/owl-carousel-directive.html',
            link: function (scope, element) {

                // Init Owl Carousel
                //element.owlCarousel({
                //    itemsCustom: false,
                //    itemsDesktop: [1199, 4],
                //    itemsDesktopSmall: [979, 3],
                //    itemsTablet: [768, 2],
                //    itemsTabletSmall: false,
                //    itemsMobile: [479, 1],
                //    singleItem: false,
                //    itemsScaleUp: false,

                //    slideSpeed: 200,
                //    paginationSpeed: 800,
                //    rewindSpeed: 1000,

                //    autoPlay: false,
                //    stopOnHover: false,

                //    navigation: false,
                //    navigationText: ["prev", "next"],
                //    rewindNav: true,
                //    scrollPerPage: false,

                //    pagination: true,
                //    paginationNumbers: false,

                //    responsive: true,
                //    responsiveRefreshRate: 200,
                //    responsiveBaseWidth: window,

                //    baseClass: "owl-carousel",
                //    theme: "owl-theme",

                //    lazyLoad: false,
                //    lazyFollow: true,
                //    lazyEffect: "fade",

                //    autoHeight: false,

                //    jsonPath: false,
                //    jsonSuccess: false,

                //    dragBeforeAnimFinish: true,
                //    mouseDrag: true,
                //    touchDrag: true,

                //    addClassActive: false,
                //    transitionStyle: false,

                //    beforeUpdate: false,
                //    afterUpdate: false,
                //    beforeInit: false,
                //    afterInit: false,
                //    beforeMove: false,
                //    afterMove: false,
                //    afterAction: false,
                //    startDragging: false,
                //    afterLazyLoad: false
                //});
            }
        }
    };
}());