(function() {
  this.Hub = (function() {
    var asyncRobotHub, startExplore;

    function Hub() {}

    asyncRobotHub = $.connection.asyncRobotHub;

    $.connection.hub.start().done(function() {
      return console.log("hub carregado de tiro porrada e bomba");
    });

    asyncRobotHub.client.setRobotPosition = function(id, x, y) {
      maze.setRobotPosition(id, x, y);
      return console.log("robot " + id + " " + x + " " + y);
    };

    startExplore = function(maze, robots) {
      return asyncRobotHub.server.runRobot(maze, robots);
    };

    return Hub;

  })();

}).call(this);
