import { html } from "../lib.js";
import { register } from "../api/api.js";

const template = (onSubmit) => html`
<div class="container">
    <div class="row space-top">
        <div class="col-md-12">
            <h1>Register New User</h1>
            <p>Please fill all fields.</p>
        </div>
    </div>
    <form @submit=${onSubmit}>
        <div class="row space-top">
            <div class="col-md-4">
                <div class="form-group">
                    <label class="form-control-label" for="email">Email</label>
                    <input class="form-control" id="email" type="text" name="email">
                </div>
                <div class="form-group">
                    <label class="form-control-label" for="password">Password</label>
                    <input class="form-control" id="password" type="password" name="password">
                </div>
                <div class="form-group">
                    <label class="form-control-label" for="rePass">Repeat</label>
                    <input class="form-control" id="rePass" type="password" name="rePass">
                </div>
                <input type="submit" class="btn btn-primary" value="Register" />
            </div>
        </div>
    </form>
</div>`;

export function registerPage(ctx) {
    update();

    function update() {
        ctx.render(template(onSubmit));
    }

    async function onSubmit(event) {
        event.preventDefault();

        const form = event.target;
        const [email, password, rePass] = [...new FormData(form).values()];

        try {
            if (password != rePass) {
                throw new Error(alert('Passwords must be equal!'));
            }
            await register(email, password);
            ctx.page.redirect('/');
        } catch (error) {
            update();
        }
    }
}