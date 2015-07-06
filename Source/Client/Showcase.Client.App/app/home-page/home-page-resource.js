(function() {
    'use strict';

    angular
        .module('showcaseSystem.resources', ['ngResource'])
        .factory('homePageResource', ['$resource', 'appSettings', homePageResource]);

    function homePageResource($resource, appSettings) {
        return $resource(appSettings.serverPath + '/api/HomePage/');
    }
}());