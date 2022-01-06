import { showHome } from "./home.js";
import { showLogin } from "./login.js";
import { showRegister } from "./register.js";

//create placeholder modules for every view - initialization and vizualisation/ one file for every section
// горе създадохме файловете create.js/details.js/edit/login/register.js

// configure and test navigation - put eventlisteners on buttons and associate buttons with modules
//implement modules
// - create async functions for requests
// - implement DOM logic - what to show on screen

const views = {
    'homeLink': showHome,
    'loginLink': showLogin,
    'registerLink': showRegister,
}
const nav = document.querySelector('nav');

document.getElementById('logoutBtn').addEventListener('click', onLogout); //this will stop the propagation of the second event listener attached tp thesame btn
nav.addEventListener('click', (event) => {
    const view = views[event.target.id]
    if (typeof view == 'function') {
        event.preventDefault();
        view();
    }
});

updateNav()

//start app in home view; 
showHome();

export function updateNav() {
    const userData = JSON.parse(sessionStorage.getItem('userData'));
    if (userData != null) {
        nav.querySelector('#welcomeMsg').textContent = `Welcome, ${userData.email}`;
        [...nav.querySelectorAll('.user')].forEach(e => e.style.display = 'block');
        [...nav.querySelectorAll('.guest')].forEach(e => e.style.display = 'none');
    } else {
        [...nav.querySelectorAll('.user')].forEach(e => e.style.display = 'none');
        [...nav.querySelectorAll('.guest')].forEach(e => e.style.display = 'block');
    }
}

async function onLogout(event) {
    event.preventDefault();
    event.stopImmediatePropagation();

    const { token } = JSON.parse(sessionStorage.getItem('userData'));

    await fetch('http://localhost:3030/users/logout', {
        headers: {
            'X-Authorization': token
        }
    });
    sessionStorage.removeItem('userData');
    updateNav();
    showLogin();
}
// Order of views:
// - catalogue ( home view);
// - login/register
// - logout
// - create - to be done
// - details
// - likes
// - edit - to be done
// - delete - to be done