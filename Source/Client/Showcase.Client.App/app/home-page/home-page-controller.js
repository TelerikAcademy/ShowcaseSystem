(function() {
    'use strict';

    angular
        .module('showcaseSystem.controllers', ['showcaseSystem.resources'])
        .controller('HomePageController', ['homePageResource', homePageController]);

    function homePageController() {
        var vm = this;
        vm.test = 'Hello';
    }
}());