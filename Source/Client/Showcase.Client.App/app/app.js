(function () {
    'use strict';

    var app = angular
        .module('showcaseSystem', ['showcaseSystem.controllers'])
        .constant('appSettings', {
            serverPath: 'http://localhost:12913/'
        });
}());