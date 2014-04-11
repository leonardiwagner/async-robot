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

    

    mazeObjectSizeWidth = 10
    mazeObjectSizeHeight = 10

    maze = $("#maze")
    
    chat.client.mountMaze = (mazeJson) ->
        mazeJson = JSON.parse mazeJson
        $("#maze").empty()
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
    
    $("#btnMazeImport").click(() ->
        json = JSON.parse($("#txtMazeJson").val())
        for landObject in json
            if landObject.value is "space"
                $(".mazeObject[data-coordinate='" + landObject.x + "-" + landObject.y  + "']").removeClass("wall")
                $(".mazeObject[data-coordinate='" + landObject.x + "-" + landObject.y  + "']").addClass("space")
            else
                $(".mazeObject[data-coordinate='" + landObject.x + "-" + landObject.y  + "']").removeClass("space")
                $(".mazeObject[data-coordinate='" + landObject.x + "-" + landObject.y  + "']").addClass("wall")
    );
    

    $("#btnMazeMount").click(
        () ->
            maze.empty()
            txtMazeHeight = $("#txtMazeHeight").val()
            txtMazeWidth = $("#txtMazeWidth").val()

            objectsPerLine = parseInt(txtMazeWidth) - 1
            
            objectY = 0
            objectX = 0
            
            mazeObjectCount = parseInt(txtMazeHeight) * parseInt(txtMazeWidth)
            maze.css('width', (parseInt(txtMazeWidth) * mazeObjectSizeWidth) + "px")
            maze.css('height', (parseInt(txtMazeHeight) * mazeObjectSizeHeight)  + "px")
            for num in [0..mazeObjectCount]
                coordinate = objectX + "-" + objectY
                maze.append("<div class='mazeObject wall' data-coordinate='#{coordinate}'></div>")
                
                if objectX == objectsPerLine
                    objectX = 0
                    objectY++
                else            
                    objectX++
                
            createMazeObjectHandlers()
    );
    
    $("#btnMazeExport").click(() ->
        mazeToJSON()
    );

    


    mazeToJSON = () ->
        maze = $("#maze")
        txtMazeHeight = $("#txtMazeHeight").val()
        txtMazeWidth = $("#txtMazeWidth").val()

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

        $("#txtMazeJson").val(returnJSON)


    
    createMazeObjectHandlers = () ->
        $(".mazeObject").click(() ->
            createMazeObjectHandler(this)
        );

    createMazeObjectHandler = (obj) ->
        #$(".mazeObject").click(() ->
        if $(obj).hasClass('wall')
            $(obj).removeClass('wall')
            $(obj).addClass('space')
        else if $(obj).hasClass('space')
            $(obj).removeClass('space')
            $(obj).addClass('wall')
        #);
        

    $("#addRobot").click(
        () ->
            chat.server.addRobot()
            console.log "adicionando robo"
    );

    $("#btnTask").click(
        () ->
            chat.server.runRobots(10)
    );
    