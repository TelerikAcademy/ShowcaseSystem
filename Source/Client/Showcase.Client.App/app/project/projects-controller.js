(function () {
    'use strict';

    angular
        .module('showcaseSystem.controllers')
        .controller('ProjectsController', ['projectsData', '$routeParams', projectsController]);

    function projectsController(projectsData, $routeParams) {
        var vm = this;
        var id = $routeParams['id'];

        projectsData.getProject(id)
            .then(function (project) {
                vm.project = project.data;
            });
    }
}());