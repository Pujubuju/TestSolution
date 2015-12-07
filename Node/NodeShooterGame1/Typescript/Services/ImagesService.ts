import nameImagePair = require("../Models/NameImagePair");

export class ImagesService {
	private images: Array<nameImagePair.NameImagePair>;

	constructor() {
		this.images = new Array();
	}

	loadImages() {
		var item = new nameImagePair.NameImagePair("ship1.png", "ship1.png");
		this.images.push(item);
	}

	getImage(name: string) {
		for (var index = 0; index < this.images.length; index++) {
			var item = this.images[index];
			if (item.name == name) {
				return item.img;
			}
		}
		return null;
	}
	
	getCount(){
		return this.images.length;
	}

}