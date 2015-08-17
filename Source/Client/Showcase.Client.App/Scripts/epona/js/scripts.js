jQuery(window).ready(function () {
    Epona();
});

function Epona() {
    jQuery.browserDetect();

    _topNav();
    _scrollTo();
    _placeholder();

    if (jQuery("html").hasClass("chrome") && jQuery("body").hasClass("smoothscroll")) {
        jQuery.smoothScroll();
    }

    /** mobile - hide on click! **/
    jQuery(document).bind("click", function () {
        if (jQuery("div.navbar-collapse").hasClass('in')) {
            jQuery("button.btn-mobile").trigger('click');
        }
    });
}

function _topNav() {
    window.scrollTop = 0;

    jQuery(window).scroll(function () {
        _toTop();
    });

    /* Scroll To Top */
    function _toTop() {
        _scrollTop = jQuery(document).scrollTop();

        if (_scrollTop > 100) {

            if (jQuery("#toTop").is(":hidden")) {
                jQuery("#toTop").show();
            }

        } else {

            if (jQuery("#toTop").is(":visible")) {
                jQuery("#toTop").hide();
            }

        }

    }


    // Mobile Submenu
    var addActiveClass = false;
    jQuery("#topMain a.dropdown-toggle").bind("click", function (e) {
        if (jQuery(this).attr('href') == '#') {
            e.preventDefault();
        }

        addActiveClass = jQuery(this).parent().hasClass("resp-active");
        jQuery("#topMain").find(".resp-active").removeClass("resp-active");

        if (!addActiveClass) {
            jQuery(this).parents("li").addClass("resp-active");
        }

        return;

    });

    // Drop Downs - do not hide on click
    jQuery("#topMain li.dropdown, #topMain a.dropdown-toggle").bind("click", function (e) {
        e.stopPropagation();
    });

    // IE11 Bugfix
    // Version 1.1
    // Wednesday, July 23, 2014
    if (jQuery("html").hasClass("ie") || jQuery("html").hasClass("ff3")) {
        jQuery("#topNav ul.nav > li.mega-menu div").addClass('block');
        jQuery("#topNav ul.nav > li.mega-menu div div").addClass('pull-left');
    }
}

function _scrollTo() {

    jQuery("a.scrollTo").bind("click", function (e) {
        e.preventDefault();

        var href = jQuery(this).attr('href');

        if (href != '#') {
            jQuery('html,body').animate({ scrollTop: jQuery(href).offset().top }, 800, 'easeInOutExpo');
        }
    });

    jQuery("a#toTop").bind("click", function (e) {
        e.preventDefault();
        jQuery('html,body').animate({ scrollTop: 0 }, 800, 'easeInOutExpo');
    });
}

function _placeholder() {

    //check for IE
    if (navigator.appVersion.indexOf("MSIE") != -1) {

        jQuery('[placeholder]').focus(function () {

            var input = jQuery(this);
            if (input.val() == input.attr('placeholder')) {
                input.val('');
                input.removeClass('placeholder');
            }

        }).blur(function () {

            var input = jQuery(this);
            if (input.val() == '' || input.val() == input.attr('placeholder')) {
                input.addClass('placeholder');
                input.val(input.attr('placeholder'));
            }

        }).blur();

    }
}

// scroll 
function wheel(e) {
    e.preventDefault();
}

function disable_scroll() {
    if (window.addEventListener) {
        window.addEventListener('DOMMouseScroll', wheel, false);
    }
    window.onmousewheel = document.onmousewheel = wheel;
}

function enable_scroll() {
    if (window.removeEventListener) {
        window.removeEventListener('DOMMouseScroll', wheel, false);
    }
    window.onmousewheel = document.onmousewheel = document.onkeydown = null;
}

// overlay
function enable_overlay() {
    jQuery("span.global-overlay").remove(); // remove first!
    jQuery('body').append('<span class="global-overlay"></span>');
}
function disable_overlay() {
    jQuery("span.global-overlay").remove();
}



/** COUNT TO
	https://github.com/mhuggins/jquery-countTo
 **************************************************************** **/
