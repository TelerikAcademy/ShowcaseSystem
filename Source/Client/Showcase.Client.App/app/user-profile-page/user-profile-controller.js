(function () {
    'use strict';

    var userProfileController = function userProfileController(userProfileData, $routeParams) {
        var vm = this,
            username = $routeParams.username,
            arrowDownCss = 'fa fa-long-arrow-down',
            arrowUpCss = 'fa fa-long-arrow-up';

        vm.orderBy = '-createdOn';
        vm.commentsPage = 1;
        vm.lastPage = 1;

        userProfileData.getUser(username)
            .then(function (user) {
                vm.user = user;
            });

        userProfileData.getComments(username, vm.commentsPage)
            .then(function (data) {
                vm.comments = data.comments;
                vm.isLastPage = data.isLastPage;
                vm.lastPage = data.lastPage;
            });

        vm.loadCommentsPage = function (page) {
            userProfileData.getComments(username, page)
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

            userProfileData.getComments(username, vm.commentsPage + 1)
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

            userProfileData.getComments(username, vm.commentsPage - 1)
                .then(function (data) {
                    vm.comments = data.comments;
                    vm.isLastPage = data.isLastPage;
                    vm.commentsPage--;
                    vm.lastPage = data.lastPage;
                });
        };

        vm.sortByDate = function (element, $event) {
            var $target = $($event.currentTarget);

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