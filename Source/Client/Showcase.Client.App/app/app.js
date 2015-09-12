(function () {
    'use strict';

    var config = function config($routeProvider, $locationProvider, $httpProvider, routeResolversProvider) {
        var CONTROLLER_VIEW_MODEL_NAME = 'vm';

        $locationProvider.html5Mode(true);

        var routeResolveChecks = routeResolversProvider.$get();
        
        $routeProvider
            .when('/', {
                templateUrl: 'home-page/home-page-view.html',
                controller: 'HomePageController',
                controllerAs: CONTROLLER_VIEW_MODEL_NAME,
                resolve: routeResolveChecks.home
            })
            .when('/projects/search', {
                templateUrl: 'projects-search-page/projects-search-page-view.html',
                controller: 'ProjectsSearchPageController',
                controllerAs: CONTROLLER_VIEW_MODEL_NAME,
                reloadOnSearch: false
            })
            .when('/projects/add', {
                templateUrl: 'add-project-page/add-project-view.html',
                controller: 'AddProjectController',
                controllerAs: CONTROLLER_VIEW_MODEL_NAME,
                resolve: routeResolveChecks.addProject
            })
            .when('/about', {
                templateUrl: 'about-page/about-view.html',
                controller: 'AboutPageController',
                controllerAs: CONTROLLER_VIEW_MODEL_NAME
            })
            .when('/statistics', {
                templateUrl: 'statistics-page/statistics-view.html',
                controller: 'StatisticsController',
                controllerAs: CONTROLLER_VIEW_MODEL_NAME,
                resolve: routeResolveChecks.statistics
            })
            .when('/projects/:id/:title', {
                templateUrl: 'project-details-page/project-details-view.html',
                controller: 'ProjectDetailsController',
                controllerAs: CONTROLLER_VIEW_MODEL_NAME,
                resolve: routeResolveChecks.projectDetails
            })
            .when('/users/:username', {
                templateUrl: 'user-profile-page/user-profile-view.html',
                controller: 'UserProfileController',
                controllerAs: CONTROLLER_VIEW_MODEL_NAME,
                resolve: routeResolveChecks.userProfile
            })
            .when('/notfound', {
                templateUrl: 'not-found-page/not-found-view.html'
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

    angular.module('templates', []); // used for client-side template caching
    angular.module('showcaseSystem.data', []);
    angular.module('showcaseSystem.services', []);
    angular.module('showcaseSystem.controllers', ['showcaseSystem.data', 'showcaseSystem.services']);
    angular.module('showcaseSystem.directives', []);

    angular.module('showcaseSystem', ['ngRoute', 'ngCookies', 'ngAnimate', 'angular-loading-bar', 'templates', 'ui.bootstrap', 'hSweetAlert', 'showcaseSystem.controllers', 'showcaseSystem.directives', 'infinite-scroll'])
        .config(['$routeProvider', '$locationProvider', '$httpProvider', 'routeResolversProvider', config])
        .run(['$rootScope', '$location', 'auth', 'notifier', run])
        .value('jQuery', jQuery)
        .value('toastr', toastr)
        .constant('appSettings', {
            serverPath: '/api/',
            odataServerPath: '/odata/',
            version: 'Showcase System 1.0 (build 20150912.88ac5c3)'
        });
}());