(function () {
    'use strict'

    var projectsPageController = function projectsPageController(projectsPageData, $routeParams) {
        var vm = this,
            pageIndex = $routeParams['pageIndex'] || 0;

        projectsPageData.getProjects(pageIndex)
            .then(function (projects) {
                vm.projects = projects;
            });
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('projectsPageController', ['projectsPageData', '$routeParams', projectsPageController]);
}());