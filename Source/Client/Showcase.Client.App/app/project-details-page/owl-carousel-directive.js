(function () {
    'use strict';

    var owlCarouselDirective = function owlCarouselDirective() {
        return {
            restrict: 'A',
            templateUrl: 'project-details-page/owl-carousel-directive.html',
            scope: {
                images: '='
            },
            link: function (scope, element) {
                scope.$watch('images', function (images) {
                    if (images && images.length && images.length > 0) {
                        var slider = element.children(":first");

                        var options = slider.attr('data-plugin-options');

                        var defaults = {
                            items: 4,
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

                        var config = jQuery.extend({}, defaults, slider.data("plugin-options"), element.data("plugin-options"));
                        console.log(config);
                        slider.owlCarousel(config).addClass("owl-carousel-init");
                        
                        jQuery.extend(true, jQuery.magnificPopup.defaults, {
                            tClose: 'Close',
                            tLoading: 'Loading...',
                            gallery: {
                                tPrev: 'Previous',
                                tNext: 'Next',
                                tCounter: '%curr% / %total%'
                            },
                            image: {
                                tError: 'Image not loaded!'
                            },
                            ajax: {
                                tError: 'Content not loaded!'
                            }
                        });

                        config = {};
			            var defaultSettings = {
			                    type: 'image',
			                    fixedContentPos: false,
			                    fixedBgPos: false,
			                    mainClass: 'mfp-no-margins mfp-with-zoom',
			                    image: {
			                        verticalFit: true
			                    },

			                    zoom: {
			                        enabled: false,
			                        duration: 300
			                    },

			                    gallery: {
			                        enabled: false,
			                        navigateByImgClick: true,
			                        preload: [0, 1],
			                        arrowMarkup: '<button title="%title%" type="button" class="mfp-arrow mfp-arrow-%dir%"></button>',
			                        tPrev: 'Previou',
			                        tNext: 'Next',
			                        tCounter: '<span class="mfp-counter">%curr% / %total%</span>'
			                    },
			                };

                        if (slider.data("plugin-options")) {
                            config = jQuery.extend({}, defaultSettings, options, slider.data("plugin-options"));
                        }

                        jQuery(element).magnificPopup(config);
                    }
                });
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('owlCarousel', ['jQuery', owlCarouselDirective]);
}());