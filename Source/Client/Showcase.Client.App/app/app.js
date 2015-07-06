(function () {
    'use strict';

    var app = angular
        .module('showcaseSystem', ['showcaseSystem.controllers', 'showcaseSystem.resources'])
        .constant('appSettings', {
            serverPath: 'http://localhost:12913/'
        });;
}());