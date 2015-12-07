/* jslint node: true */
/* global describe, it, expect */

"use strict";

var gutil = require('gulp-util');
var servicesModule = require('../../../frontend/typescript/services/ServicesModule').ServicesModule;

describe("#ServicesModule", function () {
  it("Services module can create new images service.", function () {
    var imagesService = servicesModule.ImagesService();
    expect(imagesService != null).toBe(true);
  });

  it("If images were not loaded count iss always zero.", function () {
    var imagesService = servicesModule.ImagesService();
    expect(imagesService.getCount()).toBe(0);
  });

  it("Get count returns number of images.", function () {
    var imagesService = servicesModule.ImagesService();
    imagesService.loadImages();
    expect(imagesService.getCount()).toBe(1);
  });

  it("Image service contains ship image.", function () {
    var imagesService = servicesModule.ImagesService();
    imagesService.loadImages();
    expect(imagesService.getImage("ship1.png")).not.toBe(null);
    expect(imagesService.getImage("ship1.png")).toBe("ship1.png");
  });
});