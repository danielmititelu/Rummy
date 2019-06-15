window.mountDraggable = () => {
    const containers = document.querySelectorAll('.swappable-container');

    const swappable = new Draggable.Swappable(containers, {
        draggable: '.cell',
        plugins: [Draggable.Plugins.Snappable]
    });

    swappable.on('swappable:stop', (evt) => {
        var pieceId = evt.data.dragEvent.data.source.firstElementChild.id;
        console.log('target:', pieceId);
        DotNet.invokeMethodAsync('Rummy', 'EndTurnWithPiece', pieceId)
            .then(data => {
                console.log(data);
            });
    });
    
    return true;
};