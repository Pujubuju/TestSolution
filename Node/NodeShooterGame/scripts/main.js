requirejs.config({
    paths: {
        'jquery': 'lib/jquery'
    }
});

require(['jquery', 'message', 'another', 'messageService', 'game'], 
function($, message,  another, messageService, game){
	$('#output').html(message + ' +  ' + another);
	$('#output').append('<br>');
	$('#output').append(messageService.getA() + ' +  ' + messageService.getB());
	game.init();
	window.game = game;
});