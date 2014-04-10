(function() {
  $(function() {
    var chat;
    chat = $.connection.asyncRobotHub;
    $.connection.hub.start().done(function() {
      return console.log("hub carregado de tiro porrada e bomba");
    });
    chat.client.mountMaze = function(mazeJson) {
      var landObject, _i, _len, _results;
      mazeJson = JSON.parse(mazeJson);
      _results = [];
      for (_i = 0, _len = mazeJson.length; _i < _len; _i++) {
        landObject = mazeJson[_i];
        _results.push(console.log(landObject.value));
      }
      return _results;
    };
    return $("#createMaze").click(function() {
      chat.server.requestMazeCreation();
      return console.log("mandando mensagem pro servidor");
    });
  });

}).call(this);
