function attachEvents() {
    const url = 'http://localhost:3030/jsonstore/messenger';

    const submit = document.getElementById('submit');
    const refresh = document.getElementById('refresh');

    submit.addEventListener('click', onSubmit);
    refresh.addEventListener('click', onRefresh);

    const name = document.querySelector('input[name=author]');
    const message = document.querySelector('input[name=content]');
    const textarea = document.getElementById('messages');

    async function onSubmit(e){
        data = {
            "author": name.value,
            "content": message.value,
        };

        
        const res = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        });

        name.value = '';
        message.value = '';
    }

    async function onRefresh(e){
        textarea.textContent = '';

        const res = await fetch(url);
        const data = await res.json();

        for (let mess of Object.values(data)) {
            textarea.textContent += `${mess['author']}: ${mess['content']}\n`;
        }
    }
}

attachEvents();