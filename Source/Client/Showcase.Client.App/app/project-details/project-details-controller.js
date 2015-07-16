(function () {
    'use strict';

    var projectDetailsController = function projectDetailsController(projectDetailsData, $routeParams) {
        var vm = this;
        var id = $routeParams['id'];

        projectDetailsData.getProject(id)
            .then(function (project) {
                vm.project = project;
                vm.likes = project.likes;
            });

        vm.likeProject = function (id) {
            projectDetailsData.likeProject(id)
                .then(function () {
                    vm.likes++;
                });
        };

        vm.dislikeProject = function (id) {
            projectDetailsData.dislikeProject(id)
                .then(function () {
                    vm.likes--;
                });
        };
    }

    angular
        .module('showcaseSystem.controllers')
        .controller('ProjectDetailsController', ['projectDetailsData', '$routeParams', projectDetailsController]);    
}());