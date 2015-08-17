﻿(function () {
    'use strict';

    var routeResolvers = {
        authenticate: ['$q', 'auth', function ($q, auth) {
            if (!auth.isAuthenticated()) {
                return $q.reject('not authorized');
            }
        }],
        latestProjects: ['homePageData', function (homePageData) {
            return homePageData.getLatestProjects();
        }],
        popularProjects: ['homePageData', function (homePageData) {
            return homePageData.getMostPopularProjects();
        }],
        statistics: ['statisticsData', function (statisticsData) {
            return statisticsData.getMainStatistics();
        }]
    };

    var config = function config($routeProvider, $locationProvider, $httpProvider) {
        $locationProvider.html5Mode(true);

        var routeResolveChecks = {
            authenticated: {
                authenticate: routeResolvers.authenticate
            },
            home: {
                latestProjects: routeResolvers.latestProjects,
                popularProjects: routeResolvers.popularProjects,
                statistics: routeResolvers.statistics
            }
        };

        $routeProvider
            .when('/', {
                templateUrl: '/app/home-page/home-page-view.html',
                controller: 'HomePageController',
                controllerAs: 'vm',
                resolve: routeResolveChecks.home
            })
            .when('/projects/search', {
                templateUrl: '/app/projects-search-page/projects-search-page-view.html',
                reloadOnSearch: false
            })
            .when('/projects/add', {
                templateUrl: '/app/add-project-page/add-project-view.html',
                resolve: routeResolveChecks.authenticated
            })
            .when('/statistics', {
                templateUrl: '/app/statistics-page/statistics-view.html'
            })
            .when('/projects/:id/:title', {
                templateUrl: '/app/project-details-page/project-details-view.html'
            })
            .when('/users/:username', {
                templateUrl: '/app/user-profile-page/user-profile-view.html'
            })
            .when('/notfound', {
                templateUrl: '/app/not-found-page/not-found-view.html'
            })
            .otherwise({ redirectTo: '/notfound' });

        $httpProvider.interceptors.push('httpResponseInterceptor');
    };

    var run = function run($rootScope, $location, auth, notifier) {
        $rootScope.$on('$routeChangeError', function (ev, current, previous, rejection) {
            if (rejection === 'not authorized') {
                notifier.warning('Please log into your account first!');
                $location.path('/');
                angular
                    .element('#open-login-btn')
                    .trigger('click');

                angular.element('#login-modal')
                    .attr('data-previous-route', previous.$$route.originalPath)
                    .attr('data-current-route', current.$$route.originalPath);
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

    angular.module('showcaseSystem', ['ngRoute', 'ngCookies', 'ngAnimate', 'angular-loading-bar', 'showcaseSystem.controllers', 'showcaseSystem.directives', 'infinite-scroll', 'ui.bootstrap'])
        .config(['$routeProvider', '$locationProvider', '$httpProvider', config])
        .run(['$rootScope', '$location', 'auth', 'notifier', run])
        .value('jQuery', jQuery)
        .value('toastr', toastr)
        .constant('appSettings', {
            serverPath: '/api/',
            odataServerPath: '/odata/'
        });
}());