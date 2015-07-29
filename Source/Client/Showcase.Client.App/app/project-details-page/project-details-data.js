(function () {
    'use strict';

    var projectDetailsData = function projectDetailsData(data, $window) {
        function getProject(id) {
            return data.get('projects/' + id);
        }

        function visitProject(id) {
            return data.post('projects/visit/' + id);
        }
        
        function likeProject(id) {
            return data.post('projects/like/' + id, {});
        }

        function dislikeProject(id) {
            return data.post('projects/dislike/' + id, {});
        }
        
        function flagProject(id) {
            return data.post('projects/flag/' + id);
        }

        function unflagProject(id) {
            return data.post('projects/unflag/' + id);
        }
        
        return {
            getProject: getProject,
            likeProject: likeProject,
            dislikeProject: dislikeProject,
            visitProject: visitProject,
            flagProject: flagProject,
            unflagProject: unflagProject
        };
    };
    
    angular.module('showcaseSystem.data')
        .factory('projectDetailsData', ['data', '$window', projectDetailsData]);
}());