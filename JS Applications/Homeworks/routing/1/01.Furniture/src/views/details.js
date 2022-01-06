import { html } from "../lib.js";
import { getFurnitureById, deleteFurniture } from "../api/data.js";
import { getUserData } from "../util.js";

const template = (item, isOwner, onDelete) => html`<div class="container">
    <div class="row space-top">
        <div class="col-md-12">
            <h1>Furniture Details</h1>
        </div>
    </div>
    <div class="row space-top">
        <div class="col-md-4">
            <div class="card text-white bg-primary">
                <div class="card-body">
                    <img src=${item.img} />
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <p>Make: <span>${item.make}</span></p>
            <p>Model: <span>${item.model}</span></p>
            <p>Year: <span>${item.year}</span></p>
            <p>Description: <span>${item.description}</span></p>
            <p>Price: <span>${item.price}</span></p>
            <p>Material: <span>${item.material}</span></p>
            <div style="display: ${isOwner ? 'block' : 'none'}">
                <a href="/edit/${item._id}" class="btn btn-info">Edit</a>
                <a @click=${onDelete} href="javascript:void(0)" class="btn btn-red">Delete</a>
            </div>
        </div>
    </div>
</div>`;

export async function detailsPage(ctx) {
    const item = await getFurnitureById(ctx.params.id);
    const userData = getUserData();
    const isOwner = userData && userData.id == item._ownerId;
    ctx.render(template(item, isOwner, onDelete));

    async function onDelete() {
        const accept = confirm('Are you sure you want to delete this item?');
        if (accept) {
            deleteFurniture(ctx.params.id);
            ctx.page.redirect('/');
        }
    }
}