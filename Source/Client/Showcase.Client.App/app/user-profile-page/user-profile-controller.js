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

        identity.getUser()
            .then(function (user) {
                if (user.userName.toLowerCase() === username || user.isAdmin) {
                    userProfileData.getLikedProjects(username)
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