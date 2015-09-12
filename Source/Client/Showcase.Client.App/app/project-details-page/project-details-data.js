(function () {
    'use strict';

    var projectDetailsData = function projectDetailsData(data, $window) {
        function getProject(id, titleUrl) {
            return data.get('projects/' + id + '/' + titleUrl);
        }

        function editProject(project) {
            return data.post('projects/edit', project, true);
        }

        function visitProject(id) {
            return data.post('projects/visit/' + id);
        }
        
        function likeProject(id) {
            return data.post('likes/like/' + id, null, true);
        }

        function dislikeProject(id) {
            return data.post('likes/dislike/' + id, null, true);
        }
        
        function flagProject(id) {
            return data.post('flags/flag/' + id, null, true);
        }

        function unflagProject(id) {
            return data.post('flags/unflag/' + id, null, true);
        }

        function hideProject(id) {
            return data.post('projects/hide/' + id, null, true);
        }

        function unhideProject(id) {
            return data.post('projects/unhide/' + id, null, true);
        }
                
        return {
            getProject: getProject,
            editProject: editProject,
            likeProject: likeProject,
            dislikeProject: dislikeProject,
            visitProject: visitProject,
            flagProject: flagProject,
            unflagProject: unflagProject,
            hideProject: hideProject,
            unhideProject: unhideProject
        };
    };
    
    angular.module('showcaseSystem.data')
        .factory('projectDetailsData', ['data', '$window', projectDetailsData]);
}());