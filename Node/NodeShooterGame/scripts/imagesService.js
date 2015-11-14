define([], function () {
	var service = {
		loadImage: function () {
			var preload = new createjs.LoadQueue();
			preload.addEventListener("fileload", this.handleFileComplete);
			preload.loadFile("nuke.png");
		},

		handleFileComplete: function (event) {
			document.body.appendChild(event.result);
		},
	}
	return service;
});