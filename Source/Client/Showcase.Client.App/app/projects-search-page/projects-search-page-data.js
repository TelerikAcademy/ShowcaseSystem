(function () {
    'use strict';

    var projectsSearchPageData = function projectsSearchPageData(data) {
        function searchProjects(oDataQuery) {
            oDataQuery = oDataQuery || 'Search';
            return data.getOData(oDataQuery);
        }

        return {
            searchProjects: searchProjects
        };
    };

    angular
        .module('showcaseSystem.data')
        .factory('projectsSearchPageData', ['data', projectsSearchPageData]);
}());