(function () {
    'use strict';

    var userProfileCommentsDirective = function userProfileCommentsDirective(userProfileData, $routeParams) {
        return {
            restrict: 'A',
            templateUrl: '/app/user-profile-page/user-profile-comments-directive.html',
            scope: {
                comments: '=',
                username: '='
            },
            link: function (scope, element) {
                var username = $routeParams.username.toLowerCase();

                scope.commentsPage = 0;
                scope.lastPage = 0;

                userProfileData.getComments(username, scope.commentsPage)
                    .then(function (data) {
                        scope.comments = data.comments;
                        scope.isLastPage = data.isLastPage;
                        scope.lastPage = data.lastPage;
                    });

                scope.loadCommentsPage = function (page) {
                    userProfileData.getComments(username, page)
                        .then(function (data) {
                            scope.commentsPage = page;
                            scope.comments = data.comments;
                            scope.isLastPage = data.isLastPage;
                            scope.lastPage = data.lastPage;
                        });
                };

                scope.loadNextPageComments = function () {
                    if (scope.isLastPage) {
                        return;
                    }

                    userProfileData.getComments(username, scope.commentsPage + 1)
                        .then(function (data) {
                            scope.comments = data.comments;
                            scope.isLastPage = data.isLastPage;
                            scope.commentsPage++;
                            scope.lastPage = data.lastPage;
                        });
                };

                scope.loadPreviousPageComments = function () {
                    if (scope.commentsPage == 1) {
                        return;
                    }

                    userProfileData.getComments(username, scope.commentsPage - 1)
                        .then(function (data) {
                            scope.comments = data.comments;
                            scope.isLastPage = data.isLastPage;
                            scope.commentsPage--;
                            scope.lastPage = data.lastPage;
                        });
                };
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('userProfileComments', ['userProfileData', '$routeParams', userProfileCommentsDirective]);
}());