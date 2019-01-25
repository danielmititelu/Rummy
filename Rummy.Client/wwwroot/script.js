//import { Droppable } from '@shopify/draggable';

window.cropImage = (i, j) => {
    var tileWidth = 32;
    var tileHeight = 48;

    var canvas = document.createElement("canvas");
    canvas.setAttribute('class', "piece");
    canvas.setAttribute('width', tileWidth);
    canvas.setAttribute('height', tileHeight);
    var context = canvas.getContext('2d');
    var imageObj = new Image();

    imageObj.onload = function () {
        // draw cropped image
        var sourceX = j * tileWidth;
        var sourceY = i * tileHeight;
        var destX = 0;
        var destY = 0;

        context.drawImage(imageObj, sourceX, sourceY, tileWidth, tileHeight, destX, destY, tileWidth, tileHeight);
    };

    imageObj.src = 'images/Tiles.png';
    var dropzone = $('.dropzone:first');
    dropzone.append(canvas);
    dropzone.addClass('draggable-dropzone--occupied');

    var options = {
        draggable: '.piece',
        dropzone: '.dropzone'
    };
    var droppable = new Draggable.Droppable(document.querySelectorAll('.dropzone'), options);

    //var droppableOrigin;
    
    //droppable.on('drag:start', (evt) => {
    //    droppableOrigin = evt.originalSource.parentNode.dataset.dropzone;
    //});

    //droppable.on('droppable:dropped', (evt) => {
    //    if (droppableOrigin !== evt.dropzone.dataset.dropzone) {
    //        evt.cancel();
    //    }
    //});
    
    return true;
};