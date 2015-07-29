﻿(function () {
    'use strict';

    var userProfileCommentsDirective = function userProfileCommentsDirective(userProfileData, $routeParams, identity) {
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
                scope.edittingComments = [];
                scope.commentsTexts = [];
                
                identity.getUser()
                    .then(function (user) {
                        scope.currentLoggedInUsername = user.userName;
                    });

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
                
                scope.editComment = function (id) {
                    scope.edittingComments[id] = true;
                };

                scope.cancelEdit = function (id) {
                    scope.edittingComments[id] = false;
                };

                scope.saveComment = function (id, text) {
                    userProfileData.editComment(id, text)
                        .then(function (data) {
                            scope.edittingComments[id] = false;
                        });
                };
            }            
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('userProfileComments', ['userProfileData', '$routeParams', 'identity', userProfileCommentsDirective]);
}());