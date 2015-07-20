(function () {
    'use strict';

    var data = function data($http, $q, appSettings) {
        var headers = {
            'Content-Type': 'application/json'
        };

        function get(url, isVisited) {
            var URL = appSettings.serverPath + url;
            var deferred = $q.defer();

            $http.get(URL)
                .success(function (data) {
                    deferred.resolve(data);
                });

            return deferred.promise;
        }

        function post(url, data) {
            var URL = appSettings.serverPath + url;
            var deferred = $q.defer();

            $http.post(URL, data, headers)
                .success(function (data) {
                    deferred.resolve(data);
                });

            return deferred.promise;
        }

        return {
            get: get,
            post: post
        };
    };

    angular
        .module('showcaseSystem.data')
        .factory('data', ['$http', '$q', 'appSettings', data]);
}());