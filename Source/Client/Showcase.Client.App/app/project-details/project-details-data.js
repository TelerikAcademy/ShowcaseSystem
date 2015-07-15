(function () {
    'use strict';

    angular.module('showcaseSystem.data')
        .factory('projectDetailsData', ['$http', '$q', 'appSettings', projectDetailsData]);

    function projectDetailsData($http, $q, appSettings) {
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
            getProject: getProject
        }
    }
}());