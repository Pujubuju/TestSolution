"use strict";

var Orc = require("./Orc");
var OrcOverlord = require("./OrcOverlord");
var SuperOrc = require("./SuperOrc");

var orc = new Orc(100, 20);
var orcOverlord = new OrcOverlord(200, 50);
var superOrc = new SuperOrc(1000, 200);

orc.stats();
orcOverlord.stats();
superOrc.stats();

orc.attack(orcOverlord);
orcOverlord.attack(orc);
superOrc.attack(orcOverlord);

orc.stats();
orcOverlord.stats();
superOrc.stats();