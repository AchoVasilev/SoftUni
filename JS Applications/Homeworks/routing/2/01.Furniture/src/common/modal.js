const element = document.createElement('div');
element.id = 'overlay';

element.innerHTML = `
<div id="modal">
    </div>
`;

let msg = element.querySelector('#modal');

export function showModal(message) {
    document.body.appendChild(element);
    msg.textContent = message;

setTimeout(clear, 7000);

}

export function clear() {
    element.remove();
}

