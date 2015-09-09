(function () {
    'use strict';

    var projectDetailsController = function projectDetailsController($window, project, identity, sweetAlertDispatcher, notifier, projectDetailsData, addProjectData) {
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

        function setIsOwnProject() {
            vm.isOwnProject = vm.project.collaborators.some(function (element, index, collaborators) {
                return element.userName === vm.currentLoggedInUsername;
            });
        }

        function filterProjectTags(tagType, secondTagType) {
            return vm.project.tags.filter(function (tag) { return tag.type == tagType || tag.type == secondTagType });
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
                setIsOwnProject();
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
            if (!initialProject) {
                initialProject = angular.copy(vm.project);
            }

            addProjectData.getSeasonTags()
                .then(function (seasonTags) {
                    vm.seasonTags = seasonTags;
                    vm.project.selectedSeason = filterProjectTags(0)[0];
                });

            addProjectData.getLanguageAndTechnologyTags()
                .then(function (languageAndTechnologyTags) {
                    vm.languageAndTechnologyTags = languageAndTechnologyTags.map(function(tag) { return tag.name });
                    vm.project.selectedLanguagesAndTechnologies = filterProjectTags(1, 2).map(function (tag) { return tag.name; });
                });
        };

        vm.saveEdit = function () {
            console.log(vm.project);
            if (vm.project.deletedCollaborators) {
                vm.project.deletedCollaborators = vm.project.deletedCollaborators.join(',');
            }

            projectDetailsData.editProject(vm.project)
                .then(function (updatedProjectInfo) {
                    vm.editMode = false;
                    initialProject = undefined;
                    vm.project.titleUrl = updatedProjectInfo.titleUrl;
                    vm.project.collaborators = updatedProjectInfo.collaborators;
                    vm.project.deletedCollaborators = undefined;
                    vm.project.newCollaborators = undefined;
                    setIsOwnProject();
                });
        };

        vm.cancelEdit = function () {
            vm.editMode = false;
            vm.project = angular.copy(initialProject);
        };

        vm.deleteCollaborator = function (collaborator) {
            if (collaborator.userName == vm.currentLoggedInUsername && vm.project.collaborators.length > 1) {
                notifier.warning('You have deleted yourself from this project. If you click "Save", you will not be able to edit the project again!')
            }

            if (vm.project.collaborators.length > 1) {
                vm.project.deletedCollaborators = vm.project.deletedCollaborators || [];
                vm.project.deletedCollaborators.push(collaborator.userName);
                var indexOfCollaborator = vm.project.collaborators.indexOf(collaborator);
                vm.project.collaborators.splice(indexOfCollaborator, 1);
            }
            else {
                notifier.error('You must have at least 1 collaborator in your project');
            }
        };

        function daydiff(first, second) {
            return (second - first) / (1000 * 60 * 60 * 24);
        }
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('ProjectDetailsController', ['$window', 'project', 'identity', 'sweetAlertDispatcher', 'notifier', 'projectDetailsData', 'addProjectData', projectDetailsController]);
}());