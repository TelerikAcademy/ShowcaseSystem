(function () {
    'use strict';

    var userProfileProjectsDirective = function userProfileProjectsDirective() {
        return {
            restrict: 'A',
            templateUrl: 'user-profile-page/user-profile-projects-directive.html',
            scope: {
                projects: '='
            },
            link: function (scope, element) {
                var arrowDownCss = 'fa fa-long-arrow-down',
                    arrowUpCss = 'fa fa-long-arrow-up',
                    allProjects;
                
                scope.$watch('scope.projects', function () {
                    if (scope.projects && scope.projects.length) {
                        scope.PROJECTS_PER_PAGE = 10;

                        scope.changePage = function (newPage) {
                            scope.currentPage = newPage;
                            scope.currentIndex = (newPage - 1) * scope.PROJECTS_PER_PAGE;
                        };

                        var lastPage = scope.projects.length % scope.PROJECTS_PER_PAGE === 0 ? scope.projects.length / scope.PROJECTS_PER_PAGE : (scope.projects.length / scope.PROJECTS_PER_PAGE) + 1;
                        scope.totalPages = lastPage === 0 ? 1 : lastPage;
                        scope.changePage(1);
                    }
                });

                scope.orderBy = '-createdOn';                

                scope.sortByDate = function (element, $event) {
                    var $target = $($event.currentTarget);

                    if (scope.orderBy == '-createdOn') {
                        scope.orderBy = 'createdOn';
                        removeArrowClass($target);
                        $target.find('i').addClass(arrowUpCss);
                    } else {
                        scope.orderBy = '-createdOn';
                        removeArrowClass($target);
                        $target.find('i').addClass(arrowDownCss);
                    }
                };

                scope.sortByVisits = function (element, $event) {
                    var $target = $($event.currentTarget);

                    if (scope.orderBy == '-visits') {
                        scope.orderBy = 'visits';
                        removeArrowClass($target);
                        $target.find('i').addClass(arrowUpCss);
                    } else {
                        scope.orderBy = '-visits';
                        removeArrowClass($target);
                        $target.find('i').addClass(arrowDownCss);
                    }
                };

                scope.sortByComments = function (element, $event) {
                    var $target = $($event.currentTarget);

                    if (scope.orderBy == '-comments') {
                        scope.orderBy = 'comments';
                        removeArrowClass($target);
                        $target.find('i').addClass(arrowUpCss);
                    } else {
                        scope.orderBy = '-comments';
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
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('userProfileProjects', ['jQuery', userProfileProjectsDirective]);
}());