(function () {
    'use strict';

    var config = function config($routeProvider, $locationProvider, $httpProvider, routeResolversProvider) {
        var CONTROLLER_VIEW_MODEL_NAME = 'vm';

        $locationProvider.html5Mode(true);

        var routeResolveChecks = routeResolversProvider.$get();
        
        $routeProvider
            .when('/', {
                templateUrl: '/app/home-page/home-page-view.html',
                controller: 'HomePageController',
                controllerAs: CONTROLLER_VIEW_MODEL_NAME,
                resolve: routeResolveChecks.home
            })
            .when('/projects/search', {
                templateUrl: '/app/projects-search-page/projects-search-page-view.html',
                controller: 'ProjectsSearchPageController',
                controllerAs: CONTROLLER_VIEW_MODEL_NAME,
                reloadOnSearch: false
            })
            .when('/projects/add', {
                templateUrl: '/app/add-project-page/add-project-view.html',
                controller: 'AddProjectController',
                controllerAs: CONTROLLER_VIEW_MODEL_NAME,
                resolve: routeResolveChecks.addProject
            })
            .when('/statistics', {
                templateUrl: '/app/statistics-page/statistics-view.html',
                controller: 'StatisticsController',
                controllerAs: CONTROLLER_VIEW_MODEL_NAME,
                resolve: routeResolveChecks.statistics
            })
            .when('/projects/:id/:title', {
                templateUrl: '/app/project-details-page/project-details-view.html',
                controller: 'ProjectDetailsController',
                controllerAs: CONTROLLER_VIEW_MODEL_NAME,
                resolve: routeResolveChecks.projectDetails
            })
            .when('/users/:username', {
                templateUrl: '/app/user-profile-page/user-profile-view.html',
                controller: 'UserProfileController',
                controllerAs: CONTROLLER_VIEW_MODEL_NAME,
                resolve: routeResolveChecks.userProfile
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

    angular.module('showcaseSystem', ['ngRoute', 'ngCookies', 'ngAnimate', 'angular-loading-bar', 'showcaseSystem.controllers', 'showcaseSystem.directives', 'infinite-scroll', 'ui.bootstrap', 'hSweetAlert'])
        .config(['$routeProvider', '$locationProvider', '$httpProvider', 'routeResolversProvider', config])
        .run(['$rootScope', '$location', 'auth', 'notifier', run])
        .value('jQuery', jQuery)
        .value('toastr', toastr)
        .constant('appSettings', {
            serverPath: '/api/',
            odataServerPath: '/odata/'
        });
}());