(function () {
    'use strict';

    var pieChartDirective = function pieChartDirective(jQuery) {
        return {
            restrict: 'A',
            templateUrl: 'statistics-page/pie-chart-directive.html',
            scope: {
                stats: '='
            },
            link: function (scope, element) {
                var colors = [
                    "#99cc33", "#F7464A", "#46BFBD", "#FDB45C", "#9966ff", "#888BDC", "#DEF25C", ""
                ];

                var highlights = [
                    "#99cc66", "#FF5A5E", "#5AD3D1", "#FFC870", "#9933cc", "#9FA1E3", "#BED435", ""
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

                        var options = {
                            //Boolean - Whether we should show a stroke on each segment
                            segmentShowStroke: true,

                            //String - The colour of each segment stroke
                            segmentStrokeColor: "#fff",

                            //Number - The width of each segment stroke
                            segmentStrokeWidth: 2,

                            //Number - The percentage of the chart that we cut out of the middle
                            percentageInnerCutout: 0, // This is 0 for Pie charts

                            //Number - Amount of animation steps
                            animationSteps: 100,

                            //String - Animation easing effect
                            animationEasing: "easeOutBounce",

                            //Boolean - Whether we animate the rotation of the Doughnut
                            animateRotate: true,

                            //Boolean - Whether we animate scaling the Doughnut from the centre
                            animateScale: false,

                            //String - A legend template
                            legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<segments.length; i++){%><li><span style=\"background-color:<%=segments[i].fillColor%>\"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>"
                        };

                        var chartContainer = element.children(":first");

                        var ctx = chartContainer.get(0).getContext("2d");
                        var chart = new Chart(ctx).Pie(data, options);
                    }
                });
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('pieChart', ['jQuery', pieChartDirective]);
}());