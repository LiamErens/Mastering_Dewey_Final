window.blazorCheckPositions = () => {
    const listItems = document.querySelectorAll('.draggable-item');
    const order = [];
    
    listItems.forEach(item => {
        order.push(item.innerText);
    });

    console.log('Order from blazorCheckPositions:', order); // Log the order

    
    return order;
}

