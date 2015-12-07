var express = require('express');
var app = express();

app.get('/', function (req, res) {
   res.send('Hello World');
})

var server = app.listen(8081, function () {
  var host = server.address().address
  var port = server.address().port
  console.log("Example app listening at http://%s:%s", host, port)
})

var connect = require('connect');
var serveStatic = require('serve-static');
connect()
.use(serveStatic(__dirname + "/../frontend"))
.use(serveStatic(__dirname + '/../resources/images'))
.listen(8080);
console.log("Static files server listening at http://%s:%s", "localhost", 8080)