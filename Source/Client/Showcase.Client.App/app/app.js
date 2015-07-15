(function () {
    'use strict';

    angular.module('showcaseSystem.data', []);
    angular.module('showcaseSystem.services', []);
    angular.module('showcaseSystem.controllers', ['showcaseSystem.data', 'showcaseSystem.services']);
    angular.module('showcaseSystem.directives', []);

    angular.module('showcaseSystem', ['ngRoute', 'showcaseSystem.controllers', 'showcaseSystem.directives'])
        .config(function ($routeProvider, $locationProvider, $httpProvider) {
            $locationProvider.html5Mode(true);

            $routeProvider
                .when('/', {
                    templateUrl: '/app/home-page/home-page-view.html'
                })
                .when('/test', {
                    templateUrl: '/app/home-page/home-page-view.html'
                });

            $httpProvider.interceptors.push(['$q', 'notifier', function ($q, notifier) {
                return {
                    'responseError': function (rejection) {
                        notifier.error('No connection to the server! Your Internet may be down!');
                        return $q.reject(rejection);
                    }
                };
            }]);
        })
        .value('jQuery', jQuery)
        .value('toastr', toastr)
        .constant('appSettings', {
            serverPath: 'http://localhost:12345/api/'
        });
}());