$ ->
    chat = $.connection.asyncRobotHub;

    $.connection.hub.start().done(
        () ->
            console.log "hub carregado de tiro porrada e bomba"
    );

    chat.client.mountMaze = (mazeJson) ->
        mazeJson = JSON.parse mazeJson

        lastY = 0;
        for landObject in mazeJson
            if parseInt(landObject.y) isnt lastY
            console.log landObject.value

    $("#createMaze").click(
        () ->
            chat.server.requestMazeCreation()
            console.log "mandando mensagem pro servidor"
    );