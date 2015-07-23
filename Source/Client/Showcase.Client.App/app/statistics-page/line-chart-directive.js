(function () {
    'use strict';

    var lineChartDirective = function lineChartDirective(jQuery) {
        return {
            restrict: 'A',
            templateUrl: '/app/statistics-page/line-chart-directive.html',
            scope: {
                stats: '='
            },
            link: function (scope, element) {
                scope.$watch('stats', function (stats) {
                    if (stats) {
                        console.log(stats);
                        var data = {
                            labels: stats.labels,
                            datasets: [
                                {
                                    label: "My First dataset",
                                    fillColor: "rgba(220,220,220,0.2)",
                                    strokeColor: "rgba(220,220,220,1)",
                                    pointColor: "rgba(220,220,220,1)",
                                    pointStrokeColor: "#fff",
                                    pointHighlightFill: "#fff",
                                    pointHighlightStroke: "rgba(220,220,220,1)",
                                    data: stats.values
                                }
                            ]
                        };

                        var ctx = $("#line-chart").get(0).getContext("2d");
                        var chart = new Chart(ctx).Line(data);
                    }
                });
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('lineChart', ['jQuery', lineChartDirective]);
}());