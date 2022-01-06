import { homeTemplate } from './homeTemplate.js';

let _router = undefined;
let _renderHandler = undefined;

function initialize(router, renderHandler) {
    _router = router;
    _renderHandler = renderHandler;
}

async function getView(context, next) {
    const model = {
        isLoggedIn: context.user !== null
    };

    const templateResult = homeTemplate(model);
    _renderHandler(templateResult);

    next();
}

export default {
    initialize,
    getView
};