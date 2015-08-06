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

        function flagComment(id) {
            return data.post('flags/FlagComment/' + id);
        }

        function unFlagComment(id) {
            return data.post('flags/UnFlagComment/' + id);
        }

        return {
            getUserComments: getUserComments,
            getProjectComments: getProjectComments,
            commentProject: commentProject,
            editComment: editComment,
            flagComment: flagComment,
            unFlagComment: unFlagComment
        };
    };

    angular
        .module('showcaseSystem.data')
        .factory('commentsData', ['data', commentsData]);
}());