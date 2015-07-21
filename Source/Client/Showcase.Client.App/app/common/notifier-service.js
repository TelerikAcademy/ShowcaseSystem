(function() {
    'use strict';

    var notifierService = function notifierService(toastr) {
        toastr.options.positionClass = 'toast-top-center';
        toastr.options.preventDuplicates = true;
        toastr.options.closeButton = true;

        return {
            success: function (msg) {
                toastr.success(msg);
            },
            warning: function (msg) {
                toastr.warning(msg);
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