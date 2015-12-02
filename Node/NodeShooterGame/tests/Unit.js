"use strict";

function Unit(hp, dmg){
	var stunned = false;
	
	this.isStunned = function(){
		return stunned;
	}
	
	this.stun = function(){
		stunned = true;
	}
}

module.exports = Unit;