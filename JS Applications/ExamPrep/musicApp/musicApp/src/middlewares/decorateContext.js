import { logout } from '../api/data.js';
import { getUserData } from '../api/storage.js';
import { page, render } from '../lib.js';

const root = document.getElementById('main-content');
document.getElementById('logoutBtn').addEventListener('click', onLogout);

export function decorateContext(ctx, next) {
    ctx.render = (template) => render(template, root);
    ctx.updateUserNav = updateUserNav;
    next();
}

export function updateUserNav() {
    const userData = getUserData();
    if (userData) {
        document.getElementById('create').style.display = 'inline-block';
        document.getElementById('logout').style.display = 'inline-block';
        document.getElementById('register').style.display = 'none';
        document.getElementById('login').style.display = 'none';
    } else {
        document.getElementById('create').style.display = 'none';
        document.getElementById('logout').style.display = 'none';
        document.getElementById('register').style.display = 'inline-block';
        document.getElementById('login').style.display = 'inline-block';
    }
}

function onLogout() {
    logout();
    updateUserNav();
    page.redirect('/');
}