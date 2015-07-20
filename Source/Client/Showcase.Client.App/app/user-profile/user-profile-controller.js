(function () {
    'use strict'

    var userProfileController = function userProfileController(userProfileData, $routeParams) {
        var vm = this,
            username = $routeParams['username'];

        userProfileData.getUser(username)
            .then(function (user) {
                vm.user = user;
            });
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('userProfileController', ['userProfileData', '$routeParams', userProfileController]);
}());