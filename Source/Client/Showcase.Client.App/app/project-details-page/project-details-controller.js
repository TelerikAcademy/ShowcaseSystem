(function () {
    'use strict';

    var projectDetailsController = function projectDetailsController(projectDetailsData, $routeParams, $window, $location, commentsData, identity, notifier) {
        var vm = this;
        var id = $routeParams.id;

        vm.commentText = '';
        vm.commentsPage = 0;
        vm.edittingComments = [];

        identity.getUser()
            .then(function (user) {
                vm.currentLoggedInUsername = user.userName;
            });

        vm.editComment = function (id) {
            vm.edittingComments[id] = true;
        };

        vm.cancelEdit = function (id) {
            vm.edittingComments[id] = false;
        };

        vm.saveComment = function (id, text) {
            if (vm.commentText.length < 10 || vm.commentText > 500) {
                notifier.error('The comment length should be between 10 and 500 symbols.');
                return;
            }

            commentsData.editComment(id, text)
                .then(function (data) {
                    vm.edittingComments[id] = false;
                });
        };
        
        projectDetailsData.getProject(id)
            .then(function (project) {
                var lastVisit = $window.localStorage.getItem("projectVisit" + id);
                if (lastVisit) {
                    var today = new Date();
                    if (daydiff(today - lastVisit) < 7) {
                        projectDetailsData.visitProject(id)
                            .then(function () {
                                $window.localStorage.setItem("projectVisit" + id, new Date());
                            });
                    }
                }
                else {
                    projectDetailsData.visitProject(id)
                        .then(function () {
                            $window.localStorage.setItem("projectVisit" + id, new Date());
                        });
                }

                vm.project = project;
                vm.likes = project.likes;
                vm.isLiked = project.isLiked;
                vm.isFlagged = project.isFlagged;
                vm.images = project.imageUrls;
            });

        commentsData.getProjectComments(id, vm.commentsPage)
            .then(function (data) {
                vm.comments = data.comments;
                vm.isLastPage = data.isLastPage;
                if (!data.isLastPage) {
                    vm.commentsPage++;
                }
            });

        vm.likeProject = function (id) {
            projectDetailsData.likeProject(id)
                .then(function () {
                    vm.likes++;
                    vm.isLiked = true;
                });
        };

        vm.dislikeProject = function (id) {
            projectDetailsData.dislikeProject(id)
                .then(function () {
                    vm.likes--;
                    vm.isLiked = false;
                });
        };

        vm.flagProject = function (id) {
            projectDetailsData.flagProject(id)
                .then(function () {
                    vm.isFlagged = true;
                });
        };

        vm.unflagProject = function (id) {
            projectDetailsData.unflagProject(id)
                .then(function () {
                    vm.isFlagged = false;
                });
        };

        vm.commentProject = function (id) {
            if (vm.commentText.length < 10 || vm.commentText > 500) {
                notifier.error('The comment length should be between 10 and 500 symbols.');
                return;
            }

            commentsData.commentProject(id, vm.commentText)
                .then(function (data) {
                    vm.comments.unshift(data);
                    vm.commentText = '';
                });
        };

        vm.loadMoreComments = function (id) {
            commentsData.getProjectComments(id, vm.commentsPage)
                .then(function (data) {
                    vm.comments = vm.comments.concat(data.comments);
                    vm.isLastPage = data.isLastPage;
                    if (!data.isLastPage) {
                        vm.commentsPage++;
                    }
                });
        };

        function daydiff(first, second) {
            return (second - first) / (1000 * 60 * 60 * 24);
        }
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('ProjectDetailsController', ['projectDetailsData', '$routeParams', '$window', '$location', 'commentsData', 'identity', 'notifier', projectDetailsController]);
}());