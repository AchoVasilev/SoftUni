import { html } from "../lib.js";
import { getAllFurniture } from "../api/data.js";

const itemTemplate = (item) => html`
<div class="col-md-4">
    <div class="card text-white bg-primary">
        <div class="card-body">
            <img src=${item.img} />
            <p>Description here</p>
            <footer>
                <p>Price: <span>${item.price} $</span></p>
            </footer>
            <div>
                <a href="/details/${item._id}" class="btn btn-info">Details</a>
            </div>
        </div>
    </div>
</div>`;

const homeTemplate = (items) => html`
<div class="container">
    <div class="row space-top">
        <div class="col-md-12">
            <h1>Welcome to Furniture System</h1>
            <p>Select furniture from the catalog to view details.</p>
        </div>
    </div>
    <div class="row space-top">
        ${items.map(itemTemplate)}
    </div>
</div>`;

export async function homePage(ctx) {
    const items = await getAllFurniture();
    ctx.render(homeTemplate(items));
    ctx.updateUserNav();
}