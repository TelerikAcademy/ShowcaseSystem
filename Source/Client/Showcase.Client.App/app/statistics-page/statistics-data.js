(function () {
    'use strict';

    var statisticsData = function statisticsData(data) {
        function getMainStatistics() {
            return data.get('statistics');
        }

        function getProjectsForLastSixMonths() {
            return data.get('statistics/projectsLastSixMonths');
        }
        
        function getProjectsCountTag() {
            return data.get('statistics/projectsCountByTag');
        }

        function getMostLikedProjects() {
            return data.get('statistics/topProjects');
        }

        function getTopUsers() {
            return data.get('statistics/topUsers');
        }

        return {
            getMainStatistics: getMainStatistics,
            getProjectsForLastSixMonths: getProjectsForLastSixMonths,
            getProjectsCountTag: getProjectsCountTag,
            getMostLikedProjects: getMostLikedProjects,
            getTopUsers: getTopUsers
        };
    };

    angular
        .module('showcaseSystem.data')
        .factory('statisticsData', ['data', statisticsData]);
}());