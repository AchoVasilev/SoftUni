import { html } from '../lib.js';

export const navTemplate = (model) => html`
<a href="/home" class="site-logo">Team Manager</a>
<nav>
    <a href="/browse-teams" class="action">Browse Teams</a>
    ${model.isLoggedIn ?
        html`<a href="/my-teams" class="action">My Teams</a>
    <a id="logoutBtn" href="javascript:void(0)" class="action">Logout</a>` :
         html`<a href="/login" class="action">Login</a>
    <a href="/register" class="action">Register</a>`
    }

</nav>`;