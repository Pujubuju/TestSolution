var Knight = (function () {
    function Knight(hp, dmg) {
        this.hp = hp;
        this.dmg = dmg;
    }
    Knight.prototype.attack = function (target) {
        target.decreaseHp(this.dmg);
    };
    Knight.prototype.isAlive = function () {
        return this.hp > 0;
    };
    Knight.prototype.decreaseHp = function (number) {
        this.hp -= number;
    };
    Knight.prototype.stats = function () {
        console.log("hp: " + this.hp + ", dmg: " + this.dmg);
    };
    return Knight;
})();
exports.Knight = Knight;
