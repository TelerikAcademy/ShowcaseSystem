(function () {
    'use strict';

    var pieChartDirective = function pieChartDirective(jQuery) {
        return {
            restrict: 'A',
            templateUrl: '/app/statistics-page/pie-chart-directive.html',
            scope: {
                stats: '='
            },
            link: function (scope, element) {
                var colors = [
                    "#99cc33", "#F7464A", "#46BFBD", "#FDB45C", "#9966ff", "", "", ""
                ];

                var highlights = [
                    "#99cc66", "#FF5A5E", "#5AD3D1", "#FFC870", "#9933cc", "", "", ""
                ];

                scope.$watch('stats', function (stats) {
                    if (stats && stats.length && stats.length > 0) {
                        var data = [];

                        for (var i = 0; i < stats.length; i++) {
                            data.push({
                                value: stats[i].count,
                                label: stats[i].tag,
                                color: colors[i],
                                highlight: highlights[i]
                            });
                        }

                        var chartContainer = element.children(":first");

                        var ctx = chartContainer.get(0).getContext("2d");
                        var chart = new Chart(ctx).Pie(data);
                    }
                });
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('pieChart', ['jQuery', pieChartDirective]);
}());