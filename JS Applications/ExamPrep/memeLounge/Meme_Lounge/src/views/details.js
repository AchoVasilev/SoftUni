import { deleteMeme, getMeme } from '../api/data.js';
import { getUserData } from '../api/storage.js';
import { html } from '../lib.js';

const detailsTemplate = (meme, isOwner, onDelete) => html`
<section id="meme-details">
    <h1>Meme Title: ${meme.title}

    </h1>
    <div class="meme-details">
        <div class="meme-img">
            <img alt="meme-alt" src=${meme.imageUrl}>
        </div>
        <div class="meme-description">
            <h2>Meme Description</h2>
            <p>
                ${meme.description}
            </p>

            ${isOwner
                ? html` <a class="button warning" href="/edit/${meme._id}">Edit</a>
                        <button @click=${onDelete} class="button danger">Delete</button>`
                : null
            }
        </div>
    </div>
</section>`;

export async function detailsPage(ctx) {
    const memeId = ctx.params.id;
    const meme = await getMeme(memeId);
    const userData = getUserData();

    const isOwner = userData && userData.id == meme._ownerId;

    ctx.render(detailsTemplate(meme, isOwner, onDelete));

    async function onDelete() {
        const choice = confirm('Are you sure you want to delete this meme?');

        if (choice == true) {
            await deleteMeme(memeId);
            ctx.page.redirect('/memes');
        }
    }
}