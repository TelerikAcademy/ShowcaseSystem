(function() {
    'use strict';

    angular
        .module('showcaseSystem.directives')
        .directive('statistics', ['jQuery', statisticsDirective]);

    function statisticsDirective(jQuery) {
        return {
            restrict: 'A',
            templateUrl: '/app/home-page/statistics-directive.html',
            scope: {
                stats: '='
            },
            link: function(scope, element) {
                element.find('[data-to]').each(function () {
                    var $counter = jQuery(this);
                    $counter.appear(function () {
                        $counter.countTo();
                    }, { accX: 0, accY: -50 });
                });
            }
        };
    }
}());