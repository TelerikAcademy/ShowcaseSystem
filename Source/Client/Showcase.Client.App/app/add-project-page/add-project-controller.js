(function () {
    'use strict';

    var addProjectController = function addProjectController($http) {
        var vm = this;

        vm.submitProject = function (project) {
            $http.post('/api/projects', project);
        };
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('AddProjectController', ['$http', addProjectController]);
}());