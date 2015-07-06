(function() {
    'use strict';

    angular
        .module('showcaseSystem.controllers', ['showcaseSystem.resources'])
        .controller('HomePageController', ['homePageResource', homePageController]);

    function homePageController(homePageResource) {
        var vm = this;

        homePageResource.query(function(data) {
            vm.projects = data;
        });
    }
}());