window.dragElement = (elmnt) => {
    //var elmnt = document.getElementById('myCanvas');
    var pos1 = 0, pos2 = 0, pos3 = 0, pos4 = 0;

    /* otherwise, move the DIV from anywhere inside the DIV:*/
    elmnt.onmousedown = dragMouseDown;

    function dragMouseDown(e) {
        e = e || window.event;
        e.preventDefault();
        // get the mouse cursor position at startup:
        pos3 = e.clientX;
        pos4 = e.clientY;
        document.onmouseup = closeDragElement;
        // call a function whenever the cursor moves:
        document.onmousemove = elementDrag;
    }

    function elementDrag(e) {
        e = e || window.event;
        e.preventDefault();
        // calculate the new cursor position:
        pos1 = pos3 - e.clientX;
        pos2 = pos4 - e.clientY;
        pos3 = e.clientX;
        pos4 = e.clientY;
        // set the element's new position:
        elmnt.style.top = (elmnt.offsetTop - pos2) + "px";
        elmnt.style.left = (elmnt.offsetLeft - pos1) + "px";
    }

    function closeDragElement() {
        /* stop moving when mouse button is released:*/
        document.onmouseup = null;
        document.onmousemove = null;
    }
    return true;
};

window.cropImage = (i, j) => {
    debugger;
    //var canvas = document.getElementById('myCanvas');
    var canvas = document.createElement("canvas");
    //canvas.setAttribute('id', "myCanvas");
    canvas.setAttribute('width', 32);
    canvas.setAttribute('height', 48);
    var context = canvas.getContext('2d');
    var imageObj = new Image();

    imageObj.onload = function () {
        // draw cropped image
        var sourceX = i*32;
        var sourceY = j*48;
        var sourceWidth = 32;
        var sourceHeight = 48;
        var destWidth = sourceWidth;
        var destHeight = sourceHeight;
        var destX = 0;
        var destY = 0;

        context.drawImage(imageObj, sourceX, sourceY, sourceWidth, sourceHeight, destX, destY, destWidth, destHeight);
    };
    imageObj.src = 'images/Tiles.png';
    document.body.appendChild(canvas);
    dragElement(canvas);
};