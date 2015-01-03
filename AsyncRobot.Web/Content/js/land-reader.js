var createMaze = function (jsonMaze) {
    var mazeWidth = jsonMaze.width;
    var mazeHeight = jsonMaze.height;
    var mazeTrack = jsonMaze.track;

    var mazeObjectPerLine = parseInt(mazeWidth) - 1;
    var mazeObjectCount = mazeWidth * mazeHeight;
    var trackItem = 0;
    var objectX = 0;
    var objectY = 0;
    var lastY = 0;
    var html = "<div class='line'>";
    for (var i = 0; i < mazeObjectCount - 1; i++) {
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
    $("#maze").append(html);
};


createMaze(landJson);
