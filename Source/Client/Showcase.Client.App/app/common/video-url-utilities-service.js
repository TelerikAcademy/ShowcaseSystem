(function() {
    'use strict';

    var videoUrlUtilities = function videoUrlUtilities() {
        function fixEmbedVideoSourceUrl(url) {
            var youTubePrefixLong = 'https://www.youtube.com';
            var youtubePrefixShort = 'https://youtu.be/';
            var vimeoPrefix = 'https://vimeo.com';
            var videoId;

            if (url.slice(0, youTubePrefixLong.length) == youTubePrefixLong) {
                videoId = url.substr(url.indexOf('=') + 1);
                return 'https://www.youtube.com/embed/' + videoId;
            }
            else if (url.slice(0, youtubePrefixShort) == youtubePrefixShort) {
                videoId = url.substr(url.lastIndexOf('/') + 1);
                return 'https://www.youtube.com/embed/' + videoId;
            }
            else if (url.slice(0, vimeoPrefix.length) == vimeoPrefix) {
                videoId = url.substr(url.lastIndexOf('/') + 1);
                return 'https://player.vimeo.com/video/' + videoId;
            }
        }

        return {
            fixEmbedVideoSourceUrl: fixEmbedVideoSourceUrl
        };
    };

    angular
        .module('showcaseSystem.services')
        .factory('videoUrlUtilities', [videoUrlUtilities]);
}());