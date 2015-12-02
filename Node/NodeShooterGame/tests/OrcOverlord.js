"use strict";
var Orc = require("./Orc");

function OrcOverlord(hp, dmg) {
	Orc.call(this, hp, dmg);

	this.attack = function attack(target) {
		target.takeDmg(dmg*2);
	}
}

module.exports = Orc;