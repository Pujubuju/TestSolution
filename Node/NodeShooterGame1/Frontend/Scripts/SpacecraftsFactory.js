
function Coordinates() {
    var _self = this;
    _self.x = 0;
    _self.y = 0;
    _self.set = function (x, y) {
        _self.x = x;
        _self.y = y;
    }
}

function Vector(x, y) {
    var _self = this;
    _self.x = x;
    _self.y = y;

    _self.normalized = function () {
        var norm = Math.sqrt(Math.pow(_self.x, _self.x) + Math.pow(_self.y, _self.y));
        var normalized = new Vector(_self.x / norm, _self.y / norm);
        return normalized;
    }
    
    _self.dot = function(vector){
        return _self.x * vector.x + _self.y * vector.y;
    }
}

function Spacecraft() {
    var _self = this;
    _self.coordinates = new Coordinates();
    _self.rotation = 0;
    _self.rotationSpeed = 5;
    _self.speed = 10;

    _self.vect = function () {
        return new Vector(Math.sin(_self.rotation), Math.cos(_self.rotation));
    }

    _self.findClosestTarget = function (enemies) {
        var closest = 360;
        var myVect = _self.vect();
        var x = _self.coordinates.x;
        var y = _self.coordinates.y;
        for (var index = 0; index < enemies.length; index++) {
            var enemy = enemies[index];
            var enemyX = enemy.coordinates.x;
            var enemyY = enemy.coordinates.y;
            var vectorToEnemy = new Vector(enemyX - x, enemyY - y);
            var normalizedVectorToEnemy = vectorToEnemy.normalized();
            var dot = normalizedVectorToEnemy.dot(myVect);
            var angle = Math.acos(dot) * 57;
            if(Math.abs(angle) < Math.abs(closest)){
                closest = angle;
            }
        }
        if(closest > 0){
            _self.rotation = _self.rotation + _self.rotationSpeed;
        }
        else{
            _self.rotation = _self.rotation - _self.rotationSpeed;
        }
    }



    _self.act = function () {
        _self.coordinates.x++;
    }
}

function SpacecraftsFactory(soundsService, imagesService) {
    var _self = this;
    _self.soundsService = soundsService;
    _self.imagesService = imagesService;

    _self.createNew = function (x, y) {
        var craft = new Spacecraft();
        craft.coordinates.set(x, y);
        return craft;
    }
}

define(['soundsService', 'imagesService'],
    function (soundsService, imagesService) {
        return new SpacecraftsFactory(soundsService, imagesService);
    });