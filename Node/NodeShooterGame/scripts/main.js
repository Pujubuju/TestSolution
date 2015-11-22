requirejs.config({
    paths: {
        'jquery': 'lib/jquery'
    }
});

require(['jquery', 'message', 'another', 'messageService', 'GamesFactory', 'SpacecraftsFactory'], 
function($, message,  another, messageService, GamesFactory, SpacecraftsFactory){
	$('#output').html(message + ' +  ' + another);
	$('#output').append('<br>');
	$('#output').append(messageService.getA() + ' +  ' + messageService.getB());
	
	$('#output').append('<br>');
	$('#output').append(SpacecraftsFactory.createNew("lololo").title);
	
	var game = GamesFactory.createNew();
	game.init();
	window.game = game;
});