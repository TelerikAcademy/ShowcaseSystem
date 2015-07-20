(function () {
    'use strict';

    var identityService = function identityService() {
        var currentUser = {};

        return {
            getUser: function () {
                return currentUser;
            },
            isAuthenticated: function () {
                return Object.getOwnPropertyNames(currentUser).length !== 0;
            },
            setUser: function (user) {
                currentUser = user;
            },
            removeUser: function () {
                currentUser = {};
            }
        };
    };

    angular
        .module('showcaseSystem.services')
        .factory('identity', [identityService]);
}());