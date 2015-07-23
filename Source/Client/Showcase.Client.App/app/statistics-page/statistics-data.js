(function () {
    'use strict';

    var statisticsData = function statisticsData(data) {
        function getMainStatistics() {
            return data.get('statistics');
        }

        function getProjectsForLastSixMonths() {
            return data.get('statistics/projectslastsixmonths');
        }


        return {
            getMainStatistics: getMainStatistics,
            getProjectsForLastSixMonths: getProjectsForLastSixMonths
        };
    };

    angular
        .module('showcaseSystem.data')
        .factory('statisticsData', ['data', statisticsData]);
}());