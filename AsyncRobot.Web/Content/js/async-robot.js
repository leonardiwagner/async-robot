var mazeHub = $.connection.mazeHub;
var landJson = { "width": 20, "height": 20, "track": [{ "x": 15, "y": 1 }, { "x": 16, "y": 1 }, { "x": 17, "y": 1 }, { "x": 18, "y": 1 }, { "x": 4, "y": 2 }, { "x": 5, "y": 2 }, { "x": 6, "y": 2 }, { "x": 7, "y": 2 }, { "x": 8, "y": 2 }, { "x": 9, "y": 2 }, { "x": 10, "y": 2 }, { "x": 11, "y": 2 }, { "x": 12, "y": 2 }, { "x": 13, "y": 2 }, { "x": 15, "y": 2 }, { "x": 18, "y": 2 }, { "x": 2, "y": 3 }, { "x": 3, "y": 3 }, { "x": 4, "y": 3 }, { "x": 5, "y": 3 }, { "x": 13, "y": 3 }, { "x": 15, "y": 3 }, { "x": 18, "y": 3 }, { "x": 2, "y": 4 }, { "x": 5, "y": 4 }, { "x": 8, "y": 4 }, { "x": 9, "y": 4 }, { "x": 10, "y": 4 }, { "x": 11, "y": 4 }, { "x": 12, "y": 4 }, { "x": 13, "y": 4 }, { "x": 15, "y": 4 }, { "x": 18, "y": 4 }, { "x": 2, "y": 5 }, { "x": 5, "y": 5 }, { "x": 8, "y": 5 }, { "x": 15, "y": 5 }, { "x": 18, "y": 5 }, { "x": 2, "y": 6 }, { "x": 5, "y": 6 }, { "x": 8, "y": 6 }, { "x": 15, "y": 6 }, { "x": 18, "y": 6 }, { "x": 2, "y": 7 }, { "x": 5, "y": 7 }, { "x": 8, "y": 7 }, { "x": 15, "y": 7 }, { "x": 18, "y": 7 }, { "x": 2, "y": 8 }, { "x": 3, "y": 8 }, { "x": 4, "y": 8 }, { "x": 5, "y": 8 }, { "x": 6, "y": 8 }, { "x": 7, "y": 8 }, { "x": 8, "y": 8 }, { "x": 9, "y": 8 }, { "x": 10, "y": 8 }, { "x": 11, "y": 8 }, { "x": 12, "y": 8 }, { "x": 13, "y": 8 }, { "x": 14, "y": 8 }, { "x": 15, "y": 8 }, { "x": 18, "y": 8 }, { "x": 2, "y": 9 }, { "x": 5, "y": 9 }, { "x": 8, "y": 9 }, { "x": 11, "y": 9 }, { "x": 18, "y": 9 }, { "x": 2, "y": 10 }, { "x": 5, "y": 10 }, { "x": 8, "y": 10 }, { "x": 11, "y": 10 }, { "x": 18, "y": 10 }, { "x": 2, "y": 11 }, { "x": 3, "y": 11 }, { "x": 4, "y": 11 }, { "x": 5, "y": 11 }, { "x": 6, "y": 11 }, { "x": 7, "y": 11 }, { "x": 8, "y": 11 }, { "x": 11, "y": 11 }, { "x": 18, "y": 11 }, { "x": 2, "y": 12 }, { "x": 8, "y": 12 }, { "x": 11, "y": 12 }, { "x": 12, "y": 12 }, { "x": 13, "y": 12 }, { "x": 14, "y": 12 }, { "x": 15, "y": 12 }, { "x": 16, "y": 12 }, { "x": 18, "y": 12 }, { "x": 2, "y": 13 }, { "x": 8, "y": 13 }, { "x": 16, "y": 13 }, { "x": 18, "y": 13 }, { "x": 2, "y": 14 }, { "x": 3, "y": 14 }, { "x": 4, "y": 14 }, { "x": 5, "y": 14 }, { "x": 6, "y": 14 }, { "x": 7, "y": 14 }, { "x": 8, "y": 14 }, { "x": 16, "y": 14 }, { "x": 18, "y": 14 }, { "x": 2, "y": 15 }, { "x": 5, "y": 15 }, { "x": 16, "y": 15 }, { "x": 18, "y": 15 }, { "x": 2, "y": 16 }, { "x": 5, "y": 16 }, { "x": 9, "y": 16 }, { "x": 10, "y": 16 }, { "x": 11, "y": 16 }, { "x": 13, "y": 16 }, { "x": 14, "y": 16 }, { "x": 15, "y": 16 }, { "x": 16, "y": 16 }, { "x": 18, "y": 16 }, { "x": 19, "y": 16 }, { "x": 2, "y": 17 }, { "x": 5, "y": 17 }, { "x": 9, "y": 17 }, { "x": 13, "y": 17 }, { "x": 1, "y": 18 }, { "x": 2, "y": 18 }, { "x": 5, "y": 18 }, { "x": 9, "y": 18 }, { "x": 10, "y": 18 }, { "x": 11, "y": 18 }, { "x": 12, "y": 18 }, { "x": 13, "y": 18 }] };

$.connection.hub.start().done(function () {
    console.log("signalr's hub loaded with shoot, punch and bomb");
});

mazeHub.client.setRobotPosition = function (threadId, id, x, y) {
    setRobotPosition(id, x, y);
    return console.log("[" + threadId + "] [" + id + "] x:" + x + " y:" + y);
};

mazeHub.client.reached = function (time) {
    $("#time").append("Default: " + (time) + " seconds </br>");
};

var addRobot = function (id, x, y) {
    return mazeHub.server.addRobot(id, x, y);
};

var setLand = function () {
    return mazeHub.server.runByThreads(4);
};

var runAsync = function () {
    return mazeHub.server.runAsync();
};

var runByThreads = function () {
    return mazeHub.server.runByThreads(4);
};

var mazeObjectSize = 10;
var setRobotPosition = function (id, x, y) {
    var objectX = x * mazeObjectSize;
    var objectY = y * mazeObjectSize;
    $(".robot[data-id=" + id + "]").css('left', objectX + "px").css('top', objectY + "px");
};

var start = function() {
    var x = 1;
    var y = 18;

    for (var i = 1; i <= 64; i++) {
        var randomColor = "style='background-color: #" + Math.floor(Math.random() * 16777215).toString(16) + ";'";
        $("#maze").append("<div " + randomColor + " class='mazeObject robot' data-x='" + x + "'  data-y='" + y + "' data-id='" + i + "'></div>");
        setRobotPosition(i, x, y);
    }
};


var run = function() {
    start();
    mazeHub.server.run(JSON.stringify(landJson));
};

var runAsync = function () {
    start();
    mazeHub.server.runAsync(JSON.stringify(landJson));
};

var runThread = function (threadCount) {
    start();
    mazeHub.server.runThread(JSON.stringify(landJson), threadCount);
};