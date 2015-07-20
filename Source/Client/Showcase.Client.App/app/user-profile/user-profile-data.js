(function () {
    'use strict'

    var userProfileData = function userProfileData(data) {
        function getUser(username) {            
            return data.get('users/' + username);
        }

        return {
            getUser: getUser
        }
    };

    angular
        .module('showcaseSystem.data')
        .factory('userProfileData', ['data', userProfileData]);
}());