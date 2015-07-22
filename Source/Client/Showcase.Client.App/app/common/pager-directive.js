(function () {
    'use strict';

    var ngPager = function pager(jQuery) {
        return {
            restrict: 'A',
            templateUrl: '/app/common/pager-directive.html',
            scope: {
                totalPages: '@',
                currentPage: '@',
                pageChanged: '&'
            },
            link: function (scope, element) {
                scope.changed = function (newPage) {
                    if (!newPage && newPage !== 0)
                        return;

                    if (newPage < 0 || newPage >= scope.totalPages)
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
                    for (var i = 0; i < scope.totalPages; i += 1) {
                        pages.push({ pageNumber: i, isCurrent: i == currentPage });
                    }
                    scope.pages = pages;
                    scope.selectedPage = +currentPage;
                    scope.hasPreviousPage = currentPage > 0;
                    scope.hasNextPage = currentPage < totalPages - 1;
                    scope.totalPages = totalPages;
                }
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('ngPager', ['jQuery', ngPager]);
}());