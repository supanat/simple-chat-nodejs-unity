// 01-creating-a-simple-chat-room
var express = require('express')
var app = express()
var http = require('http')
var socketIO = require('socket.io')

/*
app.get('/', function (req, res) {
    res.sendFile(__dirname + '/index.html');
});
*/

var server = http.Server(app);
server.listen(5555);

var io = socketIO(server);

io.on('connection', function (socket) {

    console.log('client connected '+ socket.id);

    socket.on('message.send', function (data) {
        console.log(data)
        //io.emit('message.sent', data);

        
        io.to(socket.id).emit('message.sent',data);
    });



    socket.on('disconnect',function(){

        console.log("client disconnected");
    });


});