import { logout } from './api/data.js';
import { showSection } from './dom.js';
import { showCatalogPage } from './views/catalog.js';
import { showCreatePage } from './views/create.js';
import { showDetailsPage } from './views/details.js';
import { showHomePage } from './views/home.js';
import { showLoginPage } from './views/login.js';
import { showRegisterPage } from './views/register.js';

const links = {
    'homeLink': 'home',
    'getStartedLink': 'home',
    'catalogLink': 'catalog',
    'loginLink': 'login',
    'registerLink': 'register',
    'createLink': 'create'
};

const views = {
    'home': showHomePage,
    'catalog': showCatalogPage,
    'login': showLoginPage,
    'register': showRegisterPage,
    'create': showCreatePage,
    'details': showDetailsPage
};

const nav = document.querySelector('nav');
nav.addEventListener('click', onNavigate);

document.getElementById('logoutBtn').addEventListener('click', async (ev) => {
    ev.preventDefault();
    await logout();
    updateNav();
    goTo('home');
});

const context = {
    goTo,
    showSection,
    updateNav
};

updateNav();
goTo('home');

function onNavigate(event) {
    const name = links[event.target.id];
    if (name) {
        event.preventDefault();
        goTo(name);
    }
}

function goTo(page, ...params) {
    const view = views[page];
    if (typeof view == 'function') {
        view(context, ...params);
    }
}

function updateNav() {
    const userData = JSON.parse(sessionStorage.getItem('userData'));
    if (userData != null) {
        [...nav.querySelectorAll('.user')].forEach(x => x.style.display = 'block');
        [...nav.querySelectorAll('.guest')].forEach(x => x.style.display = 'none');
    } else {
        [...nav.querySelectorAll('.user')].forEach(x => x.style.display = 'none');
        [...nav.querySelectorAll('.guest')].forEach(x => x.style.display = 'block');
    }
}