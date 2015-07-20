(function () {
    'use strict'

    var userProfileController = function userProfileController(userProfileData, $routeParams) {
        var vm = this,
            username = $routeParams['username'],
            arrowDownCss = 'fa fa-long-arrow-down',
            arrowUpCss = 'fa fa-long-arrow-up';

        vm.orderBy = '-createdOn';
        vm.commentsPage = 0;

        userProfileData.getUser(username)
            .then(function (user) {
                vm.user = user;
            });

        userProfileData.getComments(username, vm.commentsPage)
            .then(function (data) {
                vm.comments = data.comments;
                vm.isLastPage = data.isLastPage;
                vm.commentsPage++;
                console.log(data.comments);
            });

        vm.sortByDate = function (element, $event) {
            var $target = $($event.currentTarget);
            console.log(vm.orderBy);
            if (vm.orderBy == '-createdOn') {
                vm.orderBy = 'createdOn';
                removeArrowClass($target);
                $target.find('i').addClass(arrowUpCss);
            } else {
                vm.orderBy = '-createdOn';
                removeArrowClass($target);
                $target.find('i').addClass(arrowDownCss);
            }
        };

        vm.sortByVisits = function (element, $event) {
            var $target = $($event.currentTarget);

            if (vm.orderBy == '-visits') {
                vm.orderBy = 'visits';
                removeArrowClass($target);
                $target.find('i').addClass(arrowUpCss);
            } else {
                vm.orderBy = '-visits';
                removeArrowClass($target);
                $target.find('i').addClass(arrowDownCss);
            }
        };

        vm.sortByComments = function (element, $event) {
            var $target = $($event.currentTarget);

            if (vm.orderBy == '-comments') {
                vm.orderBy = 'comments';
                removeArrowClass($target);
                $target.find('i').addClass(arrowUpCss);
            } else {
                vm.orderBy = '-comments';
                removeArrowClass($target);
                $target.find('i').addClass(arrowDownCss);
            }
        };

        vm.loadMoreComments = function (id) {
            userProfileData.getComments(id, vm.commentsPage)
                .then(function (data) {
                    vm.comments = data.comments;
                    vm.isLastPage = data.isLastPage;
                    vm.commentsPage++;
                });
        };
        
        var removeArrowClass = function (element) {
            element
                .parent()
                .find('i')
                .removeClass(arrowUpCss)
                .removeClass(arrowDownCss);
        };
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('userProfileController', ['userProfileData', '$routeParams', userProfileController]);
}());