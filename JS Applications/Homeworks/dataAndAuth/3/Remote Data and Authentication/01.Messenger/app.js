let sendButton = document.getElementById('submit');
sendButton.addEventListener('click', createMessage)
let refreshButton = document.getElementById('refresh');
refreshButton.addEventListener('click', getAllMessages);

let textArea = document.getElementById('messages');

async function getAllMessages() {
    try {
        let url = 'http://localhost:3030/jsonstore/messenger';
        let getMessagesResult = await fetch(url);
        let messages = await getMessagesResult.json();
        console.log(messages);

        let messageString = Object.values(messages)
            .map(message => `${message.author}: ${message.content}`)
            .join('\n');
        textArea.value = messageString;
    } catch (err) {
        console.error(err);
    }
}

async function createMessage() {
    try {
        let authorInput = document.querySelector('[name="author"]');
        let contentInput = document.querySelector('[name="content"]');
        let url = 'http://localhost:3030/jsonstore/messenger';

        let newMessage = {
            author: authorInput.value,
            content: contentInput.value
        };
        let createResponse = await fetch(url,
            {
                headers: {
                    'Content-Type': 'application/json'
                },
                method: 'Post',
                body: JSON.stringify(newMessage)
            });
        let createResult = await createResponse.json();
        console.log(createResult);
        let createdMessageString = `${createResult.author}: ${createResult.content}`;
        textArea.value = textArea.value + `\n${createdMessageString}`;
    } catch (err) {
        console.error(err);
    }
}