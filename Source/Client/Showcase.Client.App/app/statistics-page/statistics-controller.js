(function () {
    'use strict';

    var statisticsController = function statisticsController(statisticsData) {
        var vm = this;
       
        statisticsData.getMainStatistics()
            .then(function (statistics) {
                vm.mainStatistics = statistics;
            });

        statisticsData.getProjectsForLastSixMonths()
            .then(function (projects) {
                vm.projectsByMonth = projects;
            });

        statisticsData.getProjectsCountTag()
            .then(function (projectsByTag) {
                vm.projectsByTag = projectsByTag;
            });
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('statisticsController', ['statisticsData', statisticsController]);
}());