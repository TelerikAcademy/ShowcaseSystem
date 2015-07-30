(function () {
    'use strict';

    var addProjectController = function addProjectController($location, addProjectData) {
        var vm = this;

        vm.submitProject = function (project) {
            addProjectData.addProject(project)
                .then(function (projectInfo) {
                    $location.path('/projects/' + projectInfo.id + '/' + projectInfo.title);
                });
        };
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('AddProjectController', ['$location', 'addProjectData', addProjectController]);
}());