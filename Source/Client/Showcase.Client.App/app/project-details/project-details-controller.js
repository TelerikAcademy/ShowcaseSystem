(function () {
    'use strict';

    angular
        .module('showcaseSystem.controllers')
        .controller('ProjectDetailsController', ['projectDetailsData', '$routeParams', projectDetailsController]);

    function projectDetailsController(projectDetailsData, $routeParams) {
        var vm = this;
        var id = $routeParams['id'];

        projectDetailsData.getProject(id)
            .then(function (project) {
                vm.project = project;
            });
    }
}());