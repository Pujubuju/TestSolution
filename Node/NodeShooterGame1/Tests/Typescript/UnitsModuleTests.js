/* jslint node: true */
/* global describe, it, expect */

"use strict";

var gutil = require('gulp-util');
var unitsModule = require('../../frontend/typescript/unitsmodule').UnitsModule;

describe("#UnitsModule", function () {
  it("Units module can create new knight.", function () {
    var knight = unitsModule.Knight(100, 20);
    expect(knight.isAlive()).toBe(true);
  });
  it("Knight with hp greater than 0 is alive.", function () {
    var knight = unitsModule.Knight(100, 20);
    expect(knight.isAlive()).toBe(true);
  });
  it("Knight with hp equal 0 is dead.", function () {
    var knight = unitsModule.Knight(0, 20);
    expect(knight.isAlive()).toBe(false);
  });
  it("Knight with hp less than 0 is dead.", function () {
    var knight = unitsModule.Knight(-1, 20);
    expect(knight.isAlive()).toBe(false);
  });
});