(function() {
  $(function() {
    var chat, createMazeObjectHandler, createMazeObjectHandlers, maze, mazeObjectSizeHeight, mazeObjectSizeWidth, mazeToJSON;
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
    mazeObjectSizeWidth = 10;
    mazeObjectSizeHeight = 10;
    maze = $("#maze");
    chat.client.mountMaze = function(mazeJson) {
      var html, landObject, lastY, _i, _len;
      mazeJson = JSON.parse(mazeJson);
      $("#maze").empty();
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
    $("#btnMazeImport").click(function() {
      var json, landObject, _i, _len, _results;
      json = JSON.parse($("#txtMazeJson").val());
      _results = [];
      for (_i = 0, _len = json.length; _i < _len; _i++) {
        landObject = json[_i];
        if (landObject.value === "space") {
          $(".mazeObject[data-coordinate='" + landObject.x + "-" + landObject.y + "']").removeClass("wall");
          _results.push($(".mazeObject[data-coordinate='" + landObject.x + "-" + landObject.y + "']").addClass("space"));
        } else {
          $(".mazeObject[data-coordinate='" + landObject.x + "-" + landObject.y + "']").removeClass("space");
          _results.push($(".mazeObject[data-coordinate='" + landObject.x + "-" + landObject.y + "']").addClass("wall"));
        }
      }
      return _results;
    });
    $("#btnMazeMount").click(function() {
      var coordinate, mazeObjectCount, num, objectX, objectY, objectsPerLine, txtMazeHeight, txtMazeWidth, _i;
      maze.empty();
      txtMazeHeight = $("#txtMazeHeight").val();
      txtMazeWidth = $("#txtMazeWidth").val();
      objectsPerLine = parseInt(txtMazeWidth) - 1;
      objectY = 0;
      objectX = 0;
      mazeObjectCount = parseInt(txtMazeHeight) * parseInt(txtMazeWidth);
      maze.css('width', (parseInt(txtMazeWidth) * mazeObjectSizeWidth) + "px");
      maze.css('height', (parseInt(txtMazeHeight) * mazeObjectSizeHeight) + "px");
      for (num = _i = 0; 0 <= mazeObjectCount ? _i <= mazeObjectCount : _i >= mazeObjectCount; num = 0 <= mazeObjectCount ? ++_i : --_i) {
        coordinate = objectX + "-" + objectY;
        maze.append("<div class='mazeObject wall' data-coordinate='" + coordinate + "'></div>");
        if (objectX === objectsPerLine) {
          objectX = 0;
          objectY++;
        } else {
          objectX++;
        }
      }
      return createMazeObjectHandlers();
    });
    $("#btnMazeExport").click(function() {
      return mazeToJSON();
    });
    mazeToJSON = function() {
      var a, objectX, objectY, objectsPerLine, returnJSON, txtMazeHeight, txtMazeWidth;
      maze = $("#maze");
      txtMazeHeight = $("#txtMazeHeight").val();
      txtMazeWidth = $("#txtMazeWidth").val();
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
      return $("#txtMazeJson").val(returnJSON);
    };
    createMazeObjectHandlers = function() {
      return $(".mazeObject").click(function() {
        return createMazeObjectHandler(this);
      });
    };
    createMazeObjectHandler = function(obj) {
      if ($(obj).hasClass('wall')) {
        $(obj).removeClass('wall');
        return $(obj).addClass('space');
      } else if ($(obj).hasClass('space')) {
        $(obj).removeClass('space');
        return $(obj).addClass('wall');
      }
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
