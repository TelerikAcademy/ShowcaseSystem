(function () {
    'use strict';

    var statisticsData = function statisticsData(data) {
        function getMainStatistics() {
            return data.get('statistics');
        }




        return {
            getMainStatistics: getMainStatistics
        };
    };

    angular
        .module('showcaseSystem.data')
        .factory('statisticsData', ['data', statisticsData]);
}());