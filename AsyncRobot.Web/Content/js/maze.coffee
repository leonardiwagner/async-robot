$ ->
    chat = $.connection.asyncRobotHub;

    $.connection.hub.start().done(
        () ->
            console.log "hub carregado de tiro porrada e bomba"
    );

    chat.client.setRobotPosition = (x, y, robotId) ->
        $("#maze").removeClass("robot-" + robotId)
        $("#maze maze-item-" + x + "-" + y).addClass("robot-" + robotId)

    chat.client.mountMaze = (mazeJson) ->
        mazeJson = JSON.parse mazeJson
        
        lastY = 0;
        html = "<div class='line'>"
        for landObject in mazeJson
            if parseInt(landObject.y) isnt lastY
                lastY = parseInt(landObject.y)
                if lastY > 0
                    html += "</div>"
                html += "<div class='line'>"

            if landObject.value is "#"
                html += "<div class='wall'></div>"
            else if landObject.value is " "
                html += "<div class='space'></div>"
        
        html += "</div>"
        $("#maze").append(html)

    $("#createMaze").click(
        () ->
            chat.server.requestMazeCreation()
            console.log "mandando mensagem pro servidor"
    );

    $("#addRobot").click(
        () ->
            chat.server.addRobot()
            console.log "adicionando robo"
    );