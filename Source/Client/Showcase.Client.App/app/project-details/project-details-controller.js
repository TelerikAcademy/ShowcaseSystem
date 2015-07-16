(function () {
    'use strict';

    var projectDetailsController = function projectDetailsController(projectDetailsData, $routeParams) {
        var vm = this;
        var id = $routeParams['id'];

        projectDetailsData.getProject(id)
            .then(function (project) {
                console.log(project);
                vm.project = project;
            });
    }

    angular
        .module('showcaseSystem.controllers')
        .controller('ProjectDetailsController', ['projectDetailsData', '$routeParams', projectDetailsController]);    
}());