import { createItem } from '../api/data.js';
import { html } from '../lib.js';

const createTemplate = (onSubmit, errorMsg, errors) => html`
<div class="row space-top">
    <div class="col-md-12">
        <h1>Create New Furniture</h1>
        <p>Please fill all fields.</p>
    </div>
</div>
<form @submit=${onSubmit}>
    ${errorMsg ? html`<div class="form-group error">${errorMsg}</div>` : null}
    <div class="row space-top">
        <div class="col-md-4">
            <div class="form-group">
                <label class="form-control-label" for="new-make">Make</label>
                <input class=${'form-control' + (errors.make ? ' is-invalid' : '')} id="new-make" type="text" name="make">
            </div>
            <div class="form-group has-success">
                <label class="form-control-label" for="new-model">Model</label>
                <input class=${'form-control' + (errors.model ? ' is-invalid' : '')} id="new-model" type="text" name="model">
            </div>
            <div class="form-group has-danger">
                <label class="form-control-label" for="new-year">Year</label>
                <input class=${'form-control' + (errors.year ? ' is-invalid' : '')} id="new-year" type="number" name="year">
            </div>
            <div class="form-group">
                <label class="form-control-label" for="new-description">Description</label>
                <input class=${'form-control' + (errors.description ? ' is-invalid' : '')} id="new-description" type="text" name="description">
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label class="form-control-label" for="new-price">Price</label>
                <input class=${'form-control' + (errors.price ? ' is-invalid' : '')} id="new-price" type="number" name="price">
            </div>
            <div class="form-group">
                <label class="form-control-label" for="new-image">Image</label>
                <input class=${'form-control' + (errors.img ? ' is-invalid' : '')} id="new-image" type="text" name="img">
            </div>
            <div class="form-group">
                <label class="form-control-label" for="new-material">Material (optional)</label>
                <input class="form-control" id="new-material" type="text" name="material">
            </div>
            <input type="submit" class="btn btn-primary" value="Create" />
        </div>
    </div>
</form>`;

export function createPage(context) {
    update();

    function update(errorMsg = null, errors = {}) {
        context.render(createTemplate(onSubmit, errorMsg, errors));
    }

    async function onSubmit(ev) {
        ev.preventDefault();

        const formData = new FormData(ev.target);
        const formDataEntries = [...formData.entries()];
        const data = formDataEntries.reduce((acc, [key, value]) => Object.assign(acc, { [key]: value.trim() }), {});

        const emptyFields = formDataEntries.filter(([k, v]) => k != 'material' && v.trim() == '');

        try {
            if (emptyFields.length > 0) {
                const errors = emptyFields.reduce((a, [k]) => Object.assign(a, { [k]: true }), {});
                throw {
                    error: new Error('Please fill all mandatory fields.'),
                    errors
                };
            }

            if (data.make.length < 4 || data.model.length < 4) {
                throw {
                    error: new Error('Field must be at least 4 characters long.'),
                    errors: {
                        make: true,
                        model: true
                    }
                };
            }

            data.year = Number(data.year);
            data.price = Number(data.price);

            if (data.year < 1950 || data.year > 2050) {
                throw {
                    error: new Error('Year must be between 1950 and 2050.'),
                    errors: {
                        year: true
                    }
                };
            }

            if (data.description.length < 10) {
                throw {
                    error: new Error('Description must be at least 10 characters long.'),
                    errors: {
                        description: true
                    }
                };
            }

            if (data.price < 0) {
                throw {
                    error: new Error('Price must be a positive number.'),
                    errors: {
                        price: true
                    }
                };
            }

            await createItem(data);
            ev.target.reset();
            context.page.redirect('/');
        } catch (err) {
            const message = err.message || err.error.message;
            update(message, err.errors || {});
        }
    }
}