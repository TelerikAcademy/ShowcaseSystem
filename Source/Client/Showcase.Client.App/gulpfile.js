/// <vs />
// Include plug-ins
var isProduction = require('yargs').argv.env === 'production',
	gulp = require('gulp'),
	gulpIf = require('gulp-if'),
	jshint = require('gulp-jshint'),
	concat = require('gulp-concat'),
	minifyCSS = require('gulp-minify-css'),
	autoprefixer = require('gulp-autoprefixer'),
	uglify = require('gulp-uglify'),
	rename = require('gulp-rename'),
	inject = require('gulp-inject'),
	minifyHTML = require('gulp-minify-html'),
	del = require('del'),
    addStream = require('add-stream'),
	angularFilesort = require('gulp-angular-filesort'),
	angularTemplateCache = require('gulp-angular-templatecache');
console.log(isProduction);

// File paths
var config = {
    vendorJsSrc: [
        'scripts/epona/plugins/jquery-2.1.3.min.js',
        'scripts/epona/plugins/bootstrap/js/bootstrap.min.js',
        'scripts/epona/plugins/magnific-popup/jquery.magnific-popup.min.js',
        'scripts/epona/plugins/owl-carousel/owl.carousel.min.js',
        'scripts/epona/plugins/layerslider/js/layerslider_pack.js',
        'scripts/epona/js/scripts.js',
        'scripts/toastr.js',
        'scripts/angular.js',
        'scripts/angular-route.js',
        'scripts/angular-cookies.js',
        'scripts/angular-animate.js',
        'scripts/loading-bar.js',
        'scripts/jquery.tokeninput.js',
        'scripts/Chart.js',
        'scripts/ng-infinite-scroll.min.js',
        'Scripts/angular-ui/ui-bootstrap-tpls.js',
        'scripts/sweetalert.min.js',
        'scripts/ngSweetAlert.js',
        'scripts/bootstrap-select.min.js'
    ],
    vendorCssSrc: [
        'content/epona/css/font-awesome.css',
        'content/epona/css/sky-forms.css',
        'scripts/epona/plugins/owl-carousel/owl.pack.css',
        'scripts/epona/plugins/magnific-popup/magnific-popup.css',
        'content/epona/css/animate.css',
        'content/epona/css/layerslider.css',
        'content/epona/css/essentials.css',
        'content/epona/css/layout.css',
        'content/epona/css/header-4.css',
        'content/epona/css/footer-default.css',
        'content/epona/css/color_scheme/green.css',
        'content/loading-bar.css',
        'content/token-input-showcase.css',
        'content/toastr.css',
        'content/sweetalert.css',
        'content/bootstrap-select.css'
    ],
    appJsSrc: ['app/**/*.js', '!app/build/*'],
    appCssSrc: ['content/main.css'],
    appTemplatesHtml: 'app/**/*.html',
    appIndexHtml: 'index-template.html'
}

// For browser caching
var getStamp = function () {
    var myDate = new Date();

    var myYear = myDate.getFullYear().toString();
    var myMonth = ('0' + (myDate.getMonth() + 1)).slice(-2);
    var myDay = ('0' + myDate.getDate()).slice(-2);
    var mySeconds = myDate.getSeconds().toString();

    var myFullDate = myYear + myMonth + myDay + mySeconds;

    return myFullDate;
};

// For angular templates
var prepareTemplates = function () {
    return gulp.src(config.appTemplatesHtml)
        // .pipe(gulpIf(isProduction, minifyHTML({ conditionals: true, empty: true }))) // TODO: minifier is making problems with Angular templates, test with other
        .pipe(angularTemplateCache());
};

// Minify, prefix and contat CSS
gulp.task('css', function () {
    del.sync(['app/build/allcss*']);

    var allCss = config.vendorCssSrc.concat(config.appCssSrc);

    return gulp.src(allCss)
		.pipe(gulpIf(isProduction, minifyCSS()))
		.pipe(autoprefixer('last 2 version', 'safari 5', 'ie 8', 'ie 9'))
		.pipe(concat('allcss' + (isProduction ? getStamp() : '') + '.min.css', { newLine: '' }))
		.pipe(gulp.dest('app/build'))
});

// Lint Task
gulp.task('lint', function () {
    return gulp.src(config.appJsSrc)
        .pipe(jshint())
        .pipe(jshint.reporter('default'));
});

// Combine and minify all library JS files
gulp.task('vendors', function () {
    del.sync(['app/build/vendorjs*']);

    return gulp.src(config.vendorJsSrc)
		.pipe(gulpIf(isProduction, uglify()))
		.pipe(concat('vendorjs' + (isProduction ? getStamp() : '') + '.min.js', { newLine: '' }))
		.pipe(gulp.dest('app/build'));
});

// Combine and minify all JS files from the app folder
gulp.task('scripts', function () {
    del.sync(['app/build/alljs*']);

    return gulp.src(config.appJsSrc)
		.pipe(angularFilesort())
		.pipe(gulpIf(isProduction, uglify()))
        .pipe(addStream.obj(prepareTemplates()))
		.pipe(concat('alljs' + (isProduction ? getStamp() : '') + '.min.js', { newLine: '' }))
		.pipe(gulp.dest('app/build'));
});

// Inject minified files into new HTML
gulp.task('html', ['css', 'scripts'], function () {
    del.sync(['index.html']);
    var target = gulp.src(config.appIndexHtml);
    var vendorSources = gulp.src(['app/build/vendorjs*'], { read: false });
    var appSources = gulp.src(['app/build/alljs*', 'app/build/allcss*'], { read: false });

    return target
        .pipe(inject(vendorSources, { starttag: '<!-- inject:vendors:{{ext}} -->' }))
        .pipe(inject(appSources))
		.pipe(minifyHTML({ conditionals: true }))
		.pipe(rename('index.html'))
		.pipe(gulp.dest('./'));
});

// Watch for changes
gulp.task('watch', ['lint', 'css', 'vendors', 'scripts', 'html'], function () {
    gulp.watch(config.appCssSrc, ['css', 'html']);
    gulp.watch(config.appJsSrc, ['lint', 'scripts', 'html']);
    gulp.watch(config.appTemplatesHtml, ['lint', 'scripts', 'html']);
    gulp.watch(config.appIndexHtml, ['html']);
});

// Set  default tasks
gulp.task('default', ['lint', 'css', 'vendors', 'scripts', 'html'], function () { });