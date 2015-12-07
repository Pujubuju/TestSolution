/// <reference path="../Models/NameImagePair" />


module Services {
	export class ImagesService {
		private images: Array<Models.NameImagePair>;
		private loadQueue: any;

		constructor(loadQueue: any) {
			this.loadQueue = loadQueue;
			this.images = new Array();
		}

		loadImages() {
			this.loadQueue.addEventListener("fileload", this.handleFileComplete);
			this.loadQueue.loadFile("ship1.png");
		}

		handleFileComplete(event: any) {			
			var itemSplitted = event.item.src.split("/");
			var name = itemSplitted[itemSplitted.length - 1];
			var img = event.result;
			var item = new Models.NameImagePair(name, img);
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

	}
}