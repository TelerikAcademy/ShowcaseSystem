(function () {
    'use strict';

    var routeResolversProvider = {
        authenticated: ['$q', 'auth', function ($q, auth) {
            if (!auth.isAuthenticated()) {
                return $q.reject('not authorized');
            }

            return $q.when(true);
        }],
        latestProjects: ['homePageData', function (homePageData) {
            return homePageData.getLatestProjects();
        }],
        popularProjects: ['homePageData', function (homePageData) {
            return homePageData.getMostPopularProjects();
        }],
        statistics: ['statisticsData', function (statisticsData) {
            return statisticsData.getMainStatistics();
        }],
        seasonTags: ['$injector', '$q', 'addProjectData', function ($injector, $q, addProjectData) {
            var authPromise = $injector.invoke(routeResolversProvider.authenticated);
            return authPromise.then(function () {
                return addProjectData.getSeasonTags();
            });
        }],
        languageAndTechnologyTags: ['$injector', '$q', 'addProjectData', function ($injector, $q, addProjectData) {
            var authPromise = $injector.invoke(routeResolversProvider.authenticated);
            return authPromise.then(function () {
                return addProjectData.getLanguageAndTechnologyTags();
            });
        }],
        detailedStatistics: ['$q', 'statisticsData', function ($q, statisticsData) {
            return $q.all([
                statisticsData.getMainStatistics(),
                statisticsData.getProjectsForLastSixMonths(),
                statisticsData.getProjectsCountTag(),
                statisticsData.getMostLikedProjects(),
                statisticsData.getTopUsers()
            ]).then(function (results) {
                return {
                    mainStatistics: results[0],
                    projectsLastSixMonths: results[1],
                    projectsCountByTag: results[2],
                    mostLikedProjects: results[3],
                    topUsers: results[4],
                };
            });
        }]
    };

    var config = function config($routeProvider, $locationProvider, $httpProvider) {
        var CONTROLLER_VIEW_MODEL_NAME = 'vm';

        $locationProvider.html5Mode(true);

        var routeResolveChecks = {
            home: {
                latestProjects: routeResolversProvider.latestProjects,
                popularProjects: routeResolversProvider.popularProjects,
                statistics: routeResolversProvider.statistics
            },
            addProject: {
                seasonTags: routeResolversProvider.seasonTags,
                languageAndTechnologyTags: routeResolversProvider.languageAndTechnologyTags
            },
            statistics: {
                detailedStatistics: routeResolversProvider.detailedStatistics
            }
        };

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