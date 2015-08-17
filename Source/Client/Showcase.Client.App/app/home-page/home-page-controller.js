(function() {
    'use strict';

    var homePageController = function homePageController(latestProjects, popularProjects, statistics) {
        var vm = this;

        vm.latestProjects = latestProjects;
        vm.mostPopularProjects = popularProjects;
        vm.statistics = statistics;
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('HomePageController', ['latestProjects', 'popularProjects', 'statistics', homePageController]);
}());