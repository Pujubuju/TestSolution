
function Coordinates() {
    var _self = this;
    _self.x = 0;
    _self.y = 0;
    _self.set = function (x, y) {
        _self.x = x;
        _self.y = y;
    }
}

function Spacecraft() {
    var _self = this;
    _self.coordinates = new Coordinates();
    _self.rotation = 0;
    _self.rotationSpeed = 5;
    _self.speed = 10;

    _self.findClosestTarget = function (enemies) {
        for (var index = 0; index < enemies.length; index++) {
            var enemy = enemies[index];
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