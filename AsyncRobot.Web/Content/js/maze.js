(function() {
  $(function() {
    var chat;
    chat = $.connection.asyncRobotHub;
    $.connection.hub.start().done(function() {
      return console.log("hub carregado de tiro porrada e bomba");
    });
    chat.client.setRobotPosition = function(x, y, robotId) {
      $("#maze").removeClass("robot-" + robotId);
      return $("#maze maze-item-" + x + "-" + y).addClass("robot-" + robotId);
    };
    chat.client.mountMaze = function(mazeJson) {
      var html, landObject, lastY, _i, _len;
      mazeJson = JSON.parse(mazeJson);
      lastY = 0;
      html = "<div class='line'>";
      for (_i = 0, _len = mazeJson.length; _i < _len; _i++) {
        landObject = mazeJson[_i];
        if (parseInt(landObject.y) !== lastY) {
          lastY = parseInt(landObject.y);
          if (lastY > 0) {
            html += "</div>";
          }
          html += "<div class='line'>";
        }
        if (landObject.value === "#") {
          html += "<div class='wall'></div>";
        } else if (landObject.value === " ") {
          html += "<div class='space'></div>";
        }
      }
      html += "</div>";
      return $("#maze").append(html);
    };
    $("#createMaze").click(function() {
      chat.server.requestMazeCreation();
      return console.log("mandando mensagem pro servidor");
    });
    return $("#addRobot").click(function() {
      chat.server.addRobot();
      return console.log("adicionando robo");
    });
  });

}).call(this);
