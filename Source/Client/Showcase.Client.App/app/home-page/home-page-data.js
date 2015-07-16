(function () {
    'use strict';

    var homePageData = function homePageData(data) {
        function getStatistics() {
            return data.get('statistics');
        }

        function getLatestProjects() {
            return data.get('projects');
        }

        return {
            getStatistics: getStatistics,
            getLatestProjects: getLatestProjects,
        };
    };

    angular.module('showcaseSystem.data')
        .factory('homePageData', ['data', homePageData]);
}());