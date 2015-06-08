(function() {
    "use strict";

    angular
        .module("common.services")
        .factory("homePageResource", ["$resource", "appSettings", homePageResource]);

    function homePageResource($resource, appSettings) {
        return $resource(appSettings.serverPath + "/api/HomePage/");
    }
}());