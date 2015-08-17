(function () {
    'use strict';

    var projectDetailsController = function projectDetailsController($routeParams, $window, $location, projectDetailsData, commentsData, identity, notifier, sweetAlertDispatcher) {
        var vm = this;
        var id = $routeParams.id;
        var titleUrl = $routeParams.title;

        projectDetailsData.getProject(id, titleUrl)
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
                vm.isHidden = project.isHidden;

                identity.getUser()
                    .then(function (user) {
                        vm.isAdmin = user.isAdmin;
                        vm.currentLoggedInUsername = user.userName;

                        vm.isOwnProject = vm.project.collaborators.some(function (element, index, collaborators) {
                            return element.username === user.userName;
                        });
                    });
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

        vm.hideProject = function (id) {
            sweetAlertDispatcher.alertWithOptions({
                title: 'Hide',
                text: 'Hidden projects can only be seen by their collaborators and admins and <strong>only admins</strong> can reveal a hidden project.<br />Are you sure you want to hide this project?',
                confirmButtonText: 'Yes, hide it!'
            }, function (isConfirmed) {
                if (isConfirmed) {
                    projectDetailsData.hideProject(id)
                        .then(function () {
                            vm.isHidden = true;
                            sweetAlertDispatcher.simpleAlert('Hidden', 'The project is now hidden');
                        });
                }
            });
        };

        vm.unhideProject = function (id) {
            projectDetailsData.unhideProject(id)
                .then(function () {
                    vm.isHidden = false;
                });
        };

        function daydiff(first, second) {
            return (second - first) / (1000 * 60 * 60 * 24);
        }
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('ProjectDetailsController', ['$routeParams', '$window', '$location', 'projectDetailsData', 'commentsData', 'identity', 'notifier', 'sweetAlertDispatcher', projectDetailsController]);
}());