
define(['soundsService', 'imagesService'], function (soundsService, imagesService) {

	var game = {

		imagesService : imagesService,
		soundsService : soundsService,

		playSound: function () {
			soundsService.playSound();
		},

		loadImage: function () {
			imagesService.loadImage();
		},

		regiterKeyEvents: function () {
			document.addEventListener('keydown', function (event) {

				var wCode = 87;
				var aCode = 65;
				var sCode = 83;
				var dCode = 68;
				var stepSize = 50;

				if (event.keyCode == wCode) {
					player.y = player.y - stepSize;
				}
				else if (event.keyCode == aCode) {
					player.x = player.x - stepSize;
				}
				else if (event.keyCode == sCode) {
					player.y = player.y + stepSize;
				}
				else if (event.keyCode == dCode) {
					player.x = player.x + stepSize;
				}
			});
		},

		player: {},

		init: function () {

			var stage = new createjs.Stage("demoCanvas");
			var circle = new createjs.Shape();
			circle.graphics.beginFill("DeepSkyBlue").drawCircle(0, 0, 50);
			circle.x = 100;
			circle.y = 100;
			stage.addChild(circle);

			player = new createjs.Shape();
			player.graphics.beginFill("Red").drawCircle(0, 0, 50);
			player.x = 100;
			player.y = 100;
			stage.addChild(player);

			createjs.Tween.get(circle, { loop: true })
				.to({ x: 400 }, 1000, createjs.Ease.getPowInOut(4))
				.to({ alpha: 0, y: 175 }, 500, createjs.Ease.getPowInOut(2))
				.to({ alpha: 0, y: 225 }, 100)
				.to({ alpha: 1, y: 200 }, 500, createjs.Ease.getPowInOut(2))
				.to({ x: 100 }, 800, createjs.Ease.getPowInOut(2));

			createjs.Ticker.setFPS(60);
			createjs.Ticker.addEventListener("tick", stage);

			this.soundsService.loadSound();
			this.regiterKeyEvents();
		},


	};







	return game;
});














