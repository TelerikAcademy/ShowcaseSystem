(function () {
    'use strict';

    var httpResponseInterceptor = function httpResponseInterceptor($q, notifier) {
        return {
            'response': function (response) {
                if (response.data.success !== undefined) {
                    if (response.data.success === true) {
                        response.data = response.data.data;
                    }
                    else if (response.data.success === false) {
                        notifier.error(response.data.errorMessage);
                        return $q.reject(response.data.errorMessage);
                    }
                }
                return response;
            },
            'responseError': function (rejection) {
                if (rejection.data && rejection.data['error_description']) {
                    notifier.error(rejection.data['error_description']);
                }
                else {
                    notifier.error('No connection to the server! Your Internet may be down!');
                }

                return $q.reject(rejection);
            }
        };
    };

    angular
        .module('showcaseSystem')
        .factory('httpResponseInterceptor', ['$q', 'notifier', httpResponseInterceptor]);
}());