(function () {
    'use strict';

    var userProfileTeammatesDirective = function userProfileTeammatesDirective(userProfileData, $routeParams) {
        return {
            restrict: 'A',
            templateUrl: '/app/user-profile-page/user-profile-teammates-directive.html',
            scope: {
                teammates: '='
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('userProfileTeammates', ['userProfileData', '$routeParams', userProfileTeammatesDirective]);
}());