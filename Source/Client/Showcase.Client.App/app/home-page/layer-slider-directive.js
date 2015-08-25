(function () {
    'use strict';

    var layerSliderDirective = function layerSliderDirective() {
        return {
            restrict: 'A',
            templateUrl: 'home-page/layer-slider-directive.html',
            scope: {
                projects: '='
            },
            link: function(scope, element) {
                scope.$watch('projects', function (projects) {
                    if (projects && projects.length && projects.length > 0) {
                        element.find("div.layerslider").layerSlider({
                            responsive: false,
                            responsiveUnder: 1280,
                            layersContainer: 1280,
                            hoverPrevNext: true,
                            skinsPath: '/scripts/epona/plugins/layerslider/skins/'
                        });
                    }
                });
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('layerSlider', [layerSliderDirective]);

}());