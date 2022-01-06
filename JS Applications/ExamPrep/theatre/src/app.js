import { page } from './lib.js';
import { decorateContext, updateUserNav } from './middlewares/decorateContext.js';
import { createPage } from './views/createPage.js';
import { dashboardPage } from './views/dashboardPage.js';
import { detailsPage } from './views/detailsPage.js';
import { editPage } from './views/editPage.js';
import { loginPage } from './views/loginPage.js';
import { profilePage } from './views/profilePage.js';
import { registerPage } from './views/register.js';

page(decorateContext);
updateUserNav();

page('/', dashboardPage);
page('/login', loginPage);
page('/register', registerPage);
page('/create', createPage);
page('/details/:id', detailsPage);
page('/edit/:id', editPage);
page('/profile', profilePage);

page.start();