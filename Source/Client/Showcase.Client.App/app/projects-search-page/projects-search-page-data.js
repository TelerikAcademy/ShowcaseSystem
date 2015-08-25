(function () {
    'use strict';

    var projectsSearchPageData = function projectsSearchPageData(data) {
        function searchProjects(oDataQuery) {
            oDataQuery = oDataQuery || 'Search';
            return data.getOData(oDataQuery);
        }

        function getSeasons() {
            return data.get('tags/AllSeasonTags');
        }

        function getTechnologies() {
            return data.get('tags/AllTechnologyTags');
        }

        function getLanguages() {
            return data.get('tags/AllLanguageTags');
        }

        return {
            searchProjects: searchProjects,
            getSeasons: getSeasons,
            getTechnologies: getTechnologies,
            getLanguages: getLanguages
        };
    };

    angular
        .module('showcaseSystem.data')
        .factory('projectsSearchPageData', ['data', projectsSearchPageData]);
}());