(function () {
    'use strict';

    var projectDetailsData = function projectDetailsData(data, $window) {
        function getProject(id, titleUrl) {
            return data.get('projects/' + id + '/' + titleUrl);
        }

        function visitProject(id) {
            return data.post('projects/visit', id);
        }
        
        function likeProject(id) {
            return data.post('likes/like/' + id);
        }

        function dislikeProject(id) {
            return data.post('likes/dislike/' + id);
        }
        
        function flagProject(id) {
            return data.post('flags/flag/' + id);
        }

        function unflagProject(id) {
            return data.post('flags/unflag/' + id);
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