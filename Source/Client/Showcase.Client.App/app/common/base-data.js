(function () {
    'use strict';

    var baseData = function baseData($http, $q, appSettings) {
        var headers = {
            'Content-Type': 'application/json'
        };

        function get(url) {
            var URL = appSettings.serverPath + url;
            var deferred = $q.defer();

            $http.get(URL)
                .success(function (data) {
                    deferred.resolve(data);
                })
                .error(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function getOData(url) {
            var URL = appSettings.odataServerPath + url;
            var deferred = $q.defer();

            $http.get(URL)
                .success(function (data) {
                    deferred.resolve(data);
                })
                .error(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function post(url, data) {
            var URL = appSettings.serverPath + url;
            var deferred = $q.defer();

            $http.post(URL, data, headers)
                .success(function (data) {
                    deferred.resolve(data);
                })
                .error(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        return {
            get: get,
            getOData: getOData,
            post: post
        };
    };

    angular
        .module('showcaseSystem.data')
        .factory('data', ['$http', '$q', 'appSettings', baseData]);
}());