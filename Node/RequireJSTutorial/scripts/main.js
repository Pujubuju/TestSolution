requirejs.config({
    paths: {
        'jquery': 'lib/jquery'
    }
});

require(['jquery', 'message', 'another', 'messageService'], 
function($, message,  another, messageService){
	$('#output').html(message + ' +  ' + another);
	$('#output').append('<br>');
	$('#output').append(messageService.getA() + ' +  ' + messageService.getB());
});