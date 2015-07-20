(function() {
    'use strict';

    var notifierService = function notifierService(toastr) {
        toastr.options.positionClass = 'toast-top-center';
        toastr.options.preventDuplicates = true;

        return {
            success: function (msg) {
                toastr.success(msg);
            },
            error: function (msg) {
                toastr.error(msg);
            }
        };
    };

    angular
        .module('showcaseSystem.services')
        .factory('notifier', ['toastr', notifierService]);
}());