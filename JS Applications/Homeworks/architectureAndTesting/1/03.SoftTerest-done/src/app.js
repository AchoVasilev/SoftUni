import { showHome } from '../src/views/home.js';
import { showLogin } from '../src/views/login.js';
import { showRegister } from '../src/views/register.js';
import { showCreate } from '../src/views/create.js';
import { showDashboard } from '../src/views/dashboard.js';
import { showDetails } from '../src/views/details.js';
import { logout } from './api/data.js';
import { showSection } from './helper.js';

document.querySelector('nav').addEventListener('click', onNavigate);

const views = {
    'homeLink': showHome,
    'registerLink': showRegister,
    'loginLink': showLogin,
    'logoutBtn': onLogout,
    'createLink': showCreate,
    'getStartedLink': showDashboard,
    'dashboardLink': showDashboard,
    'detailsLink': showDetails
}

const contex = {
    showSection,
    goTo,
    updateNav
}

function onNavigate(event) {
    event.preventDefault();
    const id = event.target.id;
    goTo(id)
}

function goTo(id, ...params) {
    const view = views[id];
    if (typeof view == 'function') {
        view(contex, ...params)
    }
}

async function onLogout(event) {

    try {
        await logout();
        goTo('homeLink');


    } catch (error) {
        alert(error.message)
    }
    
}

function updateNav() {
    const userInfo = JSON.parse(sessionStorage.getItem('userInfo'));
    if (userInfo == null) {
        Array.from(document.querySelectorAll('.user')).forEach(el => el.style.display = 'none');
        Array.from(document.querySelectorAll('.guest')).forEach(el => el.style.display = 'block');
    } else {
        Array.from(document.querySelectorAll('.user')).forEach(el => el.style.display = 'block');
        Array.from(document.querySelectorAll('.guest')).forEach(el => el.style.display = 'none');
    }
}

updateNav();
showHome(contex);