(function () {
    'use strict'

    angular
        .module('showcaseSystem.data')
        .factory('projectsPageData', ['data', projectsPageData]);

    function projectsPageData(data) {
        function getProjects(pageIndex) {
            console.log(pageIndex);
            return data.get('projects/list/' + pageIndex);
        }

        return {
            getProjects: getProjects
        }
    }
}());