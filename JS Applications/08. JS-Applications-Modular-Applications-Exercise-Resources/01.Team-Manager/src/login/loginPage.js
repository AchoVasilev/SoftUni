import { login } from '../api/data.js';
import { loginTemplate } from './loginTemplate.js';

let _router = undefined;
let _renderHandler = undefined;

function initialize(router, renderHandler) {
    _router = router;
    _renderHandler = renderHandler;
}

async function getView(context) {
    update();

    function update(errorMsg) {
        const templateResult = loginTemplate(onSubmit, errorMsg);
        _renderHandler(templateResult);
    }


    async function onSubmit(ev) {
        ev.preventDefault();

        const formData = new FormData(ev.target);
        const email = formData.get('email').trim();
        const password = formData.get('password').trim();

        try {
            const result = await login(email, password);
            ev.target.reset();
            _router.redirect('/my-teams');
        } catch (err) {
            update(err.message);
        }
    }
}

export default {
    initialize,
    getView
};