(function () {
    'use strict';

    var homePageData = function homePageData(data) {
        function getLatestProjects() {
            return data.get('projects');
        }

        function getMostPopularProjects() {
            return data.get('projects/popular');
        }

        return {
            getLatestProjects: getLatestProjects,
            getMostPopularProjects: getMostPopularProjects
        };
    };

    angular.module('showcaseSystem.data')
        .factory('homePageData', ['data', homePageData]);
}());