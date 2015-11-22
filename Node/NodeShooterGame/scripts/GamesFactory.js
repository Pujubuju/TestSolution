function Game(soundsService, ImagesService) {
	var _self = this;
	this.soundsService = soundsService;
	this.imagesService = ImagesService;
	this.player = {};

	this.playSound = function () {
		this.soundsService.playSound();
	};

	this.getImage = function () {
		var shipImage = _self.getShipImage();
		if (shipImage != null) {
			_self.player = new createjs.Bitmap(shipImage);
			_self.player.x = 100;
			_self.player.y = 100;
			_self.stage.addChild(this.player);
		}
	};

	this.getShipImage = function () {
		return _self.imagesService.getImage("ship1.png");
	}

	this.regiterKeyEvents = function () {
		document.addEventListener('keydown', function (event) {
			var wCode = 87;
			var aCode = 65;
			var sCode = 83;
			var dCode = 68;
			var stepSize = 25;

			if (event.keyCode == wCode) {
				_self.player.y = _self.player.y - stepSize;
			}
			else if (event.keyCode == aCode) {
				_self.player.x = _self.player.x - stepSize;
			}
			else if (event.keyCode == sCode) {
				_self.player.y = _self.player.y + stepSize;
			}
			else if (event.keyCode == dCode) {
				_self.player.x = _self.player.x + stepSize;
			}
		});
	};

	this.showCoords = function (event) {
		if(_self.player != null){
			_self.player.x = event.clientX - _self.player.image.width/2;
			_self.player.y = event.clientY - _self.player.image.height/2;
		}
	};

	this.init = function () {
		this.imagesService.loadImages();
		this.soundsService.loadSound();
		this.regiterKeyEvents();

		_self.stage = new createjs.Stage("demoCanvas");
		var circle = new createjs.Shape();
		circle.graphics.beginFill("DeepSkyBlue").drawCircle(0, 0, 50);
		circle.x = 100;
		circle.y = 100;
		_self.stage.addChild(circle);

		createjs.Tween.get(circle, { loop: true })
			.to({ x: 400 }, 1000, createjs.Ease.getPowInOut(4))
			.to({ alpha: 0, y: 175 }, 500, createjs.Ease.getPowInOut(2))
			.to({ alpha: 0, y: 225 }, 100)
			.to({ alpha: 1, y: 200 }, 500, createjs.Ease.getPowInOut(2))
			.to({ x: 100 }, 800, createjs.Ease.getPowInOut(2));

		createjs.Ticker.setFPS(60);
		createjs.Ticker.addEventListener("tick", _self.stage);
	};
}

function GamesFactory(soundsService, imagesService) {
    this.soundsService = soundsService;
	this.imagesService = imagesService;
	this.createNew = function (title) {
		return new Game(soundsService, imagesService);
	}
}

define(['soundsService', 'imagesService'],
	function (soundsService, imagesService) {
		return new GamesFactory(soundsService, imagesService);
	});