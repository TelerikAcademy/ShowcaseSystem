(function () {
    'use strict';

    var mainController = function mainController($location, auth, identity) {
        var vm = this;

        waitForLogin();

        vm.logout = function logout() {
            auth.logout();
            vm.currentUser = undefined;
            waitForLogin();
            $location.path('/');
        };

        vm.search = function (searchTerm) {
            $location.path('/projects/search').search('term', searchTerm);
        };

        function waitForLogin() {
            identity.getUser().then(function (user) {
                vm.currentUser = user;
            });
        }
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('MainController', ['$location', 'auth', 'identity', mainController]);
}());