(function() {
  $(function() {
    var changeObject, chat, createMaze, createMazeObjectHandler, createMazeObjectHandlers, maze, mazeObjectSize, mazeObjectSizeHeight, mazeObjectSizeWidth, mazeToJson, readMap, robotToJson, setRobotPosition, startRobot, startRobots;
    chat = $.connection.asyncRobotHub;
    mazeObjectSize = 10;
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
    $("#btnRun").click(function() {
      return startRobots();
    });
    startRobots = function() {
      var maze, robots;
      startRobot();
      maze = mazeToJson();
      robots = robotToJson();
      return chat.server.runRobot(maze, robots);
    };
    startRobot = function() {
      var coordinate, html, num, randomSpace, robotCount, robotX, robotY, trackCount, tracks, _i, _ref, _results;
      robotCount = 6;
      tracks = JSON.parse(mazeToJson()).track;
      trackCount = tracks.length;
      _results = [];
      for (num = _i = 0, _ref = robotCount - 1; 0 <= _ref ? _i <= _ref : _i >= _ref; num = 0 <= _ref ? ++_i : --_i) {
        randomSpace = Math.floor(Math.random() * trackCount);
        robotX = tracks[randomSpace].x;
        robotY = tracks[randomSpace].y;
        coordinate = robotX + "-" + robotY;
        html = "<div class='mazeObject robot' data-x='" + robotX + "'  data-y='" + robotY + "' data-id='" + num + "'></div>";
        $("#maze").append(html);
        _results.push(setRobotPosition(num, robotX, robotY));
      }
      return _results;
    };
    chat.client.setRobot = function(id, x, y) {
      setRobotPosition(id, x, y);
      return console.log("robot " + id + " " + x + " " + y);
    };
    setRobotPosition = function(robotId, x, y) {
      var objectX, objectY;
      objectX = x * mazeObjectSize;
      objectY = y * mazeObjectSize;
      return $(".robot[data-id=" + robotId + "]").css('left', objectX + "px").css('top', objectY + "px");
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
    $("#btnMazeMount").click(function() {
      return createMaze();
    });
    $("#btnMazeImport").click(function() {
      return createMaze(readMap("simple"));
    });
    readMap = function(mapName) {
      $.get("/Content/maze/" + mapName + ".html", function(data) {
        return createMaze(JSON.parse(data));
      });
      return void 0;
    };
    changeObject = (function() {
      var landObject, _i, _len, _results;
      _results = [];
      for (_i = 0, _len = json.length; _i < _len; _i++) {
        landObject = json[_i];
        if (landObject.value === "space") {
          $(".mazeObject[data-x='" + landObject.x + "-" + landObject.y + "']").removeClass("wall");
          _results.push($(".mazeObject[data-x='" + landObject.x + "-" + landObject.y + "']").addClass("space"));
        } else {
          $(".mazeObject[data-x='" + landObject.x + "-" + landObject.y + "']").removeClass("space");
          _results.push($(".mazeObject[data-x='" + landObject.x + "-" + landObject.y + "']").addClass("wall"));
        }
      }
      return _results;
    });
    createMaze = function(jsonMaze) {
      var html, lastY, mazeHeight, mazeObjectCount, mazeObjectPerLine, mazeTrack, mazeWidth, num, objectX, objectY, trackItem, _i, _ref;
      $("#maze").empty();
      if (jsonMaze !== void 0) {
        mazeWidth = jsonMaze.width;
        mazeHeight = jsonMaze.height;
        mazeTrack = jsonMaze.track;
        $("#txtMazeWidth").val(mazeWidth);
        $("#txtMazeHeight").val(mazeHeight);
      } else {
        mazeWidth = $("#txtMazeWidth").val();
        mazeHeight = $("#txtMazeHeight").val();
        mazeTrack = null;
      }
      mazeObjectPerLine = parseInt(mazeWidth) - 1;
      mazeObjectCount = mazeWidth * mazeHeight;
      trackItem = 0;
      objectX = 0;
      objectY = 0;
      lastY = 0;
      html = "<div class='line'>";
      for (num = _i = 0, _ref = mazeObjectCount - 1; 0 <= _ref ? _i <= _ref : _i >= _ref; num = 0 <= _ref ? ++_i : --_i) {
        if (objectY !== lastY) {
          lastY = objectY;
          if (lastY > 0) {
            html += "</div>";
          }
          html += "<div class='line'>";
        }
        if (mazeTrack !== null && mazeTrack[trackItem].x === objectX && mazeTrack[trackItem].y === objectY) {
          html += "<div class='mazeObject space' data-x='" + objectX + "' data-y='" + objectY + "'></div>";
          if (mazeTrack.length > trackItem + 1) {
            trackItem++;
          }
        } else {
          html += "<div class='mazeObject wall' data-x='" + objectX + "' data-y='" + objectY + "'></div>";
        }
        if (objectX === mazeObjectPerLine) {
          objectX = 0;
          objectY++;
        } else {
          objectX++;
        }
      }
      html += "</div>";
      return $("#maze").append(html);
    };
    robotToJson = function() {
      var a, returnJSON;
      returnJSON = "[";
      $.each($(".robot"), function() {
        var id, x, y;
        id = $(this).attr("data-id");
        x = $(this).attr("data-x");
        y = $(this).attr("data-y");
        return returnJSON += '{"id":' + id + ', "x": ' + x + ', "y": ' + y + ' },';
      });
      a = returnJSON.length;
      returnJSON = returnJSON.substring(0, a - 1);
      returnJSON += "]";
      return returnJSON;
    };
    mazeToJson = function() {
      var a, objectX, objectY, objectsPerLine, returnJSON, txtMazeHeight, txtMazeWidth;
      maze = $("#maze");
      txtMazeHeight = $("#txtMazeHeight").val();
      txtMazeWidth = $("#txtMazeWidth").val();
      objectsPerLine = parseInt(txtMazeWidth) - 1;
      objectY = 0;
      objectX = 0;
      returnJSON = '{';
      returnJSON += '    "width":' + txtMazeWidth;
      returnJSON += '    ,"height":' + txtMazeHeight;
      returnJSON += '    ,"track": [';
      $.each($(".mazeObject"), function() {
        var objectValue;
        if ($(this).hasClass("space")) {
          objectValue = "space";
          returnJSON += '{"x": ' + objectX + ', "y": ' + objectY + ' },';
        }
        if (objectX === objectsPerLine) {
          objectX = 0;
          return objectY++;
        } else {
          return objectX++;
        }
      });
      a = returnJSON.length;
      returnJSON = returnJSON.substring(0, a - 1);
      returnJSON += "    ]";
      returnJSON += "}";
      return returnJSON;
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
    $("#btnMazeExport").click(function() {
      return $("#txtMazeJson").val(mazeToJson());
    });
    $("#addRobot").click(function() {
      chat.server.addRobot();
      return console.log("adicionando robo");
    });
    $("#btnTask").click(function() {
      return chat.server.runRobots(10);
    });
    return createMaze(readMap("simple"));
  });

}).call(this);
