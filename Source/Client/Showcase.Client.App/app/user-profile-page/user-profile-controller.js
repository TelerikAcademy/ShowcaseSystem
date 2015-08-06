(function () {
    'use strict';

    var userProfileController = function userProfileController(userProfileData, $routeParams, identity, commentsData) {
        var vm = this,
            arrowDownCss = 'fa fa-long-arrow-down',
            arrowUpCss = 'fa fa-long-arrow-up';

        vm.orderBy = '-createdOn';
        vm.commentsPage = 0;
        vm.username = $routeParams.username.toLowerCase();

        $('.tab-button').click(function (e) {
            e.preventDefault();
        });

        userProfileData.getUser(vm.username)
            .then(function (user) {
                vm.user = user;
            });

        userProfileData.getProfile(vm.username)
            .then(function (profile) {
                vm.profile = profile;
            });

        identity.getUser()
            .then(function (user) {
                vm.isAdmin = user.isAdmin;

                if (user.userName.toLowerCase() === vm.username || user.isAdmin) {
                    userProfileData.getLikedProjects(vm.username)
                        .then(function (data) {
                            vm.likedProjects = data;
                        });
                }
            });  
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('userProfileController', ['userProfileData', '$routeParams', 'identity', 'commentsData', userProfileController]);
}());