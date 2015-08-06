(function () {
    'use strict';

    var commentsData = function commentsData(data) {
        function getUserComments(username, page) {
            return data.get('comments/user/' + username + '/' + page);
        }

        function getProjectComments(id, page) {
            return data.get('comments/' + id + '/' + page);
        }

        function commentProject(id, text) {
            return data.post('comments/' + id, {
                commentText: text
            });
        }

        function editComment(id, text) {
            return data.post('comments/edit/', {
                id: id,
                commentText: text
            });
        }

        return {
            getUserComments: getUserComments,
            getProjectComments: getProjectComments,
            commentProject: commentProject,
            editComment: editComment
        };
    };

    angular
        .module('showcaseSystem.data')
        .factory('commentsData', ['data', commentsData]);
}());