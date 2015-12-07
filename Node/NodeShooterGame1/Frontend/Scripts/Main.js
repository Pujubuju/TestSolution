requirejs.config({
    paths: {
        'jquery': 'lib/jquery'
    }
});

require(['jquery', 'GamesFactory', 'SpacecraftsFactory'], 
function($, GamesFactory, SpacecraftsFactory){	
	var game = GamesFactory.createNew();
	game.loadResources();
	window.game = game;
});