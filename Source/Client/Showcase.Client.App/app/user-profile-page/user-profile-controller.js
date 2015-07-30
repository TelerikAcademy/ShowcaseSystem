(function () {
    'use strict';

    var userProfileController = function userProfileController(userProfileData, $routeParams, identity, commentsData) {
        var vm = this,
            username = $routeParams.username.toLowerCase(),
            arrowDownCss = 'fa fa-long-arrow-down',
            arrowUpCss = 'fa fa-long-arrow-up';

        vm.orderBy = '-createdOn';
        vm.commentsPage = 0;

        $('.tab-button').click(function (e) {
            e.preventDefault();
        });

        userProfileData.getUser(username)
            .then(function (user) {
                vm.user = user;
            });

        userProfileData.getProfile(username)
            .then(function (profile) {
                vm.profile = profile;
            });

        commentsData.getUserComments(username, vm.commentsPage)
            .then(function (data) {
                vm.comments = data.comments;
                vm.isLastPage = data.isLastPage;
                vm.lastPage = data.lastPage;
            });

        identity.getUser()
            .then(function (user) {
                if (user.userName.toLowerCase() === username || user.isAdmin) {
                    userProfileData.getLikedProjects(username)
                        .then(function (data) {
                            vm.likedProjects = data;
                        });
                }
            });
        
        vm.loadCommentsPage = function (page) {
            commentsData.getUserComments(username, page)
                .then(function (data) {
                    vm.commentsPage = page;
                    vm.comments = data.comments;
                    vm.isLastPage = data.isLastPage;
                    vm.commentsPage = page;
                    vm.lastPage = data.lastPage;
                });
        };

        vm.loadNextPageComments = function () {
            if (vm.isLastPage) {
                return;
            }

            commentsData.getUserComments(username, vm.commentsPage + 1)
                .then(function (data) {
                    vm.comments = data.comments;
                    vm.isLastPage = data.isLastPage;
                    vm.commentsPage++;
                    vm.lastPage = data.lastPage;
                });
        };

        vm.loadPreviousPageComments = function () {
            if (vm.commentsPage == 1) {
                return;
            }

            commentsData.getUserComments(username, vm.commentsPage - 1)
                .then(function (data) {
                    vm.comments = data.comments;
                    vm.isLastPage = data.isLastPage;
                    vm.commentsPage--;
                    vm.lastPage = data.lastPage;
                });
        };
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('userProfileController', ['userProfileData', '$routeParams', 'identity', 'commentsData', userProfileController]);
}());