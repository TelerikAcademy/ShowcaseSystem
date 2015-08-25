(function () {
    'use strict';

    var userProfileCollaboratorsDirective = function userProfileCollaboratorsDirective(userProfileData, $routeParams) {
        return {
            restrict: 'A',
            templateUrl: 'user-profile-page/user-profile-collaborators-directive.html',
            scope: {
                collaborators: '='
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('userProfileCollaborators', ['userProfileData', '$routeParams', userProfileCollaboratorsDirective]);
}());