﻿(function () {
    'use strict';

    var projectCommentsDirective = function projectCommentsDirective($routeParams, commentsData, notifier) {
        return {
            restrict: 'A',
            templateUrl: '/app/project-details-page/project-comments-directive.html',
            scope: {
                comments: '=',
                loggedInUsername: '='
            },
            link: function (scope, element) {
                scope.edittingComments = [];
                scope.commentText = '';
                scope.commentsPage = 0;
                scope.projectId = $routeParams.id;
                
                commentsData.getProjectComments(scope.projectId, scope.commentsPage)
                    .then(function (data) {
                        scope.comments = data.comments;
                        scope.isLastPage = data.isLastPage;
                        if (!data.isLastPage) {
                            scope.commentsPage++;
                        }
                    });

                scope.editComment = function (id) {
                    scope.edittingComments[id] = true;
                };

                scope.cancelEdit = function (id) {
                    scope.edittingComments[id] = false;
                };

                scope.saveComment = function (id, text) {
                    if (text.length < 10 || text.length > 500) {
                        notifier.error('The comment length should be between 10 and 500 symbols.');
                        return;
                    }

                    commentsData.editComment(id, text)
                        .then(function (data) {
                            scope.edittingComments[id] = false;
                        });
                };

                scope.commentProject = function () {
                    if (scope.commentText.length < 10 || scope.commentText.length > 500) {
                        notifier.error('The comment length should be between 10 and 500 symbols.');
                        return;
                    }

                    commentsData.commentProject(scope.projectId, scope.commentText)
                        .then(function (data) {
                            scope.comments.unshift(data);
                            scope.commentText = '';
                        });
                };

                scope.loadMoreComments = function () {
                    commentsData.getProjectComments(scope.projectId, scope.commentsPage)
                        .then(function (data) {
                            scope.comments = scope.comments.concat(data.comments);
                            scope.isLastPage = data.isLastPage;
                            if (!data.isLastPage) {
                                scope.commentsPage++;
                            }
                        });
                };

            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('projectComments', ['$routeParams', 'commentsData', 'notifier', projectCommentsDirective]);
}());