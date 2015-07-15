(function () {
    'use strict';
    
    angular.module('showcaseSystem.data')
        .factory('homePageData', ['$http', '$q', 'appSettings', homePageData]);

    function homePageData($http, $q, appSettings) {
        function getLatestProjects() {
            var URL = appSettings.serverPath + 'homePage';
            var deferred = $q.defer();

            $http.get(URL)
                .success(function (data) {
                    deferred.resolve(data);
                });

            return deferred.promise;
        }

        function getStatistics() {
            var URL = appSettings.serverPath + 'statistics';
            var deferred = $q.defer();

            $http.get(URL)
                .success(function(data) {
                    deferred.resolve(data);
                });

            return deferred.promise;
        }

        return {
            getLatestProjects: getLatestProjects,
            getStatistics: getStatistics
        }
    }
}());