(function ($) {
    $.fn.countTo = function (options) {
        options = options || {};

        return jQuery(this).each(function () {
            // set options for current element
            var settings = jQuery.extend({}, $.fn.countTo.defaults, {
                from: jQuery(this).data('from'),
                to: jQuery(this).data('to'),
                speed: jQuery(this).data('speed'),
                refreshInterval: jQuery(this).data('refresh-interval'),
                decimals: jQuery(this).data('decimals')
            }, options);

            // how many times to update the value, and how much to increment the value on each update
            var loops = Math.ceil(settings.speed / settings.refreshInterval),
				increment = (settings.to - settings.from) / loops;

            // references & variables that will change with each update
            var self = this,
				$self = jQuery(this),
				loopCount = 0,
				value = settings.from,
				data = $self.data('countTo') || {};

            $self.data('countTo', data);

            // if an existing interval can be found, clear it first
            if (data.interval) {
                clearInterval(data.interval);
            }
            data.interval = setInterval(updateTimer, settings.refreshInterval);

            // __construct the element with the starting value
            render(value);

            function updateTimer() {
                value += increment;
                loopCount++;

                render(value);

                if (typeof (settings.onUpdate) == 'function') {
                    settings.onUpdate.call(self, value);
                }

                if (loopCount >= loops) {
                    // remove the interval
                    $self.removeData('countTo');
                    clearInterval(data.interval);
                    value = settings.to;

                    if (typeof (settings.onComplete) == 'function') {
                        settings.onComplete.call(self, value);
                    }
                }
            }

            function render(value) {
                var formattedValue = settings.formatter.call(self, value, settings);
                $self.html(formattedValue);
            }
        });
    };

    $.fn.countTo.defaults = {
        from: 0,               // the number the element should start at
        to: 0,                 // the number the element should end at
        speed: 1000,           // how long it should take to count between the target numbers
        refreshInterval: 100,  // how often the element should be updated
        decimals: 0,           // the number of decimal places to show
        formatter: formatter,  // handler for formatting the value before rendering
        onUpdate: null,        // callback method for every time the element is updated
        onComplete: null       // callback method for when the element finishes updating
    };

    function formatter(value, settings) {
        return value.toFixed(settings.decimals);
    }
}(jQuery));

/** BROWSER DETECT
	Add browser to html class
 **************************************************************** **/
(function ($) {
    $.extend({

        browserDetect: function () {

            var u = navigator.userAgent,
				ua = u.toLowerCase(),
				is = function (t) {
				    return ua.indexOf(t) > -1;
				},
				g = 'gecko',
				w = 'webkit',
				s = 'safari',
				o = 'opera',
				h = document.documentElement,
				b = [(!(/opera|webtv/i.test(ua)) && /msie\s(\d)/.test(ua)) ? ('ie ie' + parseFloat(navigator.appVersion.split("MSIE")[1])) : is('firefox/2') ? g + ' ff2' : is('firefox/3.5') ? g + ' ff3 ff3_5' : is('firefox/3') ? g + ' ff3' : is('gecko/') ? g : is('opera') ? o + (/version\/(\d+)/.test(ua) ? ' ' + o + RegExp.jQuery1 : (/opera(\s|\/)(\d+)/.test(ua) ? ' ' + o + RegExp.jQuery2 : '')) : is('konqueror') ? 'konqueror' : is('chrome') ? w + ' chrome' : is('iron') ? w + ' iron' : is('applewebkit/') ? w + ' ' + s + (/version\/(\d+)/.test(ua) ? ' ' + s + RegExp.jQuery1 : '') : is('mozilla/') ? g : '', is('j2me') ? 'mobile' : is('iphone') ? 'iphone' : is('ipod') ? 'ipod' : is('mac') ? 'mac' : is('darwin') ? 'mac' : is('webtv') ? 'webtv' : is('win') ? 'win' : is('freebsd') ? 'freebsd' : (is('x11') || is('linux')) ? 'linux' : '', 'js'];

            c = b.join(' ');
            h.className += ' ' + c;

            var isIE11 = !(window.ActiveXObject) && "ActiveXObject" in window;

            if (isIE11) {
                jQuery('html').removeClass('gecko').addClass('ie ie11');
                return;
            }

        }

    });
})(jQuery);


/** SMOOTHSCROLL V1.2.1
	Licensed under the terms of the MIT license.
 **************************************************************** **/
