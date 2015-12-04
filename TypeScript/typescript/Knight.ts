interface Unit{
    decreaseHp(number : number) : void;
}

class Knight{
    
    private hp : number;
	private dmg : number;
    
    constructor(hp : number, dmg : number) {
        this.hp = hp;
		this.dmg = dmg;
    }
    
    attack(target : Unit){
        target.decreaseHp(this.dmg);
    }
   
   isAlive(){
       return this.hp > 0;
   }
   
   decreaseHp(number : number){
       this.hp -= number;
   }
   
   stats(){
       console.log("hp: " + this.hp + ", dmg: " + this.dmg);
   }
}




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

