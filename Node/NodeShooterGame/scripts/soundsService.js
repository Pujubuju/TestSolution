define([], function () {
	var service = {
		soundID: "Thunder",
		loadSound: function () {
			createjs.Sound.registerSound("pagneLingua.mp3", this.soundID);
		},
		playSound: function () {
			createjs.Sound.play(this.soundID);
		},
	}
	return service;
});