(function ($) {
    $.extend({

        smoothScroll: function () {

            // Scroll Variables (tweakable)
            var defaultOptions = {

                // Scrolling Core
                frameRate: 60, // [Hz]
                animationTime: 700, // [px]
                stepSize: 120, // [px]

                // Pulse (less tweakable)
                // ratio of "tail" to "acceleration"
                pulseAlgorithm: true,
                pulseScale: 10,
                pulseNormalize: 1,

                // Acceleration
                accelerationDelta: 20,  // 20
                accelerationMax: 1,   // 1

                // Keyboard Settings
                keyboardSupport: true,  // option
                arrowScroll: 50,     // [px]

                // Other
                touchpadSupport: true,
                fixedBackground: true,
                excluded: ""
            };

            var options = defaultOptions;

            // Other Variables
            var isExcluded = false;
            var isFrame = false;
            var direction = { x: 0, y: 0 };
            var initDone = false;
            var root = document.documentElement;
            var activeElement;
            var observer;
            var deltaBuffer = [120, 120, 120];

            var key = {
                left: 37, up: 38, right: 39, down: 40, spacebar: 32,
                pageup: 33, pagedown: 34, end: 35, home: 36
            };


            /***********************************************
			 * INITIALIZE
			 ***********************************************/

            /**
			 * Tests if smooth scrolling is allowed. Shuts down everything if not.
			 */
            function initTest() {

                var disableKeyboard = false;

                // disable keys for google reader (spacebar conflict)
                if (document.URL.indexOf("google.com/reader/view") > -1) {
                    disableKeyboard = true;
                }

                // disable everything if the page is blacklisted
                if (options.excluded) {
                    var domains = options.excluded.split(/[,\n] ?/);
                    domains.push("mail.google.com"); // exclude Gmail for now
                    for (var i = domains.length; i--;) {
                        if (document.URL.indexOf(domains[i]) > -1) {
                            observer && observer.disconnect();
                            removeEvent("mousewheel", wheel);
                            disableKeyboard = true;
                            isExcluded = true;
                            break;
                        }
                    }
                }

                // disable keyboard support if anything above requested it
                if (disableKeyboard) {
                    removeEvent("keydown", keydown);
                }

                if (options.keyboardSupport && !disableKeyboard) {
                    addEvent("keydown", keydown);
                }
            }

            /**
			 * Sets up scrolls array, determines if frames are involved.
			 */
            function init() {

                if (!document.body) return;

                var body = document.body;
                var html = document.documentElement;
                var windowHeight = window.innerHeight;
                var scrollHeight = body.scrollHeight;

                // check compat mode for root element
                root = (document.compatMode.indexOf('CSS') >= 0) ? html : body;
                activeElement = body;

                initTest();
                initDone = true;

                // Checks if this script is running in a frame
                if (top != self) {
                    isFrame = true;
                }

                    /**
                     * This fixes a bug where the areas left and right to
                     * the content does not trigger the onmousewheel event
                     * on some pages. e.g.: html, body { height: 100% }
                     */
                else if (scrollHeight > windowHeight &&
						(body.offsetHeight <= windowHeight ||
						 html.offsetHeight <= windowHeight)) {

                    // DOMChange (throttle): fix height
                    var pending = false;
                    var refresh = function () {
                        if (!pending && html.scrollHeight != document.height) {
                            pending = true; // add a new pending action
                            setTimeout(function () {
                                html.style.height = document.height + 'px';
                                pending = false;
                            }, 500); // act rarely to stay fast
                        }
                    };
                    html.style.height = 'auto';
                    setTimeout(refresh, 10);

                    var config = {
                        attributes: true,
                        childList: true,
                        characterData: false
                    };

                    observer = new MutationObserver(refresh);
                    observer.observe(body, config);

                    // clearfix
                    if (root.offsetHeight <= windowHeight) {
                        var underlay = document.createElement("div");
                        underlay.style.clear = "both";
                        body.appendChild(underlay);
                    }
                }

                // gmail performance fix
                if (document.URL.indexOf("mail.google.com") > -1) {
                    var s = document.createElement("style");
                    s.innerHTML = ".iu { visibility: hidden }";
                    (document.getElementsByTagName("head")[0] || html).appendChild(s);
                }
                    // facebook better home timeline performance
                    // all the HTML resized images make rendering CPU intensive
                else if (document.URL.indexOf("www.facebook.com") > -1) {
                    var home_stream = document.getElementById("home_stream");
                    home_stream && (home_stream.style.webkitTransform = "translateZ(0)");
                }
                // disable fixed background
                if (!options.fixedBackground && !isExcluded) {
                    body.style.backgroundAttachment = "scroll";
                    html.style.backgroundAttachment = "scroll";
                }
            }


            /************************************************
			 * SCROLLING
			 ************************************************/

            var que = [];
            var pending = false;
            var lastScroll = +new Date;

            /**
			 * Pushes scroll actions to the scrolling queue.
			 */
            function scrollArray(elem, left, top, delay) {

                delay || (delay = 1000);
                directionCheck(left, top);

                if (options.accelerationMax != 1) {
                    var now = +new Date;
                    var elapsed = now - lastScroll;
                    if (elapsed < options.accelerationDelta) {
                        var factor = (1 + (30 / elapsed)) / 2;
                        if (factor > 1) {
                            factor = Math.min(factor, options.accelerationMax);
                            left *= factor;
                            top *= factor;
                        }
                    }
                    lastScroll = +new Date;
                }

                // push a scroll command
                que.push({
                    x: left,
                    y: top,
                    lastX: (left < 0) ? 0.99 : -0.99,
                    lastY: (top < 0) ? 0.99 : -0.99,
                    start: +new Date
                });

                // don't act if there's a pending queue
                if (pending) {
                    return;
                }

                var scrollWindow = (elem === document.body);

                var step = function (time) {

                    var now = +new Date;
                    var scrollX = 0;
                    var scrollY = 0;

                    for (var i = 0; i < que.length; i++) {

                        var item = que[i];
                        var elapsed = now - item.start;
                        var finished = (elapsed >= options.animationTime);

                        // scroll position: [0, 1]
                        var position = (finished) ? 1 : elapsed / options.animationTime;

                        // easing [optional]
                        if (options.pulseAlgorithm) {
                            position = pulse(position);
                        }

                        // only need the difference
                        var x = (item.x * position - item.lastX) >> 0;
                        var y = (item.y * position - item.lastY) >> 0;

                        // add this to the total scrolling
                        scrollX += x;
                        scrollY += y;

                        // update last values
                        item.lastX += x;
                        item.lastY += y;

                        // delete and step back if it's over
                        if (finished) {
                            que.splice(i, 1); i--;
                        }
                    }

                    // scroll left and top
                    if (scrollWindow) {
                        window.scrollBy(scrollX, scrollY);
                    }
                    else {
                        if (scrollX) elem.scrollLeft += scrollX;
                        if (scrollY) elem.scrollTop += scrollY;
                    }

                    // clean up if there's nothing left to do
                    if (!left && !top) {
                        que = [];
                    }

                    if (que.length) {
                        requestFrame(step, elem, (delay / options.frameRate + 1));
                    } else {
                        pending = false;
                    }
                };

                // start a new queue of actions
                requestFrame(step, elem, 0);
                pending = true;
            }


            /***********************************************
			 * EVENTS
			 ***********************************************/

            /**
			 * Mouse wheel handler.
			 * @param {Object} event
			 */
            function wheel(event) {

                if (!initDone) {
                    init();
                }

                var target = event.target;
                var overflowing = overflowingAncestor(target);

                // use default if there's no overflowing
                // element or default action is prevented
                if (!overflowing || event.defaultPrevented ||
					isNodeName(activeElement, "embed") ||
				   (isNodeName(target, "embed") && /\.pdf/i.test(target.src))) {
                    return true;
                }

                var deltaX = event.wheelDeltaX || 0;
                var deltaY = event.wheelDeltaY || 0;

                // use wheelDelta if deltaX/Y is not available
                if (!deltaX && !deltaY) {
                    deltaY = event.wheelDelta || 0;
                }

                // check if it's a touchpad scroll that should be ignored
                if (!options.touchpadSupport && isTouchpad(deltaY)) {
                    return true;
                }

                // scale by step size
                // delta is 120 most of the time
                // synaptics seems to send 1 sometimes
                if (Math.abs(deltaX) > 1.2) {
                    deltaX *= options.stepSize / 120;
                }
                if (Math.abs(deltaY) > 1.2) {
                    deltaY *= options.stepSize / 120;
                }

                scrollArray(overflowing, -deltaX, -deltaY);
                event.preventDefault();
            }

            /**
			 * Keydown event handler.
			 * @param {Object} event
			 */
            function keydown(event) {

                var target = event.target;
                var modifier = event.ctrlKey || event.altKey || event.metaKey ||
							  (event.shiftKey && event.keyCode !== key.spacebar);

                // do nothing if user is editing text
                // or using a modifier key (except shift)
                // or in a dropdown
                if (/input|textarea|select|embed/i.test(target.nodeName) ||
					 target.isContentEditable ||
					 event.defaultPrevented ||
					 modifier) {
                    return true;
                }
                // spacebar should trigger button press
                if (isNodeName(target, "button") &&
					event.keyCode === key.spacebar) {
                    return true;
                }

                var shift, x = 0, y = 0;
                var elem = overflowingAncestor(activeElement);
                var clientHeight = elem.clientHeight;

                if (elem == document.body) {
                    clientHeight = window.innerHeight;
                }

                switch (event.keyCode) {
                    case key.up:
                        y = -options.arrowScroll;
                        break;
                    case key.down:
                        y = options.arrowScroll;
                        break;
                    case key.spacebar: // (+ shift)
                        shift = event.shiftKey ? 1 : -1;
                        y = -shift * clientHeight * 0.9;
                        break;
                    case key.pageup:
                        y = -clientHeight * 0.9;
                        break;
                    case key.pagedown:
                        y = clientHeight * 0.9;
                        break;
                    case key.home:
                        y = -elem.scrollTop;
                        break;
                    case key.end:
                        var damt = elem.scrollHeight - elem.scrollTop - clientHeight;
                        y = (damt > 0) ? damt + 10 : 0;
                        break;
                    case key.left:
                        x = -options.arrowScroll;
                        break;
                    case key.right:
                        x = options.arrowScroll;
                        break;
                    default:
                        return true; // a key we don't care about
                }

                scrollArray(elem, x, y);
                event.preventDefault();
            }

            /**
			 * Mousedown event only for updating activeElement
			 */
            function mousedown(event) {
                activeElement = event.target;
            }


            /***********************************************
			 * OVERFLOW
			 ***********************************************/

            var cache = {}; // cleared out every once in while
            setInterval(function () { cache = {}; }, 10 * 1000);

            var uniqueID = (function () {
                var i = 0;
                return function (el) {
                    return el.uniqueID || (el.uniqueID = i++);
                };
            })();

            function setCache(elems, overflowing) {
                for (var i = elems.length; i--;)
                    cache[uniqueID(elems[i])] = overflowing;
                return overflowing;
            }

            function overflowingAncestor(el) {
                var elems = [];
                var rootScrollHeight = root.scrollHeight;
                do {
                    var cached = cache[uniqueID(el)];
                    if (cached) {
                        return setCache(elems, cached);
                    }
                    elems.push(el);
                    if (rootScrollHeight === el.scrollHeight) {
                        if (!isFrame || root.clientHeight + 10 < rootScrollHeight) {
                            return setCache(elems, document.body); // scrolling root in WebKit
                        }
                    } else if (el.clientHeight + 10 < el.scrollHeight) {
                        overflow = getComputedStyle(el, "").getPropertyValue("overflow-y");
                        if (overflow === "scroll" || overflow === "auto") {
                            return setCache(elems, el);
                        }
                    }
                } while (el = el.parentNode);
            }


            /***********************************************
			 * HELPERS
			 ***********************************************/

            function addEvent(type, fn, bubble) {
                window.addEventListener(type, fn, (bubble || false));
            }

            function removeEvent(type, fn, bubble) {
                window.removeEventListener(type, fn, (bubble || false));
            }

            function isNodeName(el, tag) {
                return (el.nodeName || "").toLowerCase() === tag.toLowerCase();
            }

            function directionCheck(x, y) {
                x = (x > 0) ? 1 : -1;
                y = (y > 0) ? 1 : -1;
                if (direction.x !== x || direction.y !== y) {
                    direction.x = x;
                    direction.y = y;
                    que = [];
                    lastScroll = 0;
                }
            }

            var deltaBufferTimer;

            function isTouchpad(deltaY) {
                if (!deltaY) return;
                deltaY = Math.abs(deltaY)
                deltaBuffer.push(deltaY);
                deltaBuffer.shift();
                clearTimeout(deltaBufferTimer);
                var allEquals = (deltaBuffer[0] == deltaBuffer[1] &&
									deltaBuffer[1] == deltaBuffer[2]);
                var allDivisable = (isDivisible(deltaBuffer[0], 120) &&
									isDivisible(deltaBuffer[1], 120) &&
									isDivisible(deltaBuffer[2], 120));
                return !(allEquals || allDivisable);
            }

            function isDivisible(n, divisor) {
                return (Math.floor(n / divisor) == n / divisor);
            }

            var requestFrame = (function () {
                return window.requestAnimationFrame ||
                        window.webkitRequestAnimationFrame ||
                        function (callback, element, delay) {
                            window.setTimeout(callback, delay || (1000 / 60));
                        };
            })();

            var MutationObserver = window.MutationObserver || window.WebKitMutationObserver;


            /***********************************************
			 * PULSE
			 ***********************************************/

            /**
			 * Viscous fluid with a pulse for part and decay for the rest.
			 * - Applies a fixed force over an interval (a damped acceleration), and
			 * - Lets the exponential bleed away the velocity over a longer interval
			 * - Michael Herf, http://stereopsis.com/stopping/
			 */
            function pulse_(x) {
                var val, start, expx;
                // test
                x = x * options.pulseScale;
                if (x < 1) { // acceleartion
                    val = x - (1 - Math.exp(-x));
                } else {     // tail
                    // the previous animation ended here:
                    start = Math.exp(-1);
                    // simple viscous drag
                    x -= 1;
                    expx = 1 - Math.exp(-x);
                    val = start + (expx * (1 - start));
                }
                return val * options.pulseNormalize;
            }

            function pulse(x) {
                if (x >= 1) return 1;
                if (x <= 0) return 0;

                if (options.pulseNormalize == 1) {
                    options.pulseNormalize /= pulse_(1);
                }
                return pulse_(x);
            }

            addEvent("mousedown", mousedown);
            addEvent("mousewheel", wheel);
            addEvent("load", init);

        }

    });
})(jQuery);

