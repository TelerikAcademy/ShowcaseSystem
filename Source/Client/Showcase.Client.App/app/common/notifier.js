(function() {
    'use strict';

    angular
        .module('showcaseSystem.services')
        .factory('notifier', ['toastr', notifier]);

    function notifier(toastr) {
        toastr.options.positionClass = "toast-top-full-width";

        return {
            success: function(msg) {
                toastr.success(msg);
            },
            error: function(msg) {
                toastr.error(msg);
            }
        };
    }
}());