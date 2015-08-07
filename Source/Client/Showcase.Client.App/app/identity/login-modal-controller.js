(function () {
    'use strict';

    var loginModalController = function loginModalController($scope, auth) {
        var vm = this;

        // TODO: directive, someone?
        angular.element('#login-form').find('input').keypress(function (e) {
            if (e.which == 10 || e.which == 13) {
                $('#login-btn').click();
            }
        });

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