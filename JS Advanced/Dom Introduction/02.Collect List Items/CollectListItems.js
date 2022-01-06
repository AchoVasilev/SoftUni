function extractText() {
    let items = document.querySelectorAll('ul#items li');

    let textArea = document.querySelector('#result');

    for (const item of items) {
        textArea.value += item.textContent + '\n';
    }
}