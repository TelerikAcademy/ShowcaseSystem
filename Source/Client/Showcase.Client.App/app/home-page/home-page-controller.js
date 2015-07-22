(function() {
    'use strict';

    var homePageController = function homePageController(homePageData) {
        var vm = this;

        homePageData.getLatestProjects()
            .then(function (projects) {
                vm.latestProjects = projects;
            });

        homePageData.getMostPopularProjects()
            .then(function (projects) {
                vm.mostPopularProjects = projects;
            });

        homePageData.getStatistics()
            .then(function (statistics) {
                vm.statistics = statistics;
            });
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('HomePageController', ['homePageData', homePageController]);
}());