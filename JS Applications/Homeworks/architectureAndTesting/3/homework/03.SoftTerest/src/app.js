import { showCatalogPage } from "./api/views/catalog.js";
import { showCreatePage } from "./api/views/create.js";
import { showDetailsPage } from "./api/views/details.js";
import { showHomePage } from "./api/views/home.js";
import { showLoginPage } from "./api/views/login.js";
import { showRegisterPage } from "./api/views/register.js";
import { showSection } from "./api/views/dom.js";


const links = {
    'homeLink': 'home' ,
    'getStartedLink': 'home' ,
    'catalogLink': 'catalog' ,
    'loginLink': 'login' ,
    'registerLink': 'register' ,
    'createLink': 'create' 
};

const views = {
    'home' : showHomePage,
    'catalog':showCatalogPage,
    'login': showLoginPage,
    'register': showRegisterPage,
    'create': showCreatePage,
    'details': showDetailsPage
};

const nav = document.querySelector('nav');
nav.addEventListener('click', onNavigate);

const ctx = {
    goTo,
    showSection
}

function onNavigate(event) {
    const name = links[event.target.id];
    if(name) {
        event.preventDefault();
        goTo(name);
    }
};
function goTo(name, ...params) {
    const view = views[name];
    if (typeof view == 'function') {
        view(ctx, ...params);
    }
}