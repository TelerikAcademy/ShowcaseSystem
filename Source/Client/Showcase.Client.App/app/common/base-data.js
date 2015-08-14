(function () {
    'use strict';

    var baseData = function baseData($http, $q, appSettings, notifier, identity) {
        var headers = {
            'Content-Type': 'application/json'
        },
            authorizationErrorMessage = 'You must be logged in to do that';

        function get(url, authorize) {
            var deferred = $q.defer();

            if (authorize && !identity.isAuthenticated()) {
                notifier.error(authorizationErrorMessage);
                deferred.reject();
            }
            else {
                var URL = appSettings.serverPath + url;

                $http.get(URL)
                    .success(function (data) {
                        deferred.resolve(data);
                    })
                    .error(function (err) {
                        deferred.reject(err);
                    });
            }            

            return deferred.promise;
        }

        function getOData(url, authorize) {
            var deferred = $q.defer();
            var URL = appSettings.odataServerPath + url;

            $http.get(URL)
                .success(function (data) {
                    deferred.resolve(data);
                })
                .error(function (err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        }

        function post(url, data, authorize) {
            var deferred = $q.defer();

            if (authorize && !identity.isAuthenticated()) {
                notifier.error(authorizationErrorMessage);
                deferred.reject();                
            }
            else {
                var URL = appSettings.serverPath + url;

                $http.post(URL, data, headers)
                    .success(function (data) {
                        deferred.resolve(data);
                    })
                    .error(function (err) {
                        deferred.reject(err);
                    });
            }

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
        .factory('data', ['$http', '$q', 'appSettings', 'notifier', 'identity', baseData]);
}());