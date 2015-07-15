(function () {
    'use strict';

    angular
        .module('showcaseSystem.data')
        .factory('data', ['$http', '$q', 'appSettings', data]);

    function data($http, $q, appSettings) {
        function get(url) {
            var URL = appSettings.serverPath + url;
            var deferred = $q.defer();

            $http.get(URL)
                .success(function (data) {
                    deferred.resolve(data);
                });

            return deferred.promise;
        }

        return {
            get: get
        }
    }
}());