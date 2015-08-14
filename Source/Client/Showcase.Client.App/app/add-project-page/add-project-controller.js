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

        vm.submitProject = function (project, tags) {
            vm.disabledSubmit = true;
            // TODO: remove after client-side validation is added
            tags.userSelectedTags = tags.userSelectedTags || '';
            tags.selectedSeason = tags.selectedSeason || '';
            tags.selectedLanguagesAndTechnologies = tags.selectedLanguagesAndTechnologies || [];
            project.tags = tags.userSelectedTags + (TAGS_SEPARATOR + tags.selectedSeason + TAGS_SEPARATOR) + (tags.selectedLanguagesAndTechnologies.join(TAGS_SEPARATOR));
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