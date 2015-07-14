(function() {
    'use strict';

    angular
        .module('showcaseSystem.controllers')
        .controller('HomePageController', ['homePageData', 'notifier', homePageController]);

    function homePageController(homePageData, notifier) {
        var vm = this;

        homePageData.getLatestProjects()
            .then(function (projects) {
                vm.latestProjects = projects;
                notifier.success('Projects loaded!');
            });
    }
}());