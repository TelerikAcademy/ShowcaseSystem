(function () {
    'use strict';
    
    angular.module('showcaseSystem.data')
        .factory('projectsData', ['$http', '$q', 'appSettings', projectsData]);

    function projectsData($http, $q, appSettings) {
        function getLatestProjects() {
            var URL = appSettings.serverPath + 'projects';
            var deferred = $q.defer();

            $http.get(URL)
                .success(function (data) {
                    deferred.resolve(data);
                });

            return deferred.promise;
        }

        function getProject(id) {
            var URL = appSettings.serverPath + 'projects/' + id;
            var deferred = $q.defer();

            $http.get(URL)
                .success(function (data) {
                    deferred.resolve(data);
                });

            return deferred.promise;
        }

        return {
            getLatestProjects: getLatestProjects,
            getProject: getProject
        }
    }
}());