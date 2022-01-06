const url = "http://localhost:3030/jsonstore/messenger";

function attachEvents() {
    const [nameField, msgField, sendBtn, refreshBtn] = document.querySelectorAll('input');
    sendBtn.addEventListener('click', prepMsg.bind(null, nameField, msgField));
    refreshBtn.addEventListener('click', showMsgs);

}

async function showMsgs() {
    try {
        const mssagesField = document.getElementById('messages');
        mssagesField.textContent = 'Loading ...';

        const data = await getMessages();
        mssagesField.textContent = '';

        data.forEach(m => mssagesField.textContent += `${m.author}: ${m.content}\n`);
    } catch {
        alert(err.message);
    }
}

function prepMsg(nameField, msgField) {
    if (nameField.value == '' || msgField.value == '') {
        alert('Provide information in both fields!');
    } else {
        try {
            const msg = {
                author: nameField.value,
                content: msgField.value
            }

            postMessage(msg);
            nameField.value = '';
            msgField.value = '';
        } catch {
            alert(err.message);
        }
    }
}

async function getMessages() {
    const res = await fetch(url);
    if (res.ok == false) {
        throw new Error(res.statusText);
    }
    const data = await res.json();
    return Object.values(data);
}

async function postMessage(msg) {
    const options = {
        method: 'post',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(msg)
    }

    const res = await fetch(url, options);
    if (res.ok == false) {
        throw new Error(res.statusText);
    }
    const data = await res.json();

    return data;
}

attachEvents();