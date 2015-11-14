define(['message', 'another'], function(message, another){
	
	var service = {
		getA : function() {
		return message;
		},
		getB : function() {
			return another;
		}
	}
		
	return service;
});