"use strict";
var Orc = require("./Orc");

function OrcOverlord(hp, dmg) {
	Orc.call(this, hp, dmg);

	this.attack = function attack(target) {
		if(critical()){
			target.takeDmg(this.dmg*2);
		}
		else{
			target.takeDmg(this.dmg);
		}		
	}
	
	var critical = function(){
		return Math.random() > 0.7;
	}
}

module.exports = Orc;