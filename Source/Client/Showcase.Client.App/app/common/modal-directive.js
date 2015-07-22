﻿(function () {
    'use strict';

    var modalDirective = function modalDirective() {
        return {
            restrict: 'A',
            link: function (scope, element) {
                element.on('show.bs.modal', function () {
                    var $clone = $(this).clone().css('display', 'block').appendTo('body');
                    var top = Math.round(($clone.height() - $clone.find('.modal-content').height()) / 2);
                    top = top > 0 ? top : 0;
                    $clone.remove();
                    $(this).find('.modal-content').css("margin-top", top);
                });

                scope.closeModal = function () {
                    element.modal('hide');
                };
            }
        };
    };

    angular
        .module('showcaseSystem.directives')
        .directive('modal', [modalDirective]);
}());