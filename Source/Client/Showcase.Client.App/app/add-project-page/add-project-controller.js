(function () {
    'use strict';

    var addProjectController = function addProjectController($location, addProjectData) {
        var TAGS_SEPARATOR = ',';
        var vm = this;

        addProjectData.getSeasonTags()
            .then(function (tags) {
                vm.seasonTags = tags;
            });

        addProjectData.getLanguageAndTechnologyTags()
            .then(function (tags) {
                vm.languageAndTechnologyTags = tags;
            });

        vm.submitProject = function (project, selectedSeason, selectedLanguagesAndTechnologies) {
            vm.disabledSubmit = true;
            project = project || {}; // TODO: remove when client-side validation is added
            selectedLanguagesAndTechnologies = selectedLanguagesAndTechnologies || [];
            project.tags += (TAGS_SEPARATOR + selectedSeason + TAGS_SEPARATOR) + (selectedLanguagesAndTechnologies.join(TAGS_SEPARATOR));
            addProjectData.addProject(project)
                .then(function (projectInfo) {
                    $location.path('/projects/' + projectInfo.id + '/' + projectInfo.titleUrl);
                }, function () {
                    vm.disabledSubmit = false;
                });
        };
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('AddProjectController', ['$location', 'addProjectData', addProjectController]);
}());