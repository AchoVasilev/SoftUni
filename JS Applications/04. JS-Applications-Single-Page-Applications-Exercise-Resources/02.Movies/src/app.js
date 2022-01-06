import { showHome } from '../views/home.js';
import { showLogin } from '../views/login.js';
import { showRegister } from '../views/register.js';
import { makeRequest } from './requests.js';

const views = {
    'home-link': showHome,
    'login-link': showLogin,
    'register-link': showRegister,
};

const logoutBtn = document.getElementById('logout-btn');
logoutBtn.addEventListener('click', onLogout);

const nav = document.querySelector('nav');
nav.addEventListener('click', onClick);

function onClick(ev) {
    const view = views[ev.target.id];

    if (typeof view == 'function') {
        ev.preventDefault();
        view();
    }
}

updateNav();
showHome();

export function updateNav() {
    const userData = JSON.parse(sessionStorage.getItem('userData'));

    if (userData != null) {
        nav.querySelector('#welcome-msg').textContent = `Welcome, ${userData.email}`;
        [...nav.querySelectorAll('.user')].forEach(e => e.style.display = 'block');
        [...nav.querySelectorAll('.guest')].forEach(e => e.style.display = 'none');
    } else {
        [...nav.querySelectorAll('.guest')].forEach(e => e.style.display = 'block');
        [...nav.querySelectorAll('.user')].forEach(e => e.style.display = 'none');
    }
}

async function onLogout(ev) {
    ev.preventDefault();
    ev.stopImmediatePropagation();

    const { token } = JSON.parse(sessionStorage.getItem('userData'));

    const url = 'http://localhost:3030/users/logout';
    const response = await makeRequest(url, 'GET', undefined, token);
    // const response = await fetch(url, {
    //     headers: {
    //         'X-Authorization': token
    //     }
    // });

    sessionStorage.removeItem('userData');
    updateNav();
    showLogin();
}