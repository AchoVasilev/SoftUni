import { editItem, getById } from '../api/data.js';
import { editTeamTemplate } from './editTeamTemplate.js';

let _router = undefined;
let _renderHandler = undefined;

function initialize(router, renderHandler) {
    _router = router;
    _renderHandler = renderHandler;
}

async function getView(context) {
    const itemId = context.params.id;
    const item = await getById(itemId);

    update();

    function update(errorMsg) {
        const template = editTeamTemplate(item, onSubmit, errorMsg);
        _renderHandler(template);
    }

    async function onSubmit(ev) {
        ev.preventDefault();

        const formData = new FormData(ev.target);
        const name = formData.get('name').trim();
        const logoUrl = formData.get('logoUrl').trim();
        const description = formData.get('description').trim();

        try {
            if (name == '' || logoUrl == '' || description == '') {
                throw new Error('All fields are required');
            }

            if (name.length < 4) {
                throw new Error('Name must be at least 4 characters long');
            }

            if (description.length < 10) {
                throw new Error('Description must be at least 10 characters long');
            }

            const data = {
                name,
                logoUrl,
                description
            };

            const response = await editItem(itemId, data);
            ev.target.reset();
            _router.redirect(`/details/${response._id}`);
        } catch (err) {
            update(err.message);
        }
    }
}

export default {
    initialize,
    getView
};