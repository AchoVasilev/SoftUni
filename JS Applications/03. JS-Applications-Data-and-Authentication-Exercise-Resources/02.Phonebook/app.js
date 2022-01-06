const list = document.getElementById('phonebook');
const personInput = document.getElementById('person');
const phoneInput = document.getElementById('phone');

function attachEvents() {
    const loadBtn = document.getElementById('btnLoad');
    loadBtn.addEventListener('click', loadContacts);

    const createBtn = document.getElementById('btnCreate');
    createBtn.addEventListener('click', onCreate);

    list.addEventListener('click', onDelete);

    loadContacts();
}

attachEvents();

async function loadContacts() {
    let btn = document.getElementById('btnLoad');
    btn.disabled = true;
    btn.style.background = 'grey';
    const url = 'http://localhost:3030/jsonstore/phonebook';
    const response = await fetch(url);
    const data = await response.json();

    list.replaceChildren();
    Object.values(data).map(createContactElement).forEach(i => list.appendChild(i));

    btn.disabled = false;
    btn.style.background = '#45A049';
}

function createContactElement(contact) {
    let liElement = document.createElement('li');
    liElement.textContent = `${contact.person}: ${contact.phone}`;

    let deleteBtn = document.createElement('button');
    deleteBtn.textContent = 'Delete';
    deleteBtn.setAttribute('data-id', `${contact._id}`);

    liElement.appendChild(deleteBtn);

    return liElement;
}

async function onCreate() {
    let person = personInput.value;
    let phone = phoneInput.value;

    if (person.length == 0 || phone.length == 0) {
        alert('Inputs cannot be empty.');
        return;
    }

    let contact = {
        person,
        phone
    };

    const response = await createContact(contact);
    let contactElement = createContactElement(response);

    list.appendChild(contactElement);
}

async function createContact(contact) {
    const url = 'http://localhost:3030/jsonstore/phonebook';
    const response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(contact)
    });

    const data = response.json();

    return data;
}

async function onDelete(ev) {
    const btnId = ev.target.dataset.id;

    if (btnId == undefined) {
        return;
    }

    await deleteContact(btnId);
    ev.target.parentElement.remove();
}

async function deleteContact(contactId) {
    const url = `http://localhost:3030/jsonstore/phonebook/${contactId}`;
    const response = await fetch(url, {
        method: 'DELETE'
    });
    
    let deletedContact = await response.json();

    return deletedContact;
}