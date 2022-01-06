import homePage from './home/homePage.js';
import nav from './nav/nav.js';
import { page } from './lib.js';
import { LitRenderer } from './rendering/litRenderer.js';
import storageService from './storageService.js';
import loginPage from './login/loginPage.js';
import registerPage from './register/registerPage.js';
import { logout } from './api/data.js';
import browseTeamsPage from './browseTeams/browseTeamsPage.js';
import teamDetailsPage from './teamDetails/teamDetailsPage.js';
import myTeamsPage from './myTeams/myTeamsPage.js';
import createTeamPage from './createTeam/createTeamPage.js';
import editTeamPage from './editTeam/editTeamPage.js';

const mainElement = document.getElementById('app');
const navElement = document.getElementById('titlebar');
const modal = document.getElementById('modal');

const litRenderer = new LitRenderer();
const navRenderHandler = litRenderer.createRenderHandler(navElement);
const mainRenderHandler = litRenderer.createRenderHandler(mainElement);

nav.initialize(page, navRenderHandler);
homePage.initialize(page, mainRenderHandler);
loginPage.initialize(page, mainRenderHandler);
registerPage.initialize(page, mainRenderHandler);
browseTeamsPage.initialize(page, mainRenderHandler);
teamDetailsPage.initialize(page, mainRenderHandler);
myTeamsPage.initialize(page, mainRenderHandler);
createTeamPage.initialize(page, mainRenderHandler);
editTeamPage.initialize(page, mainRenderHandler);

page(decorateUser);
page(nav.getView);

page('/home', '/');
page('/index.html', '/');

page('/', homePage.getView);
page('/login', loginPage.getView);
page('/register', registerPage.getView);
page('/browse-teams', browseTeamsPage.getView);
page('/details/:id', teamDetailsPage.getView);
page('/my-teams', myTeamsPage.getView);
page('/create', createTeamPage.getView);
page('/edit/:id', editTeamPage.getView);

page.start();

function decorateUser(context, next) {
    const user = storageService.getUserData();
    context.user = user;
    next();
}

const logoutBtn = document.getElementById('logoutBtn');

if (logoutBtn) {
    logoutBtn.addEventListener('click', onLogout);

    async function onLogout() {
        await logout();
        page(nav.getView);
        page.redirect('/home');
    }
}