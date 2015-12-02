"use strict";
var Unit = require("./Unit");

function Orc(hp, dmg) {
	Unit.call(this, hp, dmg);
	
	this.stats = function stats() {
		console.log("hp: " + hp + ", dmg: " + dmg);
	}
	this.attack = function attack(target) {
		target.takeDmg(dmg);
	}
	this.takeDmg = function takeDmg(dmg){
		hp = hp - dmg;
	}	
}

module.exports = Orc;