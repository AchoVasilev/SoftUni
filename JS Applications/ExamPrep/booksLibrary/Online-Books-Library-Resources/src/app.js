import { page } from './lib.js';
import { decorateContext, updateUserNav } from './middlewares/decorateContext.js';
import { createPage } from './views/create.js';
import { dashboardPage } from './views/dashboard.js';
import { detailsPage } from './views/details.js';
import { editPage } from './views/edit.js';
import { loginPage } from './views/login.js';
import { myBooksPage } from './views/myBooks.js';
import { registerPage } from './views/register.js';

page(decorateContext);
updateUserNav();
page('/', dashboardPage);
page('/login', loginPage);
page('/register', registerPage);
page('/add-book', createPage);
page('/details/:id', detailsPage);
page('/edit/:id', editPage);
page('/my-books', myBooksPage);

page.start();