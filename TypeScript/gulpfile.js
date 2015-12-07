"use strict";

// get packages
var gulp = require('gulp');
var gutil = require('gulp-util');
var jasmine = require('gulp-jasmine');
var ts = require('gulp-typescript');
var gulpSequence = require('gulp-sequence');

// get typescript configuration
var tsProject = ts.createProject('tsconfig.json');

// default gulp task
gulp.task('default', function () {
  return gutil.log('Gulp is running!');
});

// build and run tests sequence
gulp.task('Build solution and run tests', 
gulpSequence('Build solution', 'Run tests'));

// buld task
gulp.task('Build solution', function () {
  var tsResult = tsProject.src().pipe(ts(tsProject));
  return tsResult.js.pipe(gulp.dest('build'));
});

// run tests task
gulp.task('Run tests', function () {
    return gulp.src(['tests/*.js', 'tests/typescript/*.js'])
        .pipe(jasmine());
});
