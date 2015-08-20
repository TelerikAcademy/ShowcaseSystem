(function () {
    'use strict';

    var ngPager = function pager() {
        return {
            restrict: 'A',
            templateUrl: 'common/pager-directive.html',
            scope: {
                totalPages: '@',
                currentPage: '@',
                pageChanged: '&'
            },
            link: function (scope, element) {
                scope.changed = function (newPage) {
                    if (!newPage && newPage !== 0)
                        return;

                    if (newPage < 1 || newPage > scope.totalPages)
                        return;

                    scope.pageChanged({ pageNum: newPage });
                };

                scope.$watch('currentPage', updateCurrentPage);
                scope.$watch('totalPages', updateTotalPages);

                function updateTotalPages(totalPages) {
                    updatePages(scope.currentPage, totalPages);
                }

                function updateCurrentPage(currentPage) {
                    updatePages(currentPage, scope.totalPages);
                }

                function updatePages(currentPage, totalPages) {
                    var pages = [];
                    for (var i = 1; i <= scope.totalPages; i += 1) {
                        pages.push({ pageNumber: i, isCurrent: i == currentPage });
                    }

                    scope.pages = pages;
                    scope.selectedPage = +currentPage;
                    scope.hasPreviousPage = currentPage > 1;
                    scope.hasNextPage = currentPage < totalPages;
                    scope.totalPages = totalPages;
                }
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('ngPager', [ngPager]);
}());