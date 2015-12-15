(function () {
    'use strict';

    var projectDetailsController = function projectDetailsController($window, $location, $route, $sce, project, identity, sweetAlertDispatcher, notifier, projectDetailsData, addProjectData, videoUrlUtilities) {
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
            return vm.project.tags.filter(function (tag) { return tag.type == tagType || tag.type == secondTagType; });
        }

        function mapTagNames(tags) {
            return tags.map(function (tag) { return tag.name; });
        }

        function daydiff(first, second) {
            return (second - first) / (1000 * 60 * 60 * 24);
        }

        vm.editMode = false;

        vm.videoEmbedSource = $sce.trustAsResourceUrl(project.videoEmbedSource);

        vm.project = project;
        vm.likes = project.likes;
        vm.isLiked = project.isLiked;
        vm.isFlagged = project.isFlagged;
        vm.images = project.imageUrls;
        vm.isHidden = project.isHidden;

        vm.mainImage = [vm.project.mainImageUrl];

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

            vm.project.updatedImageUrls = angular.copy(vm.project.imageUrls);
            vm.project.updatedMainImageUrl = vm.project.mainImageUrl;

            addProjectData.getSeasonTags()
                .then(function (seasonTags) {
                    vm.seasonTags = mapTagNames(seasonTags);
                    vm.project.selectedSeason = filterProjectTags(0)[0].name;
                });

            addProjectData.getLanguageAndTechnologyTags()
                .then(function (languageAndTechnologyTags) {
                    vm.languageAndTechnologyTags = mapTagNames(languageAndTechnologyTags);
                    vm.project.selectedLanguagesAndTechnologies = mapTagNames(filterProjectTags(1, 2));
                });
        };

        vm.saveEdit = function () {
            if (!vm.project.updatedMainImageUrl) {
                notifier.error('Please, select main image for your project');
                return;
            }

            if (vm.project.deletedCollaborators) {
                vm.project.deletedCollaborators = vm.project.deletedCollaborators.join(',');
            }

            if (vm.project.deletedUserTags) {
                vm.project.deletedUserTags = vm.project.deletedUserTags.join(',');
            }

            vm.project.requiredTags = vm.project.selectedLanguagesAndTechnologies.join(',') + ',' + vm.project.selectedSeason;

            vm.project.videoEmbedSource = videoUrlUtilities.fixEmbedVideoSourceUrl(vm.project.videoEmbedSource);

            // TODO: extract to service
            projectDetailsData.editProject(vm.project)
                .then(function (updatedProjectInfo) {
                    if (vm.project.titleUrl == updatedProjectInfo.titleUrl) {
                        $route.reload();
                    }
                    else {
                        $location.path('/projects/' + vm.project.id + '/' + updatedProjectInfo.titleUrl);
                    }
                });
        };

        vm.cancelEdit = function () {
            vm.editMode = false;
            vm.project = angular.copy(initialProject);
        };

        vm.deleteCollaborator = function (collaborator) {
            if (collaborator.userName == vm.currentLoggedInUsername && vm.project.collaborators.length > 1) {
                notifier.warning('You have deleted yourself from this project. If you click "Save", you will not be able to edit the project again!');
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

        vm.deleteImage = function (image) {
            if (vm.project.updatedImageUrls.length <= 3) {
                notifier.error('You must have at least 3 images in your project');
                return;
            }

            var imageIndex = vm.project.updatedImageUrls.indexOf(image);
            vm.project.updatedImageUrls.splice(imageIndex, 1);

            if (vm.project.updatedMainImageUrl == image) {
                vm.project.updatedMainImageUrl = undefined;
            }
        };

        vm.selectMainImage = function (image) {
            vm.project.updatedMainImageUrl = image;
        };
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('ProjectDetailsController', ['$window', '$location', '$route', '$sce', 'project', 'identity', 'sweetAlertDispatcher', 'notifier', 'projectDetailsData', 'addProjectData', 'videoUrlUtilities', projectDetailsController]);
}());