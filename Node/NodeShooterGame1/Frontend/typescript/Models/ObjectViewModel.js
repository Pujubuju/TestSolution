var ObjectViewModel = (function () {
    function ObjectViewModel(model, viewObject) {
        this.model = model;
        this.viewObject = viewObject;
        this.update();
    }
    ObjectViewModel.prototype.update = function () {
        this.viewObject.x = this.model.x;
        this.viewObject.y = this.model.y;
    };
    return ObjectViewModel;
})();
exports.ObjectViewModel = ObjectViewModel;
