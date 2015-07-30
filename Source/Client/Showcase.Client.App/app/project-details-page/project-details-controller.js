(function () {
    'use strict';

    var projectDetailsController = function projectDetailsController(projectDetailsData, $routeParams, $window, $location, commentsData, identity, notifier) {
        var vm = this;
        var id = $routeParams.id;
        
        identity.getUser()
            .then(function (user) {
                vm.currentLoggedInUsername = user.userName;
            });

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

        function daydiff(first, second) {
            return (second - first) / (1000 * 60 * 60 * 24);
        }
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('ProjectDetailsController', ['projectDetailsData', '$routeParams', '$window', '$location', 'commentsData', 'identity', 'notifier', projectDetailsController]);
}());