let userData = null;

window.addEventListener('DOMContentLoaded', () => {
    userData = JSON.parse(localStorage.getItem('userData'));

    if (userData != null) {
        document.getElementById('guest').style.display = 'none';
        const addBtn = document.querySelector('#addForm .add');
        addBtn.disabled = false;
        document.getElementById('userEmail').textContent = userData.email;
    } else {
        document.getElementById('user').style.display = 'none';
    }

    const loadBtn = document.querySelector('.load');
    loadBtn.addEventListener('click', loadData);

    const addForm = document.getElementById('addForm');
    addForm.addEventListener('submit', onCreateSubmit);

    const logoutBtn = document.getElementById('logout');
    logoutBtn.addEventListener('click', onLogout);
});

async function onLogout() {
    const data = JSON.parse(localStorage.getItem('userData'));
    const token = data.token;

    const url = 'http://localhost:3030/users/logout';

    try {
        const response = await fetch(url, {
            method: 'GET',
            headers: {
                'X-Authorization': token
            }
        });

        if (response.ok != true) {
            const err = await response.json();
            throw new Error(err.message);
        }

        localStorage.clear();
        window.location.pathname = '/05.Fisher-Game/src/index.html';
    } catch (error) {
        alert(error.message);
    }
}

async function onCreateSubmit(ev) {
    ev.preventDefault();
    if (!userData) {
        window.location.pathname = '/05.Fisher-Game/src/login.html'
    }

    const formData = new FormData(ev.target);

    const data = [...formData.entries()].reduce((acc, [key, value]) => Object.assign(acc, { [key]: value }), {});

    try {
        if (Object.values(data).some(x => x == '')) {
            throw new Error('All fields are required.');
        }

        const url = 'http://localhost:3030/data/catches';
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'X-Authorization': userData.token
            },
            body: JSON.stringify(data)
        });

        if (response.ok != true) {
            const err = await response.json();
            throw new Error(err.message);
        }

        ev.target.reset();
        loadData();
    } catch (error) {
        alert(error.message);
    }
}

async function loadData() {
    const url = 'http://localhost:3030/data/catches';
    const response = await fetch(url);

    const data = await response.json();

    let catches = document.getElementById('catches');
    catches.replaceChildren(...data.map(createCatchPreview));
}

function createCatchPreview(info) {
    const isOwner = userData && info._ownerId == userData.id;

    const divElement = document.createElement('div');
    divElement.className = 'catch';

    let anglerLabel = createElement('label');
    anglerLabel.textContent = 'Angler';
    let anglerInput = createElement('input', 'text', 'angler', `${info.angler}`);
    anglerInput.disabled = isOwner ? false : true;

    let weightLabel = createElement('label');
    weightLabel.textContent = 'Weight';
    let weightInput = createElement('input', 'text', 'weight', `${info.weight}`);
    weightInput.disabled = isOwner ? false : true;

    let speciesLabel = createElement('label');
    speciesLabel.textContent = 'Species';
    let speciesInput = createElement('input', 'text', 'species', `${info.species}`);
    speciesInput.disabled = isOwner ? false : true;

    let locationLabel = createElement('label');
    locationLabel.textContent = 'Location';
    let locationInput = createElement('input', 'text', 'location', `${info.location}`);
    locationInput.disabled = isOwner ? false : true;

    let baitLabel = createElement('label');
    baitLabel.textContent = 'Bait';
    let baitInput = createElement('input', 'text', 'bait', `${info.bait}`);
    baitInput.disabled = isOwner ? false : true;

    let captureLabel = createElement('label');
    captureLabel.textContent = 'Capture Time';
    let captureInput = createElement('input', 'number', 'captureTime', `${info.captureTime}`);
    captureInput.disabled = isOwner ? false : true;

    let updateBtn = createBtn('update', `${info._id}`, 'Update');
    updateBtn.disabled = isOwner ? false : true;

    let deleteBtn = createBtn('delete', `${info._id}`, 'Delete');
    deleteBtn.disabled = isOwner ? false : true;

    divElement.appendChild(anglerLabel);
    divElement.appendChild(anglerInput);

    divElement.appendChild(weightLabel);
    divElement.appendChild(weightInput);

    divElement.appendChild(speciesLabel);
    divElement.appendChild(speciesInput);

    divElement.appendChild(locationLabel);
    divElement.appendChild(locationInput);

    divElement.appendChild(baitLabel);
    divElement.appendChild(baitInput);

    divElement.appendChild(captureLabel);
    divElement.appendChild(captureInput);

    divElement.appendChild(updateBtn);
    divElement.appendChild(deleteBtn);

    return divElement;
}

function createElement(elType, type = '', classItem = '', value = '') {
    let element = document.createElement(elType);
    element.setAttribute('type', type);
    element.className = classItem;
    element.setAttribute('value', value);

    return element;
}

function createBtn(classItem, id, text) {
    let btn = document.createElement('button');
    btn.classList.add(classItem);
    btn.setAttribute('data-id', id);
    btn.textContent = text;

    return btn;
}