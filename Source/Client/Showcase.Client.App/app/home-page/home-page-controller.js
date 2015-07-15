﻿(function() {
    'use strict';

    angular
        .module('showcaseSystem.controllers')
        .controller('HomePageController', ['homePageData', 'notifier', homePageController]);

    function homePageController(homePageData, notifier) {
        var vm = this;

        homePageData.getLatestProjects()
            .then(function (projects) {
                vm.latestProjects = projects;
            });

        homePageData.getStatistics()
            .then(function (statistics) {
                vm.statistics = statistics;
            });
    }
}());