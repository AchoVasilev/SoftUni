import { page } from './lib.js';
import { decorateContext, updateUserNav } from './middlewares/decorateContext.js';
import { allGamesPage } from './views/allGamesPage.js';
import { createGamePage } from './views/createPage.js';
import { detailsPage } from './views/detailsPage.js';
import { editGamePage } from './views/editPage.js';
import { homePage } from './views/homePage.js';
import { loginPage } from './views/loginPage.js';
import { registerPage } from './views/registerPage.js';

page(decorateContext);
updateUserNav();
page('/', homePage);
page('/login', loginPage);
page('/register', registerPage);
page('/all-games', allGamesPage);
page('/create', createGamePage);
page('/details/:id', detailsPage);
page('/edit/:id', editGamePage);

page.start();