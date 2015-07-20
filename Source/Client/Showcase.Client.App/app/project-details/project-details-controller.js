(function () {
    'use strict';

    var projectDetailsController = function projectDetailsController(projectDetailsData, $routeParams, $window) {
        var vm = this;
        var id = $routeParams.id;

        vm.commentText = '';
        vm.commentsPage = 1;
        
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
                vm.images = project.imageUrls;
            });

        projectDetailsData.getComments(id, vm.commentsPage)
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

        vm.commentProject = function (id) {
            projectDetailsData.commentProject(id, vm.commentText)
                .then(function (data) {
                    vm.comments.unshift(data);
                    vm.commentText = '';
                });
        }

        vm.loadMoreComments = function (id) {
            projectDetailsData.getComments(id, vm.commentsPage)
                .then(function (data) {
                    vm.comments = vm.comments.concat(data.comments);
                    vm.isLastPage = data.isLastPage;
                    if (!data.isLastPage) {
                        vm.commentsPage++;
                    }
                });
        }

        function daydiff(first, second) {
            return (second - first) / (1000 * 60 * 60 * 24);
        }
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('ProjectDetailsController', ['projectDetailsData', '$routeParams', '$window', projectDetailsController]);
}());