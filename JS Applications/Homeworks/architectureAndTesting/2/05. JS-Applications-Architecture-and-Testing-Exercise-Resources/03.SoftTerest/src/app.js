import { logout } from "./api/api.js";
import { showSection } from "./helpers.js";
import createView from "./views/createIdea.js";
import dashboardView from "./views/dashboard.js";
import detailsView from "./views/details.js";
import homeView from "./views/home.js"
import loginView from "./views/login.js";
import registerView from "./views/register.js";


const mainViewContainer = document.querySelector('#main-view');

homeView.initialize(document.querySelector('#home'), mainViewContainer);
registerView.initialize(document.querySelector('#register'), mainViewContainer);
loginView.initialize(document.querySelector('#login'), mainViewContainer);
dashboardView.initialize(document.querySelector('#dashboard-holder'), mainViewContainer);
detailsView.initialize(document.querySelector('#details'), mainViewContainer);
createView.initialize(document.querySelector('#create'), mainViewContainer);




const links = {
    'home-link': 'home',
    'dashboard-link': 'dashboard',
    'create-link': 'create',
    'login-link': 'login',
    'register-link': 'register',
    'get-started-link': 'home',
    'sign-up-link': "register",
    "sign-in-link": "login",
    "details-link": "details"
};
const views = {
    'home': homeView.showHomePage,
    'dashboard': dashboardView.showDashboardPage,
    'create': createView.showCreatePage,
    'login': loginView.showLoginPage,
    'register': registerView.showRegisterPage,
    'details': detailsView.showDetailsPage
}


const navigationBar = document.querySelector('#navigation');
navigationBar.addEventListener('click', onNavigate);

const context = {
    redirectTo,
    showSection,
    onNavigate,
    customizeNavigationBar
}

function onNavigate(event){
    const linkName = links[event.target.id];
    if (linkName) {
        event.preventDefault();
        redirectTo(linkName);
    };
};

function redirectTo(pageLink, ...params){
    const page = views[pageLink]
    if (typeof page == "function"){
        page(context, ...params)
    }
}

const logoutBUtton = navigationBar.querySelector('#logout-button');
logoutBUtton.addEventListener('click',onLogout)

async function onLogout(event){
    event.preventDefault()
    await logout()
    customizeNavigationBar()
    redirectTo('login')
}
const guestNavControls = navigationBar.querySelectorAll('.guest');
const userNavControls = navigationBar.querySelectorAll('.user');
export function customizeNavigationBar(){
    if (sessionStorage.getItem('userData')){
        guestNavControls.forEach(control => control.style.display = 'none')
        userNavControls.forEach(control => control.style.display = 'block')
    }else {
        guestNavControls.forEach(control => control.style.display = 'block')
        userNavControls.forEach(control => control.style.display = 'none')
    }
}
customizeNavigationBar()
redirectTo('home')