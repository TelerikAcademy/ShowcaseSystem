(function () {
    'use strict';

    var notFoundController = function notFoundController() {
        var vm = this;

        vm.text = 'Not Found';
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('NotFoundController', [notFoundController]);
}());