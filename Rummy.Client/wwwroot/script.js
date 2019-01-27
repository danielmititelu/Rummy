window.cropImage = (i, j) => {
    const containers = document.querySelectorAll('.swappable-container');

    const swappable = new Draggable.Swappable(containers, {
        draggable: '.cell',
        plugins: [Draggable.Plugins.Snappable]
    });
    
    return true;
};