import knight = require("./Knight");

export module UnitsModule {    
    export var Knight = function(hp :number, dmg: number){
            return new knight.Knight(hp, dmg);
        }    
}
