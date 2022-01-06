function attachEvents() {
    const load = document.getElementById('btnLoad');
    const create = document.getElementById('btnCreate');
    const phonebook = document.getElementById('phonebook');
    const person = document.getElementById('person');
    const phone = document.getElementById('phone');

    load.addEventListener('click', onLoad);
    create.addEventListener('click', onCreate);

    async function onLoad(e) {
        phonebook.innerHTML = '';

        const url = 'http://localhost:3030/jsonstore/phonebook';

        const res = await fetch(url);
        const data = await res.json();

        for (let obj of Object.values(data)) {
            let person = document.createElement('li');
            person.setAttribute('name', `${obj['_id']}`);
            person.textContent = `${obj.person}: ${obj.phone}`;
            let del = document.createElement('button');
            del.textContent = 'Delete';
            del.addEventListener('click', onDelete);
            person.appendChild(del);
            phonebook.appendChild(person);
        }
    }

    async function onCreate(e) {
        const url = 'http://localhost:3030/jsonstore/phonebook';

        data = {
            "person": person.value,
            "phone": phone.value,
        };

        
        const res = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        });

        person.value = '';
        phone.value = '';
        
        load.dispatchEvent(new Event('click'));
    }

    async function onDelete(e) {
        let key = e.target.parentElement.getAttribute('name');
        const url = `http://localhost:3030/jsonstore/phonebook/${key}`;

        const res = await fetch(url, {
            method: 'DELETE',
        });

        load.dispatchEvent(new Event('click'));
    }
}

attachEvents();