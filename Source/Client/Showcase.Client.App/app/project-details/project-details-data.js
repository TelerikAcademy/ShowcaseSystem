(function () {
    'use strict';

    var projectDetailsData = function projectDetailsData($http, $q, data, appSettings) {
        function getProject(id) {
            return data.get('projects/' + id);
        }

        function likeProject(id) {
            var URL = appSettings.serverPath + 'projects/like/' + id;
            var deferred = $q.defer();

            $http.post(URL, {})
                .success(function (data) {
                    deferred.resolve(data);
                });

            return deferred.promise;
        }

        function dislikeProject(id) {
            var URL = appSettings.serverPath + 'projects/dislike/' + id;
            var deferred = $q.defer();

            $http.post(URL, {})
                .success(function (data) {
                    deferred.resolve(data);
                });

            return deferred.promise;
        }

        function commentProject(id, text) {
            var URL = appSettings.serverPath + 'projects/comment/' + id;
            var deferred = $q.defer();

            $http.post(URL, {
                text: text
            })
                .success(function (data) {
                    deferred.resolve(data);
                });

            return deferred.promise;
        }

        return {
            getProject: getProject,
            likeProject: likeProject,
            commentProject: commentProject
        };
    };
    
    angular.module('showcaseSystem.data')
        .factory('projectDetailsData', ['$http', '$q', 'data', 'appSettings', projectDetailsData]);
}());