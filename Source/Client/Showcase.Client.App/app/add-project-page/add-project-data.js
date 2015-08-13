(function () {
    'use strict';

    var addProjectData = function addProjectData(data) {
        function getSeasonTags() {
            return data.get('tags/allSeasonTags');
        }

        function getLanguageAndTechnologyTags() {
            return data.get('tags/allLanguageAndTechnologyTags');
        }

        function addProject(project) {
            return data.post('projects', project);
        }

        return {
            getSeasonTags: getSeasonTags,
            getLanguageAndTechnologyTags: getLanguageAndTechnologyTags,
            addProject: addProject
        };
    };

    angular.module('showcaseSystem.data')
        .factory('addProjectData', ['data', addProjectData]);
}());