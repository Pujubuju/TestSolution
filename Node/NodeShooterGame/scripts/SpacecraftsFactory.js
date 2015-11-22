function Spacecraft(title) {
    this.title = title;
    this.description;
    this.setDescription = function(description)
    {
        this.description = description;
    }
}

function SpacecraftsFactory(soundsService, imagesService) {
    this.soundsService = soundsService;
	this.imagesService = imagesService;	
	this.createNew = function (title) {
		return new Spacecraft(title);
	}
}

define(['soundsService', 'imagesService'], 
function (soundsService, imagesService) {
	return new SpacecraftsFactory(soundsService, imagesService);	
});