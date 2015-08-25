(function () {
    'use strict';

    var lineChartDirective = function lineChartDirective(jQuery) {
        return {
            restrict: 'A',
            templateUrl: 'statistics-page/line-chart-directive.html',
            scope: {
                stats: '='
            },
            link: function (scope, element) {
                scope.$watch('stats', function (stats) {
                    if (stats) {
                        var chartContainer = element.children(":first");
                        var pluginOptions = element.data('plugin-options');
                        var options = angular.fromJson(pluginOptions);

                        var data = {
                            labels: stats.labels,
                            datasets: [
                                {
                                    label: options.label || "My First dataset",
                                    fillColor: options.fillColor || "rgba(153,204,51,0.4)",
                                    strokeColor: options.strokeColor || "rgba(153,204,51,1)",
                                    pointColor: options.pointColor || "rgba(153,204,51,1)",
                                    pointStrokeColor: options.pointStrokeColor || "#fff",
                                    pointHighlightFill: options.pointHighlightFill || "#fff",
                                    pointHighlightStroke: options.pointHighlightStroke || "rgba(220,220,220,1)",
                                    data: stats.values
                                }
                            ]
                        };

                        Chart.defaults.global = {
                            animation: true,
                            animationSteps: 60,
                            animationEasing: "easeOutQuart",

                            showScale: true,

                            scaleOverride: false,
                            scaleSteps: null,
                            scaleStepWidth: null,
                            scaleStartValue: null,
                            scaleLineColor: "rgba(0,0,0,.1)",
                            scaleLineWidth: 1,
                            scaleShowLabels: true,
                            scaleLabel: "<%=value%>",
                            scaleIntegersOnly: true,
                            scaleBeginAtZero: false,
                            scaleFontFamily: "'Helvetica Neue', 'Helvetica', 'Arial', sans-serif",
                            scaleFontSize: 12,
                            scaleFontStyle: "normal",
                            scaleFontColor: "#666",

                            responsive: false,
                            maintainAspectRatio: true,

                            showTooltips: true,
                            customTooltips: false,

                            tooltipEvents: ["mousemove", "touchstart", "touchmove"],
                            tooltipFillColor: "rgba(0,0,0,0.8)",
                            tooltipFontFamily: "'Helvetica Neue', 'Helvetica', 'Arial', sans-serif",
                            tooltipFontSize: 14,
                            tooltipFontStyle: "normal",
                            tooltipFontColor: "#fff",
                            tooltipTitleFontFamily: "'Helvetica Neue', 'Helvetica', 'Arial', sans-serif",
                            tooltipTitleFontSize: 14,
                            tooltipTitleFontStyle: "bold",
                            tooltipTitleFontColor: "#fff",
                            tooltipYPadding: 6,
                            tooltipXPadding: 6,
                            tooltipCaretSize: 8,
                            tooltipCornerRadius: 6,
                            tooltipXOffset: 10,
                            tooltipTemplate: "<%if (label){%><%=label%>: <%}%><%= value %>",

                            multiTooltipTemplate: "<%= value %>",

                            onAnimationProgress: function() {},
                            onAnimationComplete: function() {}
                        };

                        var ctx = chartContainer.get(0).getContext("2d");
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