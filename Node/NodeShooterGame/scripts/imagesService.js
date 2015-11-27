function ImagesService() {	
	var _self = this;
	_self.images = new Array(),
	_self.loadQueue = new createjs.LoadQueue();

	_self.loadImages = function () {
		_self.loadQueue.addEventListener("fileload", _self.handleFileComplete);
		_self.loadQueue.loadFile("nuke.png");
		_self.loadQueue.loadFile("resources/ship1.png");
	}

	_self.handleFileComplete = function (event) {
		var item = new Object();
		var itemSplitted = event.item.src.split("/");
		item.name = itemSplitted[itemSplitted.length-1];
		item.img = event.result;
		_self.images.push(item);
	}

	_self.getImage = function (name){
		for (var index = 0; index < _self.images.length; index++) {
			var item = _self.images[index];
			if(item.name == name){
				return item.img;
			}
		}
		return null;
	}
}

define([],
	function () {
		return new ImagesService();
	});