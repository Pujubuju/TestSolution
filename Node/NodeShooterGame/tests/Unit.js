"use strict";

function Unit(hp, dmg) {

	this.hp = hp;
	this.dmg = dmg;

	var stunned = false;

	this.isStunned = function () {
		return stunned;
	}

	this.stun = function () {
		stunned = true;
	}

	this.isAlive = function () {
		return this.hp > 0;
	}
}

var proto = Unit.prototype;
proto.stats = function () {
	console.log("unit");
	console.log("hp: " + this.hp + ", dmg: " + this.dmg, ", alive: " + this.isAlive());
}

	proto.isAlive = function () {
		return this.hp > 0;
	}

module.exports = Unit;