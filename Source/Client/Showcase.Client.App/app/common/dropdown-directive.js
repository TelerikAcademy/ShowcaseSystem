(function () {
    'use strikt';

    var dropdown = function dropdown() {
        return {
            restrict: 'A',
            templateUrl: '/app/common/dropdown-directive.html',
            scope: {
                options: '=',
                selected: '='
            },
            link: function (scope) {

            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('dropdown', [dropdown]);
}());