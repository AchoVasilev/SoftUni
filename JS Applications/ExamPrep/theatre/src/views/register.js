import { register } from '../api/data.js';
import { html } from '../lib.js';

const template = (onSubmit) => html`
<section id="registerPage">
    <form @submit=${onSubmit} class="registerForm">
        <h2>Register</h2>
        <div class="on-dark">
            <label for="email">Email:</label>
            <input id="email" name="email" type="text" placeholder="steven@abv.bg" value="">
        </div>

        <div class="on-dark">
            <label for="password">Password:</label>
            <input id="password" name="password" type="password" placeholder="********" value="">
        </div>

        <div class="on-dark">
            <label for="repeatPassword">Repeat Password:</label>
            <input id="repeatPassword" name="repeatPassword" type="password" placeholder="********" value="">
        </div>

        <button class="btn" type="submit">Register</button>

        <p class="field">
            <span>If you have profile click <a href="/login">here</a></span>
        </p>
    </form>
</section>
`;

export async function registerPage(ctx) {
    ctx.render(template(onSubmit));

    async function onSubmit(ev) {
        ev.preventDefault();

        const form = new FormData(ev.target);
        const email = form.get('email').trim();
        const password = form.get('password').trim();
        const repeatPassword = form.get('repeatPassword').trim();

        if (email == '' || password == '' || repeatPassword == '') {
            return alert('All fields are required!');
        }

        if (password != repeatPassword) {
            alert('Passwords do not match!');
        }

        await register(email, password);
        ctx.updateUserNav();
        ctx.page.redirect('/');
    }
}