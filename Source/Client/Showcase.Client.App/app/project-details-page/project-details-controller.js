(function () {
    'use strict';

    var projectDetailsController = function projectDetailsController(projectDetailsData, $routeParams, $window, $location, commentsData) {
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

        vm.popup = function (url, title, w, h, text, hashTags) {
            var url = url + $location.absUrl();
            if (text !== undefined) {
                url += '&text=' + text + ' - ' + vm.project.name;
            }

            if (hashTags != undefined) {
                url += '&hashtags=' + hashTags;
            }

            var left = (screen.width / 2) - (w / 2);
            var top = (screen.height / 2) - (h / 2);
            $window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
        };

        function daydiff(first, second) {
            return (second - first) / (1000 * 60 * 60 * 24);
        }
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('ProjectDetailsController', ['projectDetailsData', '$routeParams', '$window', '$location', 'commentsData', projectDetailsController]);
}());