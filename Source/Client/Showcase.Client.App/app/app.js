(function () {
    'use strict';

    var config = function config($routeProvider, $locationProvider, $httpProvider) {
        $locationProvider.html5Mode(true);

        var routeResolveChecks = {
            authenticated: {
                authenticate: ['$q', 'auth', function ($q, auth) {
                    if (!auth.isAuthenticated()) {
                        return $q.reject('not authorized');
                    }
                }]
            }
        };

        $routeProvider
            .when('/', {
                templateUrl: '/app/home-page/home-page-view.html'
            })
            .when('/projects/add', {
                templateUrl: '/app/add-project-page/add-project-view.html',
                resolve: routeResolveChecks.authenticated
            })
            .when('/projects/:id/:title', {
                templateUrl: '/app/project-details-page/project-details-view.html'
            })
            .when('/users/:username', {
                templateUrl: '/app/user-profile-page/user-profile-view.html'
            });

        $httpProvider.interceptors.push('httpResponseInterceptor');
    };

    var run = function run($rootScope, $location, auth, notifier) {
        $rootScope.$on('$routeChangeError', function (ev, current, previous, rejection) {
            if (rejection === 'not authorized') {
                angular.element('#open-login-btn').trigger('click');
            }
        });

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
        .run(['$rootScope', '$location', 'auth', 'notifier', run])
        .value('jQuery', jQuery)
        .value('toastr', toastr)
        .constant('appSettings', {
            serverPath: 'http://localhost:12345/api/'
        });
}());