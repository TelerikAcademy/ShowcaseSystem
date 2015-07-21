(function () {
    'use strict';

    var mainController = function mainController(identity) {
        var vm = this;

        identity.getUser().then(function (user) {
            vm.currentUser = user;
        });
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('MainController', ['identity', mainController]);
}());