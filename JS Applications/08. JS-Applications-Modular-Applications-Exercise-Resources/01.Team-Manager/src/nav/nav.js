import { navTemplate } from './navTemplate.js';

let _renderHandler = undefined;
let _router = undefined;

function initialize(router, renderHandler) {
    _router = router;
    _renderHandler = renderHandler;
}

async function getView(context, next) {
    const userData = context.user;

    const viewModel = {
        isLoggedIn: userData !== null
    };

    const templateResult = navTemplate(viewModel);
    _renderHandler(templateResult);

    next();
}

export default {
    initialize,
    getView
};