/** Appear
	https://github.com/bas2k/jquery.appear/
 **************************************************************** **/
(function (a) { a.fn.appear = function (d, b) { var c = a.extend({ data: undefined, one: true, accX: 0, accY: 0 }, b); return this.each(function () { var g = a(this); g.appeared = false; if (!d) { g.trigger("appear", c.data); return } var f = a(window); var e = function () { if (!g.is(":visible")) { g.appeared = false; return } var r = f.scrollLeft(); var q = f.scrollTop(); var l = g.offset(); var s = l.left; var p = l.top; var i = c.accX; var t = c.accY; var k = g.height(); var j = f.height(); var n = g.width(); var m = f.width(); if (p + k + t >= q && p <= q + j + t && s + n + i >= r && s <= r + m + i) { if (!g.appeared) { g.trigger("appear", c.data) } } else { g.appeared = false } }; var h = function () { g.appeared = true; if (c.one) { f.unbind("scroll", e); var j = a.inArray(e, a.fn.appear.checks); if (j >= 0) { a.fn.appear.checks.splice(j, 1) } } d.apply(this, arguments) }; if (c.one) { g.one("appear", c.data, h) } else { g.bind("appear", c.data, h) } f.scroll(e); a.fn.appear.checks.push(e); (e)() }) }; a.extend(a.fn.appear, { checks: [], timeout: null, checkAll: function () { var b = a.fn.appear.checks.length; if (b > 0) { while (b--) { if (a.fn.appear.checks[b]) { (a.fn.appear.checks[b])() } } } }, run: function () { if (a.fn.appear.timeout) { clearTimeout(a.fn.appear.timeout) } a.fn.appear.timeout = setTimeout(a.fn.appear.checkAll, 20) } }); a.each(["append", "prepend", "after", "before", "attr", "removeAttr", "addClass", "removeClass", "toggleClass", "remove", "css", "show", "hide"], function (c, d) { var b = a.fn[d]; if (b) { a.fn[d] = function () { var e = b.apply(this, arguments); a.fn.appear.run(); return e } } }) })(jQuery);

