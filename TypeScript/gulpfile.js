/* File: gulpfile.js */

// grab our gulp packages
var gulp = require('gulp');
var gutil = require('gulp-util');

// typescript compilation
var ts = require('gulp-typescript');
var tsProject = ts.createProject('tsconfig.json');

// create a default task and just log a message
gulp.task('default', function () {
  return gutil.log('Gulp is running!');
});

// build task
gulp.task('Build solution', function () {
  var tsResult = tsProject.src().pipe(ts(tsProject));
  return tsResult.js.pipe(gulp.dest('built'));
});