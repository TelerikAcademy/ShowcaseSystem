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
                notifier.success('Projects loaded!');
            });
    }
}());