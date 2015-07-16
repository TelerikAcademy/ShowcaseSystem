(function () {
    'use strict';

    var revolutionSliderDirective = function revolutionSliderDirective() {
        return {
            restrict: 'A',
            templateUrl: '/app/home-page/revolution-slider-directive.html',
            link: function (scope, element) {
                element.find('.fullwidthbanner ul , .fullscreenbanner ul').removeClass('hide');

                var sliderElement = element.find('.fullwidthbanner');
                var listInSlider = sliderElement.find('ul li');

                var thumbWidth = 100,
                    thumbHeight = 50,
                    hideThumbs = 200,
                    navigationType = 'bullet',
                    navigationArrows = 'solo',
                    navigationVOffset = 10;

                // Init Revolution Slider
                var revapi = sliderElement.revolution({
                    dottedOverlay: 'none',
                    delay: 9000,
                    startwidth: 1170,
                    startheight: sliderElement.attr('data-height') || 500,
                    hideThumbs: hideThumbs,

                    thumbWidth: thumbWidth,
                    thumbHeight: thumbHeight,
                    thumbAmount: parseInt(listInSlider.length) || 2,

                    navigationType: navigationType,
                    navigationArrows: navigationArrows,
                    navigationStyle: sliderElement.attr('data-navigationStyle') || 'round',

                    touchenabled: 'on',
                    onHoverStop: 'on',

                    navigationHAlign: 'center',
                    navigationVAlign: 'bottom',
                    navigationHOffset: 0,
                    navigationVOffset: navigationVOffset,

                    soloArrowLeftHalign: 'left',
                    soloArrowLeftValign: 'center',
                    soloArrowLeftHOffset: 20,
                    soloArrowLeftVOffset: 0,

                    soloArrowRightHalign: 'right',
                    soloArrowRightValign: 'center',
                    soloArrowRightHOffset: 20,
                    soloArrowRightVOffset: 0,

                    parallax: 'mouse',
                    parallaxBgFreeze: 'on',
                    parallaxLevels: [7, 4, 3, 2, 5, 4, 3, 2, 1, 0],

                    shadow: 0,
                    fullWidth: 'on',
                    fullScreen: 'off',

                    stopLoop: 'off',
                    stopAfterLoops: -1,
                    stopAtSlide: -1,

                    spinner: 'spinner0',
                    shuffle: 'off',

                    autoHeight: 'off',
                    forceFullWidth: 'off',

                    hideThumbsOnMobile: 'off',
                    hideBulletsOnMobile: 'on',
                    hideArrowsOnMobile: 'on',
                    hideThumbsUnderResolution: 0,

                    hideSliderAtLimit: 0,
                    hideCaptionAtLimit: 768,
                    hideAllCaptionAtLilmit: 0,
                    startWithSlide: 0,
                    fullScreenOffsetContainer: ''
                });
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('revolutionSlider', [revolutionSliderDirective]);

}());