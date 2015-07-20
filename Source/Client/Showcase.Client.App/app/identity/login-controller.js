(function () {
    'use strict';

    var loginController = function loginController(auth) {
        var vm = this;

        vm.login = function (user) {
            auth.login(user);
        }
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('LoginController', ['auth', loginController]);
}());