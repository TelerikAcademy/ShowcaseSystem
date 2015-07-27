(function () {
    'use strict';

    var addProjectController = function addProjectController() {
        var vm = this;

        vm.submitProject = function (project) {
            console.log(project);
        };
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('AddProjectController', ['$http', addProjectController]);
}());