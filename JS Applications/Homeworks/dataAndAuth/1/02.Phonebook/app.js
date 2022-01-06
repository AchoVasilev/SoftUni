const phonebook = document.getElementById('phonebook');

function attachEvents() {
    const [loadBtn, personInput, phoneInput, createBtn] = document.querySelectorAll('button, input');
    loadBtn.addEventListener('click', loadPhones);
    createBtn.addEventListener('click', createRecord.bind(null, personInput, phoneInput));
}

async function createRecord(personInput, phoneInput) {
    phonebook.textContent = 'Loading ...';
    if (personInput.value == '' || phoneInput.value == '') {
        alert('Please enter name and phone!')
    } else {
        const record = {
            person: personInput.value,
            phone: phoneInput.value
        }
        try {
            await postPhone(record);
            loadPhones();
        } catch {
            alert(err.message);
        }
        personInput.value = '';
        phoneInput.value = '';
    }
}


async function loadPhones() {
    phonebook.textContent = "Loading ...";
    try {
        const data = await getPhones();
        phonebook.replaceChildren();
        data.forEach(e => phonebook.appendChild(e));
    } catch {
        alert(err.message);
    }

}


async function getPhones() {
    const url = 'http://localhost:3030/jsonstore/phonebook';
    try {
        const res = await fetch(url);
        if (res.ok == false) {
            throw new Error(res.statusText);
        }
        const data = await res.json();
        return Object.values(data).map(r => {
            const li = document.createElement('li');
            const deleteBtn = document.createElement('button');
            deleteBtn.textContent = "Delete";
            li.textContent = `${r.person}: ${r.phone}`;
            li.appendChild(deleteBtn);
            deleteBtn.addEventListener('click', deleteRecord.bind(null, r._id))

            return li;
        });
    } catch {
        alert(err.message);
    }
}

async function postPhone(record) {
    const url = 'http://localhost:3030/jsonstore/phonebook';

    const options = {
        method: 'post',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(record)
    }

    const res = await fetch(url, options);
    if (res.ok == false) {
        throw new Error(res.statusText);
    }

    const data = res.json();

    return data;
}


async function deleteRecord(id) {
    const url = `http://localhost:3030/jsonstore/phonebook/${id}`;

    const options = {
        method: 'delete'
    }
    phonebook.textContent = "Loading ...";
    try {
        const res = await fetch(url, options);
        if (res.ok == false) {
            throw new Error(res.statusText);
        }
        const data = res.json();
        loadPhones();
        return data;
    } catch {
        alert(err.message);
    }
}

attachEvents();