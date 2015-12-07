import imagesService = require("./ImagesService");

export module ServicesModule {    
    export var ImagesService = function(){
            return new imagesService.ImagesService();
        }    
}