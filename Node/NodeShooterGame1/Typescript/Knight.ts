/// <reference path="Abstract/IUnit.ts" />

export class Knight {

	private hp: number;
	private dmg: number;

	constructor(hp: number, dmg: number) {
		this.hp = hp;
		this.dmg = dmg;
	}

	attack(target: Abstract.IUnit) {
		target.decreaseHp(this.dmg);
	}

	isAlive() {
		return this.hp > 0;
	}

	decreaseHp(number: number) {
		this.hp -= number;
	}

	stats() {
		console.log("hp: " + this.hp + ", dmg: " + this.dmg);
	}
}