/** jQuery Easing v1.3
	http://gsgd.co.uk/sandbox/jquery/easing/
 **************************************************************** **/
jQuery.easing.jswing = jQuery.easing.swing; jQuery.extend(jQuery.easing, { def: "easeOutQuad", swing: function (e, f, a, h, g) { return jQuery.easing[jQuery.easing.def](e, f, a, h, g) }, easeInQuad: function (e, f, a, h, g) { return h * (f /= g) * f + a }, easeOutQuad: function (e, f, a, h, g) { return -h * (f /= g) * (f - 2) + a }, easeInOutQuad: function (e, f, a, h, g) { if ((f /= g / 2) < 1) { return h / 2 * f * f + a } return -h / 2 * ((--f) * (f - 2) - 1) + a }, easeInCubic: function (e, f, a, h, g) { return h * (f /= g) * f * f + a }, easeOutCubic: function (e, f, a, h, g) { return h * ((f = f / g - 1) * f * f + 1) + a }, easeInOutCubic: function (e, f, a, h, g) { if ((f /= g / 2) < 1) { return h / 2 * f * f * f + a } return h / 2 * ((f -= 2) * f * f + 2) + a }, easeInQuart: function (e, f, a, h, g) { return h * (f /= g) * f * f * f + a }, easeOutQuart: function (e, f, a, h, g) { return -h * ((f = f / g - 1) * f * f * f - 1) + a }, easeInOutQuart: function (e, f, a, h, g) { if ((f /= g / 2) < 1) { return h / 2 * f * f * f * f + a } return -h / 2 * ((f -= 2) * f * f * f - 2) + a }, easeInQuint: function (e, f, a, h, g) { return h * (f /= g) * f * f * f * f + a }, easeOutQuint: function (e, f, a, h, g) { return h * ((f = f / g - 1) * f * f * f * f + 1) + a }, easeInOutQuint: function (e, f, a, h, g) { if ((f /= g / 2) < 1) { return h / 2 * f * f * f * f * f + a } return h / 2 * ((f -= 2) * f * f * f * f + 2) + a }, easeInSine: function (e, f, a, h, g) { return -h * Math.cos(f / g * (Math.PI / 2)) + h + a }, easeOutSine: function (e, f, a, h, g) { return h * Math.sin(f / g * (Math.PI / 2)) + a }, easeInOutSine: function (e, f, a, h, g) { return -h / 2 * (Math.cos(Math.PI * f / g) - 1) + a }, easeInExpo: function (e, f, a, h, g) { return (f == 0) ? a : h * Math.pow(2, 10 * (f / g - 1)) + a }, easeOutExpo: function (e, f, a, h, g) { return (f == g) ? a + h : h * (-Math.pow(2, -10 * f / g) + 1) + a }, easeInOutExpo: function (e, f, a, h, g) { if (f == 0) { return a } if (f == g) { return a + h } if ((f /= g / 2) < 1) { return h / 2 * Math.pow(2, 10 * (f - 1)) + a } return h / 2 * (-Math.pow(2, -10 * --f) + 2) + a }, easeInCirc: function (e, f, a, h, g) { return -h * (Math.sqrt(1 - (f /= g) * f) - 1) + a }, easeOutCirc: function (e, f, a, h, g) { return h * Math.sqrt(1 - (f = f / g - 1) * f) + a }, easeInOutCirc: function (e, f, a, h, g) { if ((f /= g / 2) < 1) { return -h / 2 * (Math.sqrt(1 - f * f) - 1) + a } return h / 2 * (Math.sqrt(1 - (f -= 2) * f) + 1) + a }, easeInElastic: function (f, h, e, l, k) { var i = 1.70158; var j = 0; var g = l; if (h == 0) { return e } if ((h /= k) == 1) { return e + l } if (!j) { j = k * 0.3 } if (g < Math.abs(l)) { g = l; var i = j / 4 } else { var i = j / (2 * Math.PI) * Math.asin(l / g) } return -(g * Math.pow(2, 10 * (h -= 1)) * Math.sin((h * k - i) * (2 * Math.PI) / j)) + e }, easeOutElastic: function (f, h, e, l, k) { var i = 1.70158; var j = 0; var g = l; if (h == 0) { return e } if ((h /= k) == 1) { return e + l } if (!j) { j = k * 0.3 } if (g < Math.abs(l)) { g = l; var i = j / 4 } else { var i = j / (2 * Math.PI) * Math.asin(l / g) } return g * Math.pow(2, -10 * h) * Math.sin((h * k - i) * (2 * Math.PI) / j) + l + e }, easeInOutElastic: function (f, h, e, l, k) { var i = 1.70158; var j = 0; var g = l; if (h == 0) { return e } if ((h /= k / 2) == 2) { return e + l } if (!j) { j = k * (0.3 * 1.5) } if (g < Math.abs(l)) { g = l; var i = j / 4 } else { var i = j / (2 * Math.PI) * Math.asin(l / g) } if (h < 1) { return -0.5 * (g * Math.pow(2, 10 * (h -= 1)) * Math.sin((h * k - i) * (2 * Math.PI) / j)) + e } return g * Math.pow(2, -10 * (h -= 1)) * Math.sin((h * k - i) * (2 * Math.PI) / j) * 0.5 + l + e }, easeInBack: function (e, f, a, i, h, g) { if (g == undefined) { g = 1.70158 } return i * (f /= h) * f * ((g + 1) * f - g) + a }, easeOutBack: function (e, f, a, i, h, g) { if (g == undefined) { g = 1.70158 } return i * ((f = f / h - 1) * f * ((g + 1) * f + g) + 1) + a }, easeInOutBack: function (e, f, a, i, h, g) { if (g == undefined) { g = 1.70158 } if ((f /= h / 2) < 1) { return i / 2 * (f * f * (((g *= (1.525)) + 1) * f - g)) + a } return i / 2 * ((f -= 2) * f * (((g *= (1.525)) + 1) * f + g) + 2) + a }, easeInBounce: function (e, f, a, h, g) { return h - jQuery.easing.easeOutBounce(e, g - f, 0, h, g) + a }, easeOutBounce: function (e, f, a, h, g) { if ((f /= g) < (1 / 2.75)) { return h * (7.5625 * f * f) + a } else { if (f < (2 / 2.75)) { return h * (7.5625 * (f -= (1.5 / 2.75)) * f + 0.75) + a } else { if (f < (2.5 / 2.75)) { return h * (7.5625 * (f -= (2.25 / 2.75)) * f + 0.9375) + a } else { return h * (7.5625 * (f -= (2.625 / 2.75)) * f + 0.984375) + a } } } }, easeInOutBounce: function (e, f, a, h, g) { if (f < g / 2) { return jQuery.easing.easeInBounce(e, f * 2, 0, h, g) * 0.5 + a } return jQuery.easing.easeOutBounce(e, f * 2 - g, 0, h, g) * 0.5 + h * 0.5 + a } });

