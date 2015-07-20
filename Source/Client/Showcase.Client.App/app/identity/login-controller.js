(function () {
    'use strict';

    var loginController = function loginController($scope, auth) {
        var vm = this;

        vm.login = function (user) {
            auth.login(user).then(function () {
                $scope.closeModal();
            });
        }
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('LoginController', ['$scope', 'auth', loginController]);
}());