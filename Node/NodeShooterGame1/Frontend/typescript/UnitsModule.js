var knight = require("./Knight");
var UnitsModule;
(function (UnitsModule) {
    UnitsModule.Knight = function (hp, dmg) {
        return new knight.Knight(hp, dmg);
    };
})(UnitsModule = exports.UnitsModule || (exports.UnitsModule = {}));
