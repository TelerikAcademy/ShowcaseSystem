(function () {
    'use strict';

    var sweetAlertDispatcher = function sweetAlertDispatcher(sweet) {
        var defaultOptions = {           
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#cc6666',
            closeOnConfirm: false,
            closeOnCancel: true,
            html: true,
            showLoaderOnConfirm: true
        };

        function alertWithOptions(options, callback) {
            var alertOptions = angular.extend({}, defaultOptions, options);
            sweet.show(alertOptions, callback);
        }

        function simpleAlert(title, text) {
            sweet.show(title, text);
        }

        return {
            alertWithOptions: alertWithOptions,
            simpleAlert: simpleAlert
        };
    };

    angular
        .module('showcaseSystem.services')
        .factory('sweetAlertDispatcher', ['sweet', sweetAlertDispatcher]);
}());