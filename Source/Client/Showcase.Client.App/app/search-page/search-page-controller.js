(function () {
    'use strict';

    var searchPageController = function searchPageController(searchPageData, $routeParams, $location) {

        var CONSTS = {
            DESC: 'desc'
        };

        var vm = this, orderBy, desc;
        
        vm.filterOptions = searchPageData.getFilterOptions();
        console.log(vm.filterOptions);
        
        vm.search = function () {
            $location.search('$orderby', vm.filterOptions.orderby.value + (vm.filterOptions.desc ? ' ' + CONSTS.DESC : ''));
            $location.search('$top', vm.filterOptions.pageSize);
            if (!!vm.filterOptions.page) {
                $location.search('$skip', vm.filterOptions.page);
            }
        };

        vm.search();
        searchPageData.searchProjects($location.url())
            .then(function (projects) {
                vm.projects = projects;
            });
    };

    angular
        .module('showcaseSystem.controllers')
        .controller('searchPageController', ['searchPageData', '$routeParams', '$location', searchPageController]);
}());