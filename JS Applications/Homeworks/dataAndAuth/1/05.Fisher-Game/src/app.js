let userData = null;

window.addEventListener('DOMContentLoaded', () => {
    const user = document.getElementById('user');
    const guest = document.getElementById('guest');
    const addBTN = document.querySelector('#addForm .add');
    const loadBTN = document.querySelector('.load');
    const logoutBTN = document.getElementById('logout');
    const nameText = document.querySelector('p span');
    const addForm = document.querySelector('#addForm');

    document.getElementById('catches').replaceChildren();

    userData = JSON.parse(sessionStorage.getItem('userData'));
    if (userData != null) {
        guest.style.display = 'none';
        user.style.display = 'inline-block';
        addBTN.disabled = false;
        nameText.textContent = userData.email;
    } else {
        guest.style.display = 'inline-block';
        user.style.display = 'none';
        logoutBTN.style.display = 'none';
        nameText.textContent = 'guest';
    }

    loadBTN.addEventListener('click', loadData);

    addBTN.addEventListener('click', onCreateSubmit.bind(null, loadBTN));

    logoutBTN.addEventListener('click', logout.bind(null, user, guest, addBTN, logoutBTN,loadBTN, nameText));
});


async function logout(user, guest, addBTN, logoutBTN, loadBTN,nameText, ev) {
    ev.preventDefault();
    userData = JSON.parse(sessionStorage.getItem('userData'));

    const url = 'http://localhost:3030/users/logout';
    if (userData != null) {
        try {
            const res = await fetch(url, {
                method: 'get',
                headers: {
                    'X-Authorization': userData.token
                }
            });
            if (res.ok != true) {
                const error = await res.json();
                throw new Error(error.message);
            }
            sessionStorage.setItem('userData', 'null');
            userData = null;
            user.style.display = 'none';
            guest.style.display = '';

            addBTN.disabled = true;
            logoutBTN.style.display = 'none';
            nameText.textContent = 'guest';
            loadBTN.dispatchEvent(new Event('click'));

            // window.location = '/index.html';
        } catch (err) {
            alert(err.message);
        }
    }
}

async function onCreateSubmit(loadBTN, ev) {
    ev.preventDefault();
    userData = JSON.parse(sessionStorage.getItem('userData'));

    if (!userData) {
        window.location = '/05.Fisher-Game/login.html';
        return;
    }

    const formData = new FormData(addForm);

    const data = [...formData.entries()].reduce((a, [k, v]) => Object.assign(a, { [k]: v }), {});

    try {
        if (Object.values(data).some(x => x == '')) {
            throw new Error('All fields are required!')
        }
        const res = await fetch('http://localhost:3030/data/catches', {
            method: 'post',
            headers: {
                'Content-Type': 'application/json',
                'X-Authorization': userData.token
            },
            body: JSON.stringify(data)
        });
        if (res.ok != true) {
            const error = await res.json();
            throw new Error(error.message);
        }
        addForm.reset();
        loadBTN.dispatchEvent(new Event('click'));
    } catch (err) {
        alert(err.message);
    }
}

async function loadData(ev) {
    ev.target.disabled = true;
    ev.target.textContent = "Loading...";
    const res = await fetch('http://localhost:3030/data/catches');
    const data = await res.json();

    document.getElementById('catches').replaceChildren(...data.map(createPreview));
    ev.target.textContent = "Load";
    ev.target.disabled = false;

}

function createPreview(item) {
    const isOwner = (userData && item._ownerId == userData.id);
    const element = document.createElement('div');
    const updateBTN = createField('button', { 'class': 'update', 'data-id': `${item._id}`, 'disabled': `${!isOwner ? 'disabled' : ''}` }, 'Update');
    const deleteBTN = createField('button', { 'class': 'delete', 'data-id': `${item._id}`, 'disabled': `${!isOwner ? 'disabled' : ''}` }, 'Delete');
    element.className = 'catch';
    element.innerHTML = `<label>Angler</label>
<input type="text" class="angler" value="${item.angler}" ${!isOwner ? 'disabled' : ''}>
<label>Weight</label>
<input type="text" class="weight" value="${item.weight}" ${!isOwner ? 'disabled' : ''}>
<label>Species</label>
<input type="text" class="species" value="${item.species}" ${!isOwner ? 'disabled' : ''}>
<label>Location</label>
<input type="text" class="location" value="${item.location}" ${!isOwner ? 'disabled' : ''}>
<label>Bait</label>
<input type="text" class="bait" value="${item.bait}" ${!isOwner ? 'disabled' : ''}>
<label>Capture Time</label>
<input type="number" class="captureTime" value="${item.captureTime}" ${!isOwner ? 'disabled' : ''}>`

    element.appendChild(updateBTN);
    element.appendChild(deleteBTN);

    deleteBTN.addEventListener('click', deleteCatch);
    updateBTN.addEventListener('click', updateCatch);

    return element;
}

async function deleteCatch(ev) {
    const loadBTN = document.querySelector('.load');
    document.getElementById('catches').innerHTML = "Loading..."
    try {
        const res = await fetch('http://localhost:3030/data/catches/' + ev.target.getAttribute('data-id'), {
            method: 'delete',
            headers: {
                'Content-Type': 'application/json',
                'X-Authorization': userData.token
            },

        });
        if (res.ok != true) {
            const error = await res.json();
            throw new Error(error.message);
        }
        loadBTN.dispatchEvent(new Event('click'));
    } catch (err) {
        alert(err.message);
    }

};

async function updateCatch(ev) {
    const loadBTN = document.querySelector('.load');
    const myCatch = ev.target.parentElement;

    const data = {
        "angler": myCatch.querySelector('.angler').value,
        "weight": myCatch.querySelector('.weight').value,
        "species": myCatch.querySelector('.species').value,
        "location": myCatch.querySelector('.location').value,
        "bait": myCatch.querySelector('.bait').value,
        "captureTime": myCatch.querySelector('.captureTime').value
    }
    document.getElementById('catches').innerHTML = "Loading..."


    try {
        if (Object.values(data).some(x => x == '')) {
            throw new Error('All fields are required!')
        }
        const res = await fetch('http://localhost:3030/data/catches/' + ev.target.getAttribute('data-id'), {
            method: 'put',
            headers: {
                'Content-Type': 'application/json',
                'X-Authorization': userData.token
            },
            body: JSON.stringify(data)
        });
        if (res.ok != true) {
            const error = await res.json();
            throw new Error(error.message);
        }
        loadBTN.dispatchEvent(new Event('click'));
    } catch (err) {
        alert(err.message);
    }


};

function createField(type, attr, ...content) {
    const element = document.createElement(type);

    for (let value of content) {
        if (typeof value == 'number' || typeof value == 'string') {
            value = document.createTextNode(value);
        }
        element.appendChild(value);
    }
    for (let prop in attr) {
        if (prop == "class") {
            element.classList.add(attr[prop]);
        } else if (prop == 'data-id') {
            element.setAttribute('data-id', attr[prop])
        }
        else {
            element[prop] = attr[prop];
        }
    }

    return element;
}