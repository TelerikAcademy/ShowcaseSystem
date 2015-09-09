(function () {
    'use strict';

    var projectDetailsController = function projectDetailsController($window, project, identity, sweetAlertDispatcher, notifier, projectDetailsData) {
        var vm = this;
        var id = project.id;
        var initialProject;

        // TODO: extract visits to service
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

        vm.editMode = false;
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
                    return element.userName === user.userName;
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
            sweetAlertDispatcher.showHideProjectAlert(id)
                .then(function () {
                    vm.isHidden = true;
                });
        };

        vm.unhideProject = function (id) {
            projectDetailsData.unhideProject(id)
                .then(function () {
                    vm.isHidden = false;
                });
        };

        vm.startEdit = function () {
            vm.editMode = true;
            initialProject = angular.copy(project);
        };

        vm.saveEdit = function () {
            projectDetailsData.editProject(project)
                .then(function (updatedProjectInfo) {
                    vm.editMode = false;
                    vm.project.titleUrl = updatedProjectInfo.titleUrl;
                });
        };

        vm.cancelEdit = function () {
            console.log(vm.project);
            vm.editMode = false;
            vm.project = initialProject;
        };

        vm.deleteCollaborator = function (collaborator) {
            vm.project.deletedCollaborators = vm.project.deletedCollaborators || [];
            vm.project.deletedCollaborators.push(collaborator.userName);
            if (vm.project.collaborators.length > 1) {
                var indexOfCollaborator = vm.project.collaborators.indexOf(collaborator);
                vm.project.collaborators.splice(indexOfCollaborator, 1);
            }
            else {
                notifier.error('You must have at least 1 collaborator in your project');
            }
        }

        function daydiff(first, second) {
            return (second - first) / (1000 * 60 * 60 * 24);
        }
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('ProjectDetailsController', ['$window', 'project', 'identity', 'sweetAlertDispatcher', 'notifier', 'projectDetailsData', projectDetailsController]);
}());