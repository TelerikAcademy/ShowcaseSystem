﻿(function () {
    'use strict';

    var searchPageController = function searchPageController($scope, searchPageData, $routeParams, $location) {
        var vm = this,
            oDataQuery,
            CONSTS = {
                DESC: 'desc'
            };

        vm.filterOptions = searchPageData.getFilterOptions();
        vm.searchTerms = searchPageData.getSearchTerms();
        vm.query = $routeParams;

        vm.filterOptions.scrolling = localStorage.scrolling === 'true';

        vm.filter = function (query) {
            $routeParams = {
                $orderby: vm.filterOptions.orderOption.value + (vm.filterOptions.desc ? ' ' + CONSTS.DESC : ''),
                $top: vm.filterOptions.pageSize,
                $skip: $scope.currentpage || 0,
                $count: 'true'
            };

            if (query) {
                Object.keys(query)
                    .forEach(function (key) {
                        $routeParams[key] = query[key];
                    });
            }

            Object.keys($routeParams)
                .forEach(function (key) {
                    $location.search(key, $routeParams[key]);
                });

            /*
            if (query && (!!query.pageNum || query.pageNum === 0)) {
                $location.search('$skip', query.pageNum * vm.filterOptions.pageSize);
            }

            //localStorage.infiniteScrolling = vm.filterOptions.infiniteScrolling; // TODO: change
            if (!vm.filterOptions.infiniteScrolling) {
                $location.search('$count', 'true');
            }
            */
        };

        // not working if attached to vm
        $scope.changePage = function (newPage) {
            $scope.currentPage = newPage;
            vm.filter({ $skip: newPage * vm.filterOptions.pageSize });
        };

        vm.search = function () {

        };

        oDataQuery = searchPageData.getQuery($routeParams);
        var stertTime = new Date().getTime();
        searchPageData.searchProjects(oDataQuery)
            .then(function (odata) {
                // results data
                vm.projects = odata.results;
                // searchbar data
                vm.searchTerms.totalResultsCount = odata.count;
                vm.searchTerms.timeElapsed = (new Date().getTime() - stertTime) / 1000;
                // pager data
                $scope.totalPages = Math.ceil(odata.count / vm.filterOptions.pageSize);
                $scope.currentPage = !!$routeParams.$skip ? $routeParams.$skip / $routeParams.$top : 0;
            });
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('searchPageController', ['$scope', 'searchPageData', '$routeParams', '$location', searchPageController]);
}());