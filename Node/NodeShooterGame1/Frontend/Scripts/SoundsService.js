function SoundsService() {
	var _self = this;
	_self.soundID = "Thunder",
	
	_self.loadSound = function () {
		createjs.Sound.registerSound("pagneLingua.mp3", _self.soundID);
	}
	
	_self.playSound = function () {
		createjs.Sound.play(_self.soundID);
	}
}

define([],
	function () {
		return new SoundsService();
	});