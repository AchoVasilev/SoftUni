function attachEvents() {
    const refreshBtn = document.getElementById('refresh');
    refreshBtn.addEventListener('click', loadMessages);

    const submitBtn = document.getElementById('submit');
    submitBtn.addEventListener('click', onSubmit);
    loadMessages();
}

const authorInput = document.querySelector('[name="author"]');
const contentInput = document.querySelector('[name="content"]');
const list = document.getElementById('messages');

attachEvents();

async function onSubmit() {
    const author = authorInput.value;
    const content = contentInput.value;

    if (author.length == 0 || content.length == 0) {
        alert('Inputs cannot be empty.');
        return;
    }

    const result = await createMessage({ author, content });

    contentInput.value = '';
    list.value += '\n' + `${author}: ${content}`;
}

async function loadMessages() {
    const url = 'http://localhost:3030/jsonstore/messenger';
    const response = await fetch(url);

    const data = await response.json();
    const messages = Object.values(data);

    list.value = messages.map(m => `${m.author}: ${m.content}`)
        .join('\n');
}

async function createMessage(message) {
    const url = 'http://localhost:3030/jsonstore/messenger';
    const options = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(message)
    };

    const response = await fetch(url, options);
    const result = await response.json();

    return result;
}