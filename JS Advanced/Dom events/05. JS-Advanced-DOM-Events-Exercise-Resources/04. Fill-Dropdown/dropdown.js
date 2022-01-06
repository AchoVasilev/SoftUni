function addItem() {
    let html = {
        text: document.getElementById('newItemText'),
        value: document.getElementById('newItemValue'),
        menu: document.getElementById('menu')
    };

    let option = document.createElement('option');

    // @ts-ignore
    option.textContent = html.text.value;
    // @ts-ignore
    option.value = html.value.value;

    // @ts-ignore
    html.text.value = '';
    // @ts-ignore
    html.value.value = '';

    html.menu.appendChild(option);
}