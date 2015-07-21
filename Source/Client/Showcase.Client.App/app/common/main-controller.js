(function () {
    'use strict';

    var mainController = function mainController(auth, identity) {
        var vm = this;

        waitForLogin();

        vm.logout = function logout() {
            auth.logout();
            vm.currentUser = undefined;
            waitForLogin();
        };

        function waitForLogin() {
            identity.getUser().then(function (user) {
                vm.currentUser = user;
            });
        }
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('MainController', ['auth', 'identity', mainController]);
}());