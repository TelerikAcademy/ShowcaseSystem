(function () {
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
            .when('/users/:username', {
                templateUrl: '/app/user-profile/user-profile-view.html'
            })
            .when('/search', {
                templateUrl: '/app/search-page/search-page-view.html'
            });
        
        $httpProvider.interceptors.push('httpResponseInterceptor');
    };

    var run = function run(auth, notifier) {
        if (auth.isAuthenticated()) {
            auth.getIdentity().then(function (identity) {
                notifier.success('Welcome back, ' + identity.userName + '!');
            });
        }
    };

    angular.module('showcaseSystem.data', []);
    angular.module('showcaseSystem.services', []);
    angular.module('showcaseSystem.controllers', ['showcaseSystem.data', 'showcaseSystem.services']);
    angular.module('showcaseSystem.directives', []);

    angular.module('showcaseSystem', ['ngRoute', 'ngCookies', 'ngAnimate', 'angular-loading-bar', 'showcaseSystem.controllers', 'showcaseSystem.directives'])
        .config(['$routeProvider', '$locationProvider', '$httpProvider', config])
        .run(['auth', 'notifier', run])
        .value('jQuery', jQuery)
        .value('toastr', toastr)
        .constant('appSettings', {
            serverPath: 'http://localhost:12345/api/'
        });
}());