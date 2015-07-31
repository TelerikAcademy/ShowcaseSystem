﻿(function () {
    'use strict';

    var userProfileStatisticsDirective = function userProfileStatisticsDirective() {
        return {
            restrict: 'A',
            templateUrl: '/app/user-profile-page/user-profile-statistics-directive.html',
            scope: {
                user: '='
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('userProfileStatistics', [userProfileStatisticsDirective]);
}());