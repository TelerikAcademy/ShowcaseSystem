﻿(function () {
    'use strict';

    angular.module('showcaseSystem.data', []);
    angular.module('showcaseSystem.services', []);
    angular.module('showcaseSystem.controllers', ['showcaseSystem.data', 'showcaseSystem.services']);
    angular.module('showcaseSystem.directives', []);

    angular.module('showcaseSystem', ['ngRoute', 'showcaseSystem.controllers', 'showcaseSystem.directives'])
        .config(['$routeProvider', '$locationProvider', '$httpProvider', function ($routeProvider, $locationProvider, $httpProvider) {
            $locationProvider.html5Mode(true);

            $routeProvider
                .when('/', {
                    templateUrl: '/app/home-page/home-page-view.html'
                })
                .when('/projects/list/:pageIndex?', {
                    templateUrl: '/app/projects-page/projects-page-view.html'
                })
                .when('/projects/:id/:title', {
                    templateUrl: '/app/project-details/project-details-view.html'
                });

            $httpProvider.interceptors.push('httpResponseInterceptor');
        }])
        .value('jQuery', jQuery)
        .value('toastr', toastr)
        .constant('appSettings', {
            serverPath: 'http://localhost:12345/api/'
        });
}());