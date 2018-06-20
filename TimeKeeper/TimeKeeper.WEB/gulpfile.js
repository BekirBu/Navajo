var gulp = require('gulp');
var jshint = require('gulp-jshint');
var concat = require('gulp-concat');
var clean = require('gulp-clean-css');
var rename = require('gulp-rename');
var minify = require('gulp-minify');
var uglify = require('gulp-uglify');
var template = require('gulp-angular-templatecache');
var gutil = require('gulp-util');
var browserSync = require('browser-sync').create();
var browserify = require('browserify');
var merge = require('utils-merge');

var source = ["scripts/*.js", "scripts/**/*.js", "styles/bootstrap.min.js"];
var library = [
    'library/angular.min.js',
    'library/angular-route.min.js',
    "library/angular-animate.min.js",
    "library/jquery-3.2.1.min.js",
    "library/material-dashboard.js",
    "library/material.min.js",
    "library/bootstrap.min.js",
    "library/ui-bootstrap-tpls.min.js",
    "library/angular*base64-upload.js",
    "library/toaster.min.js",
    "library/sweetalert.min.js",
    "library/Chart.js",
    "library/angular-chart.min.js"
];
var html = 'views/*.html';
var style = ['styles/*.min.css', 'styles/*.css'];
var imgs = 'images/*';

gulp.task('jshint', function() {
    gulp.src(source)
        .pipe(jshint())
        .pipe(jshint.reporter('gulp-jshint-file-reporter', { filename: './jshint-output.log' }));
});

gulp.task('lib', function(){
    return gulp.src(library)
        .pipe(concat('lib.min.js'))
        .pipe(gulp.dest('dist'));
});

gulp.task('css', function(){
    return gulp.src(style)
        .pipe(concat('app.css'))
        .pipe(clean('app.css'))
        .pipe(rename('app.min.css'))
        .pipe(gulp.dest('dist'));
});

gulp.task('app', function() {
    return gulp.src(source)
        .pipe(concat('app.js'))
        .pipe(uglify())
        .pipe(rename('app.min.js'))
        .pipe(gulp.dest('dist'));
});

gulp.task('html', function () {
    return gulp
        .src(html)
        .pipe(template('template.js', { module: 'timeKeeper', root: 'views/' }))
        .pipe(gulp.dest('dist'));
});

gulp.task('imgs', function () {
    return gulp
        .src(imgs)
        .pipe(gulp.dest('dist'))
});
/*
gulp.task('browse', function(){
    browserSync.init({
        server: {
            baseDir: "dist"
        },
        browser: "chrome"
    });
});
*/

gulp.task('browse', function(){
    browserSync.init({
        server: {
            baseDir: "dist",
            index: "index.html"
        },
        port: 3001,
        browser: "chrome"
    });
});

gulp.task('default', function(){
    browserSync.init({
        server: {
            baseDir: "./",
            index: "index.html"
        },
        port: 3001,
        browser:  "chrome"
    });
});

gulp.task('otvori', function(){
    browserSync.init({
        server: {
            baseDir: "./",
            index: "index.html"
        },
        port: 3001,
        browser: "chrome"
    });
});

gulp.task('update', ['app', 'lib', 'css', 'html', 'imgs']);
gulp.task('runAll', ['app', 'lib', 'css', 'html', 'imgs', 'browse']);