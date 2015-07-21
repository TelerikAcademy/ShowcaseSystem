(function () {
    'use strict';

    var loginModalController = function loginModalController($scope, auth) {
        var vm = this;

        vm.login = function (user) {
            auth.login(user).then(function () {
                $scope.closeModal();
            });
        };
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('LoginModalController', ['$scope', 'auth', loginModalController]);
}());