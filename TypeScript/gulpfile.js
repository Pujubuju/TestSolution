"use strict";

var gulp = require('gulp');
var gutil = require('gulp-util');
var jasmine = require('gulp-jasmine');
var ts = require('gulp-typescript');
var tsProject = ts.createProject('tsconfig.json');
var runSequence = require('run-sequence');
var gulpSequence = require('gulp-sequence');

gulp.task('default', function () {
  return gutil.log('Gulp is running!');
});

gulp.task('Build solution and run tests', 
gulpSequence('Build solution', 'Run tests'));

gulp.task('Build solution', function () {
  var tsResult = tsProject.src().pipe(ts(tsProject));
  return tsResult.js.pipe(gulp.dest('build'));
});

gulp.task('Run tests', function () {
    return gulp.src(['tests/*.js', 'tests/typescript/*.js'])
        .pipe(jasmine());
});
