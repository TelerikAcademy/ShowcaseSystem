(function () {
    'use strict';

    angular.module('showcaseSystem.controllers', []);
    angular.module('showcaseSystem.directives', []);

    angular.module('showcaseSystem', ['ngRoute', 'showcaseSystem.controllers', 'showcaseSystem.directives'])
        .config(function ($routeProvider) {
            $routeProvider
                .when('/', {
                    templateUrl: '/app/home-page/home-page-view.html',
                });
        })
        .constant('appSettings', {
            serverPath: 'http://localhost:12913/'
        });
}());