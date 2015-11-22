function ImagesService() {	
	var _self = this;
	this.images = new Array(),
	this.loadQueue = new createjs.LoadQueue();

	this.loadImages = function () {
		this.loadQueue.addEventListener("fileload", this.handleFileComplete);
		this.loadQueue.loadFile("nuke.png");
		this.loadQueue.loadFile("ship1.png");
	}

	this.handleFileComplete = function (event) {
		// document.body.appendChild(event.result);
		var item = new Object();
		item.name = event.item.src;
		item.img = event.result;
		_self.images.push(item);
	}

	this.getImage = function (name){
		for (var index = 0; index < this.images.length; index++) {
			var item = this.images[index];
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