var imagesService = require("./ImagesService");
var ServicesModule;
(function (ServicesModule) {
    ServicesModule.ImagesService = function () {
        return new imagesService.ImagesService();
    };
})(ServicesModule = exports.ServicesModule || (exports.ServicesModule = {}));
