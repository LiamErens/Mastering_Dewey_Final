window.blazorCheckPositions = () => {
    const listItems = document.querySelectorAll('.dropList');
    const order = [];
    listItems.forEach(item => {
        order.push(item.innerText);
    });
    return order;
}
//