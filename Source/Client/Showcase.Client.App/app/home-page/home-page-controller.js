(function() {
    'use strict';

    angular
        .module('showcaseSystem.controllers')
        .controller('HomePageController', ['projectsData', 'notifier', homePageController]);

    function homePageController(projectsData, notifier) {
        var vm = this;

        projectsData.getLatestProjects()
            .then(function (projects) {
                vm.latestProjects = projects;
            });

        homePageData.getStatistics()
            .then(function (statistics) {
                vm.statistics = statistics;
            });
    }
}());