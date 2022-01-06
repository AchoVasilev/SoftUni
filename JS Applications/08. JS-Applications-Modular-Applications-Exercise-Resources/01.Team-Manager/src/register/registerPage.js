import { register } from '../api/data.js';
import { registerTemplate } from './registerTemplate.js';

let _renderHandler = undefined;
let _router = undefined;

function initialize(router, renderHandler) {
    _router = router;
    _renderHandler = renderHandler;
}

async function getView(context) {
    update();

    function update(errorMsg) {
        const templateResult = registerTemplate(onSubmit, errorMsg);
        _renderHandler(templateResult);
    }

    async function onSubmit(ev) {
        ev.preventDefault();

        const formData = new FormData(ev.target);
        const email = formData.get('email').trim();
        const username = formData.get('username').trim();
        const password = formData.get('password').trim();
        const repass = formData.get('repass').trim();

        try {
            if (email == '' || password == '' || username == '') {
                throw new Error('All fields are required!');
            }

            if (password != repass) {
                throw new Error('Passwords do not match');
            }

            if (username.length < 3) {
                throw new Error('Username must be at least 3 characters long.');
            }

            if (password.length < 3) {
                throw new Error('Password must be at least 3 characters long.');
            }

            const data = {
                email,
                username,
                password
            };

            await register(data);
            ev.target.reset();
            _router.redirect('/my-teams');
        } catch (error) {
            update(error.message);
        }
    }
}

export default {
    initialize,
    getView
};