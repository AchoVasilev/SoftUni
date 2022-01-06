import { page } from './lib.js';
import { decorateContext, updateUserNav } from './middlewares/decorateContext.js';
import { catalogPage } from './views/catalogPage.js';
import { createPage } from './views/createPage.js';
import { detailsPage } from './views/detailsPage.js';
import { editPage } from './views/editPage.js';
import { homePage } from './views/homePage.js';
import { loginPage } from './views/loginPage.js';
import { registerPage } from './views/registerPage.js';
import { searchPage } from './views/searchPage.js';

page(decorateContext);
updateUserNav();
page('/', homePage);
page('/login', loginPage);
page('/register', registerPage);
page('/catalog', catalogPage);
page('/create', createPage);
page('/edit/:id', editPage);
page('/details/:id', detailsPage);
page('/search', searchPage);

page.start();