(function () {
    'use strict';

    var projectDetailsController = function (projectDetailsData, $routeParams) {
        var vm = this;
        var id = $routeParams.id;

        projectDetailsData.getProject(id)
            .then(function (project) {
                vm.project = project;
            });
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('ProjectDetailsController', ['projectDetailsData', '$routeParams', projectDetailsController]);
}());