// when <script> is before <body>, in <head>, wait for the page/DOM to load first!
window.addEventListener('DOMContentLoaded', () => {
    const guest = document.getElementById('guest');
    const user = document.getElementById('user');
    const indicator = document.querySelector('p.email').children[0];
    const updt = Array.from(document.querySelectorAll('button.update'));
    const del = Array.from(document.querySelectorAll('button.delete'));
    const add = document.querySelector('button.add');
    const addForm = document.getElementById('addForm');
    addForm.addEventListener('submit', onAdd);
    const load = document.querySelector('button.load');
    load.addEventListener('click', onLoad);
    document.getElementById('catches').innerHTML = '';
    
    const logout = user.children[0];

    if (sessionStorage.getItem('accessToken') != null){ // logged
        guest.style.display = 'none';
        user.style.display = 'inline-block';
        logout.addEventListener('click', onLogout);
        indicator.textContent = `${sessionStorage.getItem('indicator')}`;
        add.disabled = false;

    } else { // not logged
        guest.style.display = 'inline-block';
        user.style.display = 'none';
        updt.forEach(butt => {
            butt.disabled = true;
        });
        del.forEach(butt => {
            butt.disabled = true;
        });
        add.disabled = true;
    }
}); 

async function onLoad(e){
    const catches = document.getElementById('catches');
    catches.innerHTML = '';
    const url = 'http://localhost:3030/data/catches';
    const res = await fetch(url);
    const data = await res.json(); // array of objects
    console.log(data);

    for (let obj of data){
        catches.innerHTML += `<div class="catch">
        <label>Angler</label>
        <input type="text" class="angler" value="${obj.angler}">
        <label>Weight</label>
        <input type="text" class="weight" value="${obj.weight}">
        <label>Species</label>
        <input type="text" class="species" value="${obj.species}">
        <label>Location</label>
        <input type="text" class="location" value="${obj.location}">
        <label>Bait</label>
        <input type="text" class="bait" value="${obj.bait}">
        <label>Capture Time</label>
        <input type="number" class="captureTime" value="${obj.captureTime}">
        <button class="update" owner-id="${obj['_ownerId']}">Update</button>
        <button class="delete" owner-id="${obj['_ownerId']}">Delete</button>
        </div>`;
    }

    const updt = Array.from(document.querySelectorAll('button.update'));
    const del = Array.from(document.querySelectorAll('button.delete'));

    updt.forEach(butt => {
        butt.addEventListener('click', onUpdate);
        if (sessionStorage.getItem('_id') == butt.getAttribute('owner-id')){
            butt.disabled = false;
        } else {
            butt.disabled = true;
        }
    });
    del.forEach(butt => {
        butt.addEventListener('click', onDelete);
        if (sessionStorage.getItem('_id') == butt.getAttribute('owner-id')){
            butt.disabled = false;
        } else {
            butt.disabled = true;
        }
    });
}

// TODO
async function onUpdate(e){

}

// TODO
async function onDelete(e){

}


async function onAdd(e){
    e.preventDefault();
    const formData = new FormData(e.target);
    const url = 'http://localhost:3030/data/catches';

    await fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "X-Authorization": sessionStorage.getItem('accessToken')
        },
        body: JSON.stringify({
            "angler": formData.get('angler'),
            "weight": formData.get('weight'),
            "species": formData.get('species'),
            "location": formData.get('location'),
            "bait": formData.get('bait'),
            "captureTime": formData.get('captureTime')
        })
    });

    e.target.reset();
}

async function onLogout(e){
    // send authorized get request
    const url = 'http://localhost:3030/users/logout';
    const res = await fetch(url, {
        method: "GET",
        headers: {
            "X-Authorization": sessionStorage.getItem('accessToken')
        }
    });

    if (res.status == 204) {
        alert('Logged out!');
        sessionStorage.clear();
    } else {
        alert('Error with auth...');
    }

    location.href = 'index.html';
}