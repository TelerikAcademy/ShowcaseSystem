﻿(function () {
    'use strict';

    var userProfileData = function userProfileData(data) {
        function getUser(username) {
            return data.get('users/profile/' + username);
        }

        function getComments(username, page) {
            return data.get('comments/user/' + username + '/' + page);
        }

        function getLikedProjects(username) {
            return data.get('projects/likedprojects/' + username)
        }

        function editComment(id, text) {
            return data.post('comments/edit/' + id, {
                commentText: text
            });
        }

        return {
            getUser: getUser,
            getComments: getComments,
            getLikedProjects: getLikedProjects,
            editComment: editComment
        };
    };

    angular
        .module('showcaseSystem.data')
        .factory('userProfileData', ['data', userProfileData]);
}());