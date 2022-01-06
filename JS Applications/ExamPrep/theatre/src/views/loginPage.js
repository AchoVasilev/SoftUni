import { login } from '../api/data.js';
import { html } from '../lib.js';

const template = (onSubmit) => html`
<section id="loginaPage">
    <form @submit=${onSubmit} class="loginForm">
        <h2>Login</h2>
        <div>
            <label for="email">Email:</label>
            <input id="email" name="email" type="text" placeholder="steven@abv.bg" value="">
        </div>
        <div>
            <label for="password">Password:</label>
            <input id="password" name="password" type="password" placeholder="********" value="">
        </div>

        <button class="btn" type="submit">Login</button>

        <p class="field">
            <span>If you don't have profile click <a href="/register">here</a></span>
        </p>
    </form>
</section>
`;

export async function loginPage(ctx) {
    ctx.render(template(onSubmit));

    async function onSubmit(ev) {
        ev.preventDefault();
        const form = new FormData(ev.target);

        const email = form.get('email').trim();
        const password = form.get('password').trim();

        if (email == '' || password == '') {
            return alert('All fields are required!');
        }

        await login(email, password);
        ctx.updateUserNav();
        ctx.page.redirect('/');
    }
}