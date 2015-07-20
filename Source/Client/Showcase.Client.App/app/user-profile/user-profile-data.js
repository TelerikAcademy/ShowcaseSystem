(function () {
    'use strict'

    var userProfileData = function userProfileData(data) {
        function getUser(username) {
            return data.get('users/' + username);
        }

        function getComments(username, page) {
            return data.get('comments/user/' + username + '/' + page);
        }

        return {
            getUser: getUser,
            getComments: getComments
        }
    };

    angular
        .module('showcaseSystem.data')
        .factory('userProfileData', ['data', userProfileData]);
}());