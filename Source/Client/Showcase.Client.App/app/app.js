﻿(function () {
    'use strict';

    var config = function config($routeProvider, $locationProvider, $httpProvider) {
        $locationProvider.html5Mode(true);

        $routeProvider
            .when('/', {
                templateUrl: '/app/home-page/home-page-view.html'
            })
            .when('/projects/:id/:title', {
                templateUrl: '/app/project-details/project-details-view.html'
            })
            .when('/search', {
                templateUrl: '/app/search-page/search-page-view.html'
            });
        
        $httpProvider.interceptors.push('httpResponseInterceptor');
    };

    angular.module('showcaseSystem.data', []);
    angular.module('showcaseSystem.services', []);
    angular.module('showcaseSystem.controllers', ['showcaseSystem.data', 'showcaseSystem.services']);
    angular.module('showcaseSystem.directives', []);

    angular.module('showcaseSystem', ['ngRoute', 'showcaseSystem.controllers', 'showcaseSystem.directives'])
        .config(['$routeProvider', '$locationProvider', '$httpProvider', config])
        .value('jQuery', jQuery)
        .value('toastr', toastr)
        .constant('appSettings', {
            serverPath: 'http://localhost:12345/api/'
        });
}());