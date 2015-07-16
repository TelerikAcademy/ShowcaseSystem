(function () {
    'use strict';

    angular.module('showcaseSystem.data')
        .factory('projectDetailsData', ['data', projectDetailsData]);

    function projectDetailsData(data) {
        function getProject(id) {
            return data.get('projects/' + id);
        }

        return {
            getProject: getProject
        };
    }
}());