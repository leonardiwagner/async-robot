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
    
    
    

    
    
    $("#btnMazeMount").click(() ->
        createMaze()
    );

    $("#btnMazeImport").click(() ->
        createMaze(readMap("simple"))
    );

    
    readMap = (mapName) ->
        $.get("/Content/maze/" + mapName + ".html", (data) ->
          createMaze(JSON.parse(data));
        );
        undefined

    changeObject = (() ->
        for landObject in json
            if landObject.value is "space"
                $(".mazeObject[data-coordinate='" + landObject.x + "-" + landObject.y  + "']").removeClass("wall")
                $(".mazeObject[data-coordinate='" + landObject.x + "-" + landObject.y  + "']").addClass("space")
            else
                $(".mazeObject[data-coordinate='" + landObject.x + "-" + landObject.y  + "']").removeClass("space")
                $(".mazeObject[data-coordinate='" + landObject.x + "-" + landObject.y  + "']").addClass("wall")
    );

    createMaze = (jsonMaze) ->
        $("#maze").empty()

        if jsonMaze isnt undefined
            mazeWidth = jsonMaze.width
            mazeHeight = jsonMaze.height
            mazeTrack = jsonMaze.track
        else        
            mazeWidth = $("#txtMazeWidth").val()
            mazeHeight = $("#txtMazeHeight").val()
            mazeTrack = null

        mazeObjectPerLine = parseInt(mazeWidth) - 1
        mazeObjectCount = mazeWidth * mazeHeight

        trackItem = 0
        objectX = 0
        objectY = 0
        lastY = 0

        html = "<div class='line'>"
        for num in [0..mazeObjectCount - 1]
            if objectY isnt lastY
                lastY = objectY
                if lastY > 0
                    html += "</div>"
                html += "<div class='line'>"
            
            coordinate = objectX + "-" + objectY
            if mazeTrack isnt null and mazeTrack[trackItem].x is objectX and mazeTrack[trackItem].y is objectY
                html += "<div class='mazeObject space' data-coordinate='#{coordinate}'></div>"
                if mazeTrack.length > trackItem + 1
                    trackItem++
            else
                html += "<div class='mazeObject wall' data-coordinate='#{coordinate}'></div>"
                
            if objectX == mazeObjectPerLine
                objectX = 0
                objectY++
            else            
                objectX++

        html += "</div>"
        $("#maze").append(html)
    
    mazeToJson = () ->
        maze = $("#maze")
        txtMazeHeight = $("#txtMazeHeight").val()
        txtMazeWidth = $("#txtMazeWidth").val()

        objectsPerLine = parseInt(txtMazeWidth) - 1
        
        objectY = 0
        objectX = 0

        returnJSON = '{'
        returnJSON += '    "width":' + txtMazeWidth
        returnJSON += '    ,"height":' + txtMazeHeight
        returnJSON += '    ,"track": ['


        $.each($(".mazeObject"), () ->
            if $(this).hasClass("space")
                objectValue = "space"
                returnJSON += '{"x": ' + objectX + ', "y": ' + objectY + ' },'
            
            if objectX == objectsPerLine
                objectX = 0
                objectY++
            else            
                objectX++
        );
        a =returnJSON.length
        returnJSON = returnJSON.substring(0, a - 1)
        
        returnJSON += "    ]"
        returnJSON += "}"

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
    