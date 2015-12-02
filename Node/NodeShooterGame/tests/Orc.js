"use strict";
var Unit = require("./Unit");

function Orc(hp, dmg) {
	Unit.call(this, hp, dmg);
	
	this.attack = function attack(target) {
		target.takeDmg(dmg);
	}
	this.takeDmg = function takeDmg(dmg){
		this.hp = this.hp - dmg;
	}	
	
	
	//this.stats = function(){
	//	console.log("orc");
	//}
}

Orc.prototype.stats = function () {
	console.log("orc");
	console.log("hp: " + this.hp + ", dmg: " + this.dmg, ", alive: " + this.isAlive());	
	Unit.prototype.stats();
}

module.exports = Orc;