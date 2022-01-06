import { validateData } from "../api/api.js";
import { createItem } from "../api/data.js";
import { showModal } from "../common/modal.js";
import { html } from "../lib.js";

const createTemplate = (onSubmit, errorMsg, errors) => html`
<div class="row space-top">
    <div class="col-md-12">
        <h1>Create New Furniture</h1>
        <p>Please fill all fields.</p>
        ${errorMsg ? html`<div class="form-group error">${errorMsg}</div>` : null}
    </div>
</div>
<form @submit=${onSubmit}>
    <div class="row space-top">
        <div class="col-md-4">
            <div class="form-group">
                <label class="form-control-label" for="new-make">Make</label>
                <input class="form-control ${errors.make ? errors.make : ''}" id="new-make" type="text" name="make">
            </div>
            <div class="form-group has-success">
                <label class="form-control-label" for="new-model">Model</label>
                <input class="form-control ${errors.model ? errors.model : ''}" id="new-model" type="text" name="model">
            </div>
            <div class="form-group has-danger">
                <label class="form-control-label" for="new-year">Year</label>
                <input class="form-control ${errors.year ? errors.year : ''}" id="new-year" type="number" name="year">
            </div>
            <div class="form-group">
                <label class="form-control-label" for="new-description">Description</label>
                <input class="form-control ${errors.description ? errors.description : ''}" id="new-description"
                    type="text" name="description">
            </div>
        </div>
        <div class="col-md-4">
            <div class="form-group">
                <label class="form-control-label" for="new-price">Price</label>
                <input class="form-control ${errors.price ? errors.price : ''}" id="new-price" type="number"
                    name="price">
            </div>
            <div class="form-group">
                <label class="form-control-label" for="new-image">Image</label>
                <input class="form-control ${errors.img ? errors.img : ''}" id="new-image" type="text" name="img">
            </div>
            <div class="form-group">
                <label class="form-control-label" for="new-material">Material (optional)</label>
                <input class="form-control" id="new-material" type="text" name="material">
            </div>
            <input type="submit" class="btn btn-primary" value="Create" />
        </div>
    </div>
</form>
`

export function createPage(ctx) {

    update(null, {});

    function update(errorMsg, errors) {
        ctx.render(createTemplate(onSubmit, errorMsg, errors));
    }

    async function onSubmit(event) {
        event.preventDefault();
        const formData = [...Object(new FormData(event.target)).entries()];

        const data = formData.reduce((a, [k, v]) => Object.assign(a, { [k]: v.trim() }), {});
        const missing = formData.filter(([k, v]) => k != 'material' && v.trim() == '');

        try {
            // let errors = {};
            // let error = [];
            // if (missing.length > 0) {
            //     errors = missing.reduce((a, [k]) => Object.assign(a, { [k]: 'is-invalid' }), {})
            //     throw {
            //         error: new Error('Fill all required fields'),
            //         errors
            //     };
            // }

            // data.year = Number(data.year);
            // data.price = Number(data.price);

            // if (data.make.length < 4) {

            //     Object.assign(errors, { make: "is-invalid" });
            //     error.push('Make must have at least 4 symbols!');
            // } else {
            //     Object.assign(errors, { make: "is-valid" });
            // }




            // if (data.model.length < 4) {

            //     error.push('Model must have at least 4 symbols!');
            //     Object.assign(errors, { model: "is-invalid" });
            // } else {
            //     Object.assign(errors, { model: "is-valid" });
            // }


            // if (data.year < 1950 || data.year > 2050) {
            //     error.push('Year must be between 1950 and 2050!');
            //     Object.assign(errors, { year: "is-invalid" });
            // } else {
            //     Object.assign(errors, { year: "is-valid" });
            // }

            // if (data.description.length < 10) {
            //     error.push('Description must be more than 10 symbols long!');
            //     Object.assign(errors, { description: "is-invalid" });
            // } else {
            //     Object.assign(errors, { description: "is-valid" });
            // }

            // if (data.price < 0) {
            //     error.push('Price must be a positive number!');
            //     Object.assign(errors, { price: "is-invalid" });
            // } else {
            //     Object.assign(errors, { price: "is-valid" });
            // }

            // if (error.length > 0) {
            //     const allErrors = error.join("\n");
            //     console.log(allErrors);
            //     throw {
            //         error: new Error(allErrors),
            //         errors

            //     }
            // }

            validateData(data, missing);

            const result = await createItem(data);
            event.target.reset();
            ctx.page.redirect('/details/' + result._id);
            showModal('Instead to the dashboard you\'ll be redirected to the newly added furniture\'s details in 7sec');

        } catch (err) {
            const message = err.message || err.error.message;
            update(message, err.errors || {})

        }
    }
}
