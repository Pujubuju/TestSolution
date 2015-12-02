"use strict";

var Unit = require("./Unit");
var Orc = require("./Orc");
var OrcOverlord = require("./OrcOverlord");
var SuperOrc = require("./SuperOrc");

var unit = new Unit(100, 10);
var orc = new Orc(200, 20);

unit.stats();
orc.stats();


// var orcOverlord = new OrcOverlord(200, 50);
// var superOrc = new SuperOrc(1000, 200);
// 
// orcOverlord.stats();
// superOrc.stats();
// 
// console.log("Let the fight begins!!! Wraaaagrhhh!!!")
// while (orcOverlord.isAlive() && superOrc.isAlive()) {
// 	orcOverlord.attack(superOrc);
// 	superOrc.attack(orcOverlord);
// 
// 	orcOverlord.stats();
// 	superOrc.stats();
// }
