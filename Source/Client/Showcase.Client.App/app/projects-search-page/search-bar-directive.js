(function () {
    'use strict';
    
    var searchBar = function searchBar() {
        return {
            restrict: 'A',
            templateUrl: '/app/projects-search-page/search-bar-directive.html',
            scope: {
                searchTerms: '=',
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('searchBar', [searchBar]);

}());