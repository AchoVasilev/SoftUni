function addItem() {
    let ulItemsElement = document.getElementById('items');

    let inputElement = document.getElementById('newItemText');

    let liElement = document.createElement('li');

    liElement.textContent = inputElement.value;

    ulItemsElement.appendChild(liElement);

    inputElement.value = '';
}