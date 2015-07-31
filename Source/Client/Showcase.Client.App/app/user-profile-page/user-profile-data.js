(function () {
    'use strict';

    var userProfileData = function userProfileData(data) {
        function getUser(username) {
            return data.get('Users/Profile/' + username);
        }
        
        function getLikedProjects(username) {
            return data.get('Projects/LikedProjects/' + username)
        }
        
        function getProfile(username) {
            return data.get('Users/RemoteProfile/' + username);
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