/// <reference path="../Abstract/IXYObject.ts" />

export class ObjectViewModel {

	private model: Abstract.IXYObject;
	private viewObject: Abstract.IXYObject;

	constructor(model: Abstract.IXYObject, viewObject: Abstract.IXYObject) {
		this.model = model;
		this.viewObject = viewObject;
		this.update();
	}

	update(){
		this.viewObject.x = this.model.x;
		this.viewObject.y = this.model.y;
	}
}