/** Backstretch v2.0.4
	http://srobbin.com/jquery-plugins/backstretch/
 **************************************************************** **/
(function (a, d, p) { a.fn.backstretch = function (c, b) { (c === p || 0 === c.length) && a.error("No images were supplied for Backstretch"); 0 === a(d).scrollTop() && d.scrollTo(0, 0); return this.each(function () { var d = a(this), g = d.data("backstretch"); if (g) { if ("string" == typeof c && "function" == typeof g[c]) { g[c](b); return } b = a.extend(g.options, b); g.destroy(!0) } g = new q(this, c, b); d.data("backstretch", g) }) }; a.backstretch = function (c, b) { return a("body").backstretch(c, b).data("backstretch") }; a.expr[":"].backstretch = function (c) { return a(c).data("backstretch") !== p }; a.fn.backstretch.defaults = { centeredX: !0, centeredY: !0, duration: 5E3, fade: 0 }; var r = { left: 0, top: 0, overflow: "hidden", margin: 0, padding: 0, height: "100%", width: "100%", zIndex: -999999 }, s = { position: "absolute", display: "none", margin: 0, padding: 0, border: "none", width: "auto", height: "auto", maxHeight: "none", maxWidth: "none", zIndex: -999999 }, q = function (c, b, e) { this.options = a.extend({}, a.fn.backstretch.defaults, e || {}); this.images = a.isArray(b) ? b : [b]; a.each(this.images, function () { a("<img />")[0].src = this }); this.isBody = c === document.body; this.$container = a(c); this.$root = this.isBody ? l ? a(d) : a(document) : this.$container; c = this.$container.children(".backstretch").first(); this.$wrap = c.length ? c : a('<div class="backstretch"></div>').css(r).appendTo(this.$container); this.isBody || (c = this.$container.css("position"), b = this.$container.css("zIndex"), this.$container.css({ position: "static" === c ? "relative" : c, zIndex: "auto" === b ? 0 : b, background: "none" }), this.$wrap.css({ zIndex: -999998 })); this.$wrap.css({ position: this.isBody && l ? "fixed" : "absolute" }); this.index = 0; this.show(this.index); a(d).on("resize.backstretch", a.proxy(this.resize, this)).on("orientationchange.backstretch", a.proxy(function () { this.isBody && 0 === d.pageYOffset && (d.scrollTo(0, 1), this.resize()) }, this)) }; q.prototype = { resize: function () { try { var a = { left: 0, top: 0 }, b = this.isBody ? this.$root.width() : this.$root.innerWidth(), e = b, g = this.isBody ? d.innerHeight ? d.innerHeight : this.$root.height() : this.$root.innerHeight(), j = e / this.$img.data("ratio"), f; j >= g ? (f = (j - g) / 2, this.options.centeredY && (a.top = "-" + f + "px")) : (j = g, e = j * this.$img.data("ratio"), f = (e - b) / 2, this.options.centeredX && (a.left = "-" + f + "px")); this.$wrap.css({ width: b, height: g }).find("img:not(.deleteable)").css({ width: e, height: j }).css(a) } catch (h) { } return this }, show: function (c) { if (!(Math.abs(c) > this.images.length - 1)) { var b = this, e = b.$wrap.find("img").addClass("deleteable"), d = { relatedTarget: b.$container[0] }; b.$container.trigger(a.Event("backstretch.before", d), [b, c]); this.index = c; clearInterval(b.interval); b.$img = a("<img />").css(s).bind("load", function (f) { var h = this.width || a(f.target).width(); f = this.height || a(f.target).height(); a(this).data("ratio", h / f); a(this).fadeIn(b.options.speed || b.options.fade, function () { e.remove(); b.paused || b.cycle(); a(["after", "show"]).each(function () { b.$container.trigger(a.Event("backstretch." + this, d), [b, c]) }) }); b.resize() }).appendTo(b.$wrap); b.$img.attr("src", b.images[c]); return b } }, next: function () { return this.show(this.index < this.images.length - 1 ? this.index + 1 : 0) }, prev: function () { return this.show(0 === this.index ? this.images.length - 1 : this.index - 1) }, pause: function () { this.paused = !0; return this }, resume: function () { this.paused = !1; this.next(); return this }, cycle: function () { 1 < this.images.length && (clearInterval(this.interval), this.interval = setInterval(a.proxy(function () { this.paused || this.next() }, this), this.options.duration)); return this }, destroy: function (c) { a(d).off("resize.backstretch orientationchange.backstretch"); clearInterval(this.interval); c || this.$wrap.remove(); this.$container.removeData("backstretch") } }; var l, f = navigator.userAgent, m = navigator.platform, e = f.match(/AppleWebKit\/([0-9]+)/), e = !!e && e[1], h = f.match(/Fennec\/([0-9]+)/), h = !!h && h[1], n = f.match(/Opera Mobi\/([0-9]+)/), t = !!n && n[1], k = f.match(/MSIE ([0-9]+)/), k = !!k && k[1]; l = !((-1 < m.indexOf("iPhone") || -1 < m.indexOf("iPad") || -1 < m.indexOf("iPod")) && e && 534 > e || d.operamini && "[object OperaMini]" === {}.toString.call(d.operamini) || n && 7458 > t || -1 < f.indexOf("Android") && e && 533 > e || h && 6 > h || "palmGetResource" in d && e && 534 > e || -1 < f.indexOf("MeeGo") && -1 < f.indexOf("NokiaBrowser/8.5.0") || k && 6 >= k) })(jQuery, window);