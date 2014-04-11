(function() {
  $(function() {
    var chat, createMazeObjectHandlers, maze, mazeObjectSizeHeight, mazeObjectSizeWidth, mazeToJSON, txtMazeHeight, txtMazeWidth;
    chat = $.connection.asyncRobotHub;
    $.connection.hub.start().done(function() {
      return console.log("hub carregado de tiro porrada e bomba");
    });
    chat.client.printTask = function(taskString) {
      return console.log(taskString);
    };
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
    mazeObjectSizeWidth = 10;
    mazeObjectSizeHeight = 10;
    maze = $("#maze");
    txtMazeHeight = $("#txtMazeHeight").val();
    txtMazeWidth = $("#txtMazeWidth").val();
    $("#btnMazeMount").click(function() {
      var mazeObjectCount, num, _i;
      mazeObjectCount = parseInt(txtMazeHeight) * parseInt(txtMazeWidth);
      maze.css('width', (parseInt(txtMazeWidth) * mazeObjectSizeWidth) + "px");
      maze.css('height', (parseInt(txtMazeHeight) * mazeObjectSizeHeight) + "px");
      for (num = _i = 0; 0 <= mazeObjectCount ? _i <= mazeObjectCount : _i >= mazeObjectCount; num = 0 <= mazeObjectCount ? ++_i : --_i) {
        maze.append("<div class='mazeObject wall'></div>");
      }
      return createMazeObjectHandlers();
    });
    $("#btnMazeExport").click(function() {
      return mazeToJSON();
    });
    mazeToJSON = function() {
      var a, objectX, objectY, objectsPerLine, returnJSON;
      objectsPerLine = parseInt(txtMazeWidth) - 1;
      objectY = 0;
      objectX = 0;
      returnJSON = "[";
      $.each($(".mazeObject"), function() {
        var objectValue;
        if ($(this).hasClass("wall")) {
          objectValue = "wall";
        } else if ($(this).hasClass("space")) {
          objectValue = "space";
        }
        returnJSON += '{"x": ' + objectX + ', "y": ' + objectY + ', "value": "' + objectValue + '" },';
        if (objectX === objectsPerLine) {
          objectX = 0;
          return objectY++;
        } else {
          return objectX++;
        }
      });
      a = returnJSON.length;
      returnJSON = returnJSON.substring(0, a - 1);
      returnJSON += "]";
      return console.log(JSON.parse(returnJSON));
    };
    createMazeObjectHandlers = function() {
      return $(".mazeObject").click(function() {
        if ($(this).hasClass('wall')) {
          $(this).removeClass('wall');
          return $(this).addClass('space');
        } else if ($(this).hasClass('space')) {
          $(this).removeClass('wall');
          return $(this).addClass('space');
        }
      });
    };
    $("#addRobot").click(function() {
      chat.server.addRobot();
      return console.log("adicionando robo");
    });
    return $("#btnTask").click(function() {
      return chat.server.runRobots(10);
    });
  });

}).call(this);
