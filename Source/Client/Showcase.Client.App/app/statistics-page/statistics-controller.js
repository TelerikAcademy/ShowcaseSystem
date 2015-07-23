(function () {
    'use strict';

    var statisticsController = function statisticsController(statisticsData) {
        var vm = this;
       
        statisticsData.getMainStatistics()
            .then(function (statistics) {
                vm.mainStatistics = statistics;
                console.log(statistics);
            });
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('statisticsController', ['statisticsData', statisticsController]);
}());