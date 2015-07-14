(function() {
    'use strict';

    angular
        .module('showcaseSystem.controllers')
        .controller('HomePageController', ['homePageData', homePageController]);

    function homePageController(homePageData) {
        var vm = this;

        homePageData.getLatestProjects()
            .then(function (projects) {
                vm.latestProjects = projects;
            });
    }
}());