"use strict";
var OrcOverlord = require("./OrcOverlord");

function SuperOrc(hp, dmg) {
	OrcOverlord.call(this, hp, dmg);

	this.attack = function attack(target) {
		target.takeDmg(dmg*3);
	}
}

module.exports = SuperOrc;