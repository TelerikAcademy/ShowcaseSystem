(function () {
    'use strict';

    var userProfileData = function userProfileData(data) {
        function getUser(username) {
            return data.get('users/profile/' + username + '/');
        }
        
        function getLikedProjects(username) {
            return data.get('projects/likedProjects/' + username + '/');
        }
        
        function getProfile(username) {
            return data.get('users/remoteProfile/' + username + '/');
        }

        return {
            getUser: getUser,
            getLikedProjects: getLikedProjects,
            getProfile: getProfile
        };
    };

    angular
        .module('showcaseSystem.data')
        .factory('userProfileData', ['data', userProfileData]);
}());