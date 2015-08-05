/// <vs BeforeBuild='watch' />
// Include plug-ins
var gulp = require('gulp'),
	jshint = require('gulp-jshint'),
	concat = require('gulp-concat'),
	minifyCSS = require('gulp-minify-css'),
	autoprefixer = require('gulp-autoprefixer'),
	uglify = require('gulp-uglify'),
	rename = require('gulp-rename'),
	inject = require('gulp-inject'),
	minifyHTML = require('gulp-minify-html'),
	del = require('del'),
	angularFilesort = require('gulp-angular-filesort');
 
 // File paths
var config = {
    // Include all js files but exclude any min.js files
	appJsSrc : ['app/**/*.js', '!app/**/*.min.js'],
	appCssSrc: ['content/toastr.css', 'content/main.css'],
	appIndexHtml: 'index-template.html'
}

// For browser caching
var getStamp = function() {
  var myDate = new Date();

  var myYear = myDate.getFullYear().toString();
  var myMonth = ('0' + (myDate.getMonth() + 1)).slice(-2);
  var myDay = ('0' + myDate.getDate()).slice(-2);
  var mySeconds = myDate.getSeconds().toString();

  var myFullDate = myYear + myMonth + myDay + mySeconds;

  return myFullDate;
};

 // Minify, prefix and contat CSS
gulp.task('css', function(){
	del.sync(['app/build/allcss*']);

    return gulp.src(config.appCssSrc)
		// .pipe(minifyCSS())
		.pipe(autoprefixer('last 2 version', 'safari 5', 'ie 8', 'ie 9'))
		.pipe(concat('allcss' + getStamp() + '.min.css', {newLine: ''}))
		.pipe(concat('allcss.min.css', {newLine: ''}))
		.pipe(gulp.dest('app/build'))
});
 
// Lint Task
gulp.task('lint', function() {
    return gulp.src(config.appJsSrc)
        .pipe(jshint())
        .pipe(jshint.reporter('default'));
});

// Combine and minify all JS files from the app folder
gulp.task('scripts', function() {
	del.sync(['app/build/alljs*']);
	
	return gulp.src(config.appJsSrc)
		.pipe(angularFilesort())
		// .pipe(uglify())
		.pipe(concat('alljs' + getStamp() + '.min.js', {newLine: ''}))
		.pipe(concat('alljs.min.js', {newLine: ''}))
		.pipe(gulp.dest('app/build'));
});

// Inject minified files into new HTML
gulp.task('html', ['css', 'scripts'], function () {
    del.sync(['index.html']);
	var target = gulp.src(config.appIndexHtml);
	var sources = gulp.src(['app/build/alljs*', 'app/build/allcss*']);
	
	return target.pipe(inject(sources))
		.pipe(minifyHTML({ conditionals: true }))
		.pipe(rename('index.html'))
		.pipe(gulp.dest('./'));
});

// Watch for changes
gulp.task('watch', ['lint', 'css', 'scripts', 'html'], function () {
	gulp.watch(config.appCssSrc, ['css', 'html']);
	gulp.watch(config.appJsSrc, ['lint', 'scripts', 'html']);
	gulp.watch(config.appIndexHtml, ['html']);
});
 
// Set  default tasks
gulp.task('default', ['lint', 'css', 'scripts', 'html'], function(){});