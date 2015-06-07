(function() {
    "use strict";
    angular
        .module("showcaseSystem")
        .controller("ProjectListController",
            projectListCtrl);

    function projectListCtrl() {
        var vm = this;

        vm.projects = [
            {
                "name": "Telerik Academy Learning System",
                "author": "Telerik Academy"
            },
            {
                "name": "Telerik Academy Test System",
                "author": "Telerik Academy"
            },
            {
                "name": "Telerik Academy Showcase System",
                "author": "Telerik Academy"
            }
        ];
    }
}());