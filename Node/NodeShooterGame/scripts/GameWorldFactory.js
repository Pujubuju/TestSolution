function GameWorld(spacecraftsFactory) {
	var _self = this;
	_self.spacecraftsFactory = spacecraftsFactory;
	_self.start = function () {
		setInterval(_self.onClockTick, 200);
	}
	_self.stop = function () {
		clearInterval(_self.start);
	}
	_self.crafts = new Array();
	for (var index = 0; index < 2; index++) {
		_self.crafts.push(spacecraftsFactory.createNew(200, 200));		
	}
	
	_self.onClockTick = function(){
		for (var index = 0; index < _self.crafts.length; index++) {
			var craft = _self.crafts[index];
			if(Math.random() > 0.5){
				craft.coordinates.x += Math.random()*10;
				craft.coordinates.y += Math.random()*10;
			}
			else{
				craft.coordinates.x -= Math.random()*10;
				craft.coordinates.y -= Math.random()*10;
			}			
		}
		_self.viewUpdateHandler();
	};
	
	_self.viewUpdateHandler = function () {		
	}
}

function GameWorldFactory(spacecraftsFactory) {
	this.createNew = function (title) {
		return new GameWorld(spacecraftsFactory);
	}
}

define(['spacecraftsFactory'],
	function (spacecraftsFactory) {
		return new GameWorldFactory(spacecraftsFactory);
	});