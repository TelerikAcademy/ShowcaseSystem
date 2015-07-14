(function () {
    'use strict';


    angular.module('showcaseSystem.data', []);
    angular.module('showcaseSystem.controllers', ['showcaseSystem.data']);
    angular.module('showcaseSystem.directives', []);

    angular.module('showcaseSystem', ['ngRoute', 'showcaseSystem.controllers', 'showcaseSystem.directives'])
        .config(function ($routeProvider) {
            $routeProvider
                .when('/', {
                    templateUrl: '/app/home-page/home-page-view.html',
                });
        })
        .value('jQuery', jQuery)
        .constant('appSettings', {
            serverPath: 'http://localhost:12913/api/'
        });
}());