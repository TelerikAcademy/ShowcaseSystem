(function () {
    'use strict';

    angular
        .module('showcaseSystem', ['showcaseSystem.controllers'])
        .constant('appSettings', {
            serverPath: 'http://localhost:12913/'
        });
}());