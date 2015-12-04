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
var knight1 = new Knight(200, 10);
var knight2 = new Knight(200, 10);
var kinght3 = new Knight(300, 20);
var kinght4 = new Knight(300, 20);
while (knight1.isAlive() && knight2.isAlive()) {
    knight1.attack(knight2);
    knight2.attack(knight1);
    knight1.stats();
    knight2.stats();
}
