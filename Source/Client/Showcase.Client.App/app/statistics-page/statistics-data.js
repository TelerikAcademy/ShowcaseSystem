(function () {
    'use strict';

    var statisticsData = function statisticsData(data) {
        function getMainStatistics() {
            return data.get('statistics');
        }

        function getProjectsForLastSixMonths() {
            return data.get('statistics/projectslastsixmonths');
        }
        
        function getProjectsCountTag() {
            return data.get('statistics/projectscountbytag');
        }

        function getMostLikedProjects() {
            return data.get('statistics/mostliked');
        }

        return {
            getMainStatistics: getMainStatistics,
            getProjectsForLastSixMonths: getProjectsForLastSixMonths,
            getProjectsCountTag: getProjectsCountTag,
            getMostLikedProjects: getMostLikedProjects
        };
    };

    angular
        .module('showcaseSystem.data')
        .factory('statisticsData', ['data', statisticsData]);
}());