import { html } from "../lib.js";
import { getFurnitureById, editFurniture } from "../api/data.js";

const template = (item, onSubmit, errors) => html`<div class="container">
    <div class="row space-top">
        <div class="col-md-12">
            <h1>Edit Furniture</h1>
            <p>Please fill all fields.</p>
        </div>
    </div>
    <form @submit=${onSubmit}>
        <div class="row space-top">
            <div class="col-md-4">
                <div class="form-group">
                    <label class="form-control-label" for="new-make">Make</label>
                    <input class=${'form-control' + (errors ? errors.make ? ' is-valid' : ' is-invalid' : '' )} id="new-make" type="text" name="make" .value=${item.make}>
                </div>
                <div class="form-group has-success">
                    <label class="form-control-label" for="new-model">Model</label>
                    <input class=${'form-control' + (errors ? errors.model ? ' is-valid' : ' is-invalid' : '' )} id="new-model" type="text" name="model" .value=${item.model}>
                </div>
                <div class="form-group has-danger">
                    <label class="form-control-label" for="new-year">Year</label>
                    <input class=${'form-control' + (errors ? errors.year ? ' is-valid' : ' is-invalid' : '' )} id="new-year" type="number" name="year" .value=${item.year}>
                </div>
                <div class="form-group">
                    <label class="form-control-label" for="new-description">Description</label>
                    <input class=${'form-control' + (errors ? errors.description ? ' is-valid' : ' is-invalid' : '' )} id="new-description" type="text" name="description"
                        .value=${item.description}>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label class="form-control-label" for="new-price">Price</label>
                    <input class=${'form-control' + (errors ? errors.price ? ' is-valid' : ' is-invalid' : '' )} id="new-price" type="number" name="price" .value=${item.price}>
                </div>
                <div class="form-group">
                    <label class="form-control-label" for="new-image">Image</label>
                    <input class=${'form-control' + (errors ? errors.img ? ' is-valid' : ' is-invalid' : '' )} id="new-image" type="text" name="img" .value=${item.img}>
                </div>
                <div class="form-group">
                    <label class="form-control-label" for="new-material">Material (optional)</label>
                    <input class="form-control" id="new-material" type="text" name="material" .value=${item.material}>
                </div>
                <input type="submit" class="btn btn-info" value="Edit" />
            </div>
        </div>
    </form>
</div>`;

export async function editPage(ctx) {
    const item = await getFurnitureById(ctx.params.id);

    update();

    function update(errors) {
        ctx.render(template(item, onSubmit, errors));
    }

    async function onSubmit(event) {
        event.preventDefault();

        const form = event.target;
        const formData = new FormData(form);
        const errors = {};

        errors.make = formData.get('make').trim().length >= 4 ? true : false;
        errors.model = formData.get('model').trim().length >= 4 ? true : false;

        const year = Number(formData.get('year').trim());
        errors.year = year >= 1950 && year <= 2050 ? true : false;

        errors.description = formData.get('description').trim().length > 10 ? true : false;
        errors.price = Number(formData.get('price').trim()) > 0 ? true : false;
        errors.img = formData.get('img').trim() != '' ? true : false;

        if (Object.values(errors).some(v => v == false)) {
            update(errors);
        } else {
            const furniture = [...formData.entries()].reduce((a, [k, v]) => Object.assign(a, { [k]: v }), {});
            editFurniture(ctx.params.id, furniture);
            ctx.page.redirect(`/details/${ctx.params.id}`);
        }
    }
}