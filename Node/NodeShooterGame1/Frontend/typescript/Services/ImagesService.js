var nameImagePair = require("../Models/NameImagePair");
var ImagesService = (function () {
    function ImagesService() {
        this.images = new Array();
    }
    ImagesService.prototype.loadImages = function () {
        var item = new nameImagePair.NameImagePair("ship1.png", "ship1.png");
        this.images.push(item);
    };
    ImagesService.prototype.getImage = function (name) {
        for (var index = 0; index < this.images.length; index++) {
            var item = this.images[index];
            if (item.name == name) {
                return item.img;
            }
        }
        return null;
    };
    ImagesService.prototype.getCount = function () {
        return this.images.length;
    };
    return ImagesService;
})();
exports.ImagesService = ImagesService;
