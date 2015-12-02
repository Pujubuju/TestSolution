"use strict";
var Unit = require("./Unit");
var OrcOverlord = require("./OrcOverlord");

function SuperOrc(hp, dmg) {
	OrcOverlord.call(this, hp, dmg);

	this.stats = function stats() {
		console.log("SuperOrc");
		Unit.prototype.stats();
		//console.log("hp: " + this.hp + ", dmg: " + this.dmg, ", alive: " + this.isAlive());
	}

	this.attack = function attack(target) {
		target.takeDmg(dmg*3);
	}
}

module.exports = SuperOrc;