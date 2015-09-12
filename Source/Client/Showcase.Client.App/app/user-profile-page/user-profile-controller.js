(function () {
    'use strict';

    var userProfileController = function userProfileController(userProfileData, commentsData, identity, user, profile) {
        var vm = this,
            arrowDownCss = 'fa fa-long-arrow-down',
            arrowUpCss = 'fa fa-long-arrow-up';

        vm.orderBy = '-createdOn';
        vm.commentsPage = 0;
        vm.user = user;
        vm.profile = profile;
        vm.username = vm.user.username.toLowerCase();

        // TODO: $ should not be here!
        $('.tab-button').click(function (e) {
            e.preventDefault();
        });

        identity.getUser()
            .then(function (user) {
                vm.isAdmin = user.isAdmin;
                vm.currentlyLoggedUser = user;

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
        .controller('UserProfileController', ['userProfileData', 'commentsData', 'identity', 'user', 'profile', userProfileController]);
}());