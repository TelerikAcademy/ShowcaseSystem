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
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('statisticsController', ['statisticsData', statisticsController]);
}());