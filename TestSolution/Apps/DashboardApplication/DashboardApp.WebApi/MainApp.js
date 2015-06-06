// create the module and name it scotchApp
var mainApp = angular.module('mainApp', ['ngRoute']);

// configure our routes
mainApp.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: 'App/Components/Home/Home.html',
            controller: 'homeController'
        })
        .when('/Home', {
            templateUrl: 'App/Components/Home/Home.html',
            controller: 'homeController'
        })
        .when('/Page1', {
            templateUrl: 'App/Components/Page1/Page1.html',
            controller: 'page1Controller'
        })
        .when('/Page2', {
            templateUrl: 'App/Components/Page2/Page2.html',
            controller: 'page2Controller'
        })
        .when('/Register', {
            templateUrl: 'App/Components/Register/Register.html',
            controller: 'registerController'
        })
        .when('/Login', {
            templateUrl: 'App/Components/Login/Login.html',
            controller: 'loginController'
        });
});




