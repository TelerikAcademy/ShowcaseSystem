(function () {
    'use strict';

    var projectDetailsData = function projectDetailsData(data, $window) {
        function getProject(id) {
            return data.get('projects/' + id);
        }

        function visitProject(id) {
            return data.post('projects/visit/' + id);
        }

        function getComments(id, page) {
            return data.get('comments/' + id + '/' + page);
        }

        function likeProject(id) {
            return data.post('projects/like/' + id, {});
        }

        function dislikeProject(id) {
            return data.post('projects/dislike/' + id, {});
        }

        function commentProject(id, text) {
            return data.post('comments/' + id, {
                commentText: text
            });
        }

        function flagProject(id) {
            return data.post('projects/flag/' + id);
        }

        function unflagProject(id) {
            return data.post('projects/unflag/' + id);
        }
        
        return {
            getProject: getProject,
            getComments: getComments,
            likeProject: likeProject,
            dislikeProject: dislikeProject,
            commentProject: commentProject,
            visitProject: visitProject,
            flagProject: flagProject,
            unflagProject: unflagProject
        };
    };
    
    angular.module('showcaseSystem.data')
        .factory('projectDetailsData', ['data', '$window', projectDetailsData]);
}());