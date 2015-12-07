
function ObjectViewModel(model, viewObject) {
	var _self = this;
	_self.model = model;
	_self.viewObject = viewObject;

	_self.update = function () {
		_self.viewObject.x = _self.model.coordinates.x;
		_self.viewObject.y = _self.model.coordinates.y;
	}

	_self.update();
}

function Game(soundsService, ImagesService, gameWorldFactory) {
	var _self = this;
	_self.soundsService = soundsService;
	_self.imagesService = ImagesService;
	_self.gameWorldFactory = gameWorldFactory;	
	
	_self.gameWorld = gameWorldFactory.createNew();

	_self.player = {};

	_self.playSound = function () {
		_self.soundsService.playSound();
	};

	_self.getImage = function () {
		var shipImage = _self.getShipImage();
		if (shipImage != null) {
			_self.player = new createjs.Bitmap(shipImage);
			_self.player.x = 100;
			_self.player.y = 100;
			_self.stage.addChild(_self.player);
		}
	};

	_self.getShipImage = function () {
		return _self.imagesService.getImage("ship1.png");
	}

	_self.regiterKeyEvents = function () {
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

	_self.showCoords = function (event) {
		if (_self.player != null) {
			_self.player.x = event.clientX - _self.player.image.width / 2;
			_self.player.y = event.clientY - _self.player.image.height / 2;
		}
	};

	_self.loadResources = function(){
					_self.imagesService.loadImages();
		_self.soundsService.loadSound();
		_self.regiterKeyEvents();
	}

	_self.init = function () {


		_self.stage = new createjs.Stage("demoCanvas");
		_self.viewModels = new Array();
		for (var index = 0; index < _self.gameWorld.crafts.length; index++) {
			var craft = _self.gameWorld.crafts[index];
			
			var shipImage = _self.getShipImage();
			var ship = new createjs.Bitmap(shipImage);
			ship.scaleX = 0.5;
			ship.scaleY = 0.5;
			
			var circle = new createjs.Shape();
			circle.graphics.beginFill("DeepSkyBlue").drawCircle(0, 0, 10);
			_self.viewModels.push(new ObjectViewModel(craft, ship));
			_self.stage.addChild(ship);
			
			
			
		}
		_self.gameWorld.start();
	};

	_self.gameWorld.viewUpdateHandler = function () {
		for (var index = 0; index < _self.viewModels.length; index++) {
			var viewModel = _self.viewModels[index];
			viewModel.update();
		}
		_self.stage.update();
	}

	_self.start = function () {
		for (var index = 0; index < _self.viewModels.length; index++) {
			var viewModel = _self.viewModels[index];
			viewModel.update();
		}
		
	}
}

function GamesFactory(soundsService, imagesService, gameWorldFactory) {
    this.soundsService = soundsService;
	this.imagesService = imagesService;
	this.createNew = function (title) {
		return new Game(soundsService, imagesService, gameWorldFactory);
	}
}

define(['soundsService', 'imagesService', 'gameWorldFactory'],
	function (soundsService, imagesService, gameWorldFactory) {
		return new GamesFactory(soundsService, imagesService, gameWorldFactory);
	});