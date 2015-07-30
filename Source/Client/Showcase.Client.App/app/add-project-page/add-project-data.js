(function () {
    'use strict';

    var addProjectData = function addProjectData(data) {
        function addProject(project) {
            return data.post('projects', project);
        }

        return {
            addProject: addProject
        };
    };

    angular.module('showcaseSystem.data')
        .factory('addProjectData', ['data', addProjectData]);
}());