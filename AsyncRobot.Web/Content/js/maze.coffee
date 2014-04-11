$ ->
    chat = $.connection.asyncRobotHub;

    $.connection.hub.start().done(
        () ->
            console.log "hub carregado de tiro porrada e bomba"
    );

    chat.client.printTask = (taskString) ->
        console.log taskString

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

    mazeObjectSizeWidth = 10
    mazeObjectSizeHeight = 10

    maze = $("#maze")
    txtMazeHeight = $("#txtMazeHeight").val()
    txtMazeWidth = $("#txtMazeWidth").val()


    $("#btnMazeMount").click(
        () ->
            mazeObjectCount = parseInt(txtMazeHeight) * parseInt(txtMazeWidth)
            maze.css('width', (parseInt(txtMazeWidth) * mazeObjectSizeWidth) + "px")
            maze.css('height', (parseInt(txtMazeHeight) * mazeObjectSizeHeight)  + "px")
            for num in [0..mazeObjectCount]
                maze.append("<div class='mazeObject wall'></div>")
                
            createMazeObjectHandlers()
    );
    
    $("#btnMazeExport").click(() ->
        mazeToJSON()
    );

    mazeToJSON = () ->
        objectsPerLine = parseInt(txtMazeWidth) - 1
        
        objectY = 0
        objectX = 0
        returnJSON = "["

        $.each($(".mazeObject"), () ->
            if $(this).hasClass("wall")
                objectValue = "wall"
            else if $(this).hasClass("space")
                objectValue = "space"
            
            returnJSON += '{"x": ' + objectX + ', "y": ' + objectY + ', "value": "' + objectValue + '" },'
            
            if objectX == objectsPerLine
                objectX = 0
                objectY++
            else            
                objectX++
        );
        a =returnJSON.length
        returnJSON = returnJSON.substring(0, a - 1)
        returnJSON += "]"

        console.log JSON.parse(returnJSON)


    
    createMazeObjectHandlers = () ->
        $(".mazeObject").click(
            () ->
                if $(this).hasClass('wall')
                    $(this).removeClass('wall')
                    $(this).addClass('space')
                else if $(this).hasClass('space')
                    $(this).removeClass('wall')
                    $(this).addClass('space')
        );

    $("#addRobot").click(
        () ->
            chat.server.addRobot()
            console.log "adicionando robo"
    );

    $("#btnTask").click(
        () ->
            chat.server.runRobots(10)
    );
    