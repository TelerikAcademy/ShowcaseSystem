(function () {
    'use strict';

    var statisticsController = function statisticsController(detailedStatistics) {
        var vm = this;
       
        vm.mainStatistics = detailedStatistics.mainStatistics;
        vm.projectsByMonth = detailedStatistics.projectsLastSixMonths;
        vm.projectsByTag = detailedStatistics.projectsCountByTag;
        vm.mostLiked = detailedStatistics.mostLikedProjects;
        vm.topUsers = detailedStatistics.topUsers;
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('StatisticsController', ['detailedStatistics', statisticsController]);
}());