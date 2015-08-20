(function () {
    'use strict';

    var sweetAlertDispatcher = function sweetAlertDispatcher($q, sweet, projectDetailsData) {
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

        function showSimpleAlert(title, text) {
            sweet.show(title, text);
        }

        function showHideProjectAlert(id) {
            var hideProjectOptions = {
                title: 'Hide',
                text: 'Hidden projects can only be seen by their collaborators and admins and <strong>only admins</strong> can reveal a hidden project.<br />Are you sure you want to hide this project?',
                confirmButtonText: 'Yes, hide it!'
            };

            var deferred = $q.defer();
            var alertOptions = angular.extend({}, defaultOptions, hideProjectOptions);

            sweet.show(alertOptions, function (isConfirmed) {
                if (isConfirmed) {
                    projectDetailsData.hideProject(id)
                        .then(function () {
                            deferred.resolve();
                            showSimpleAlert('Hidden', 'The project is now hidden');
                        });
                }
                else {
                    deferred.reject();
                }
            });

            return deferred.promise;
        }

        return {
            alertWithOptions: alertWithOptions,
            showSimpleAlert: showSimpleAlert,
            showHideProjectAlert: showHideProjectAlert
        };
    };

    angular
        .module('showcaseSystem.services')
        .factory('sweetAlertDispatcher', ['$q', 'sweet', 'projectDetailsData', sweetAlertDispatcher]);
}());