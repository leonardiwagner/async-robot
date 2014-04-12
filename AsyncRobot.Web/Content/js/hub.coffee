class @Hub
    constructor: () ->

    asyncRobotHub = $.connection.asyncRobotHub;

    $.connection.hub.start().done(
        () ->
            console.log "hub carregado de tiro porrada e bomba"
    );

    asyncRobotHub.client.setRobotPosition = (id, x, y) ->
        maze.setRobotPosition(id,x,y)
        console.log "robot #{id} #{x} #{y}"
    
    startExplore = (maze, robots) ->
        asyncRobotHub.server.runRobot(maze, robots)