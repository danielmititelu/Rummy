window.mountDraggable = (i, j) => {
    const containers = document.querySelectorAll('.swappable-container');

    const swappable = new Draggable.Swappable(containers, {
        draggable: '.cell',
        plugins: [Draggable.Plugins.Snappable]
    });

    swappable.on('drag:start', (evt) => {
        pos = {
            x: evt.sensorEvent.clientX,
            y: evt.sensorEvent.clientY
        };
        console.log('drag start:', pos.x, pos.y);
    });
    
    return true;
};