import { deleteTheatreById, getMyLikeByTheaterId, getTheatreById, getTheatreLikes, likeTheatre } from '../api/data.js';
import { getUserData } from '../api/storage.js';
import { html } from '../lib.js';

const template = (theatre, isOwner, onDelete, showLikeBtn, onLike, likes) => html`
<section id="detailsPage">
    <div id="detailsBox">
        <div class="detailsInfo">
            <h1>Title: ${theatre.title}</h1>
            <div>
                <img src="${theatre.imageUrl}" />
            </div>
        </div>

        <div class="details">
            <h3>Theater Description</h3>
            <p>${theatre.description}</p>
            <h4>Date: ${theatre.date}</h4>
            <h4>Author: ${theatre.author}</h4>
            <div class="buttons">
                ${
                    theatreControlsTemplate(theatre, isOwner, onDelete)
                }

                ${
                    likeControlsTemplate(showLikeBtn, onLike)
                }
            </div>
            <p class="likes">Likes: ${likes}</p>
        </div>
    </div>
</section>`;

const theatreControlsTemplate = (theatre, isOwner, onDelete) => {
    if (isOwner) {
        return html`<a @click=${onDelete} class="btn-delete" href="javascript:void(0)">Delete</a>
                    <a class="btn-edit" href="/Edit/${theatre._id}">Edit</a>`;
    } else {
        null;
    }
};

const likeControlsTemplate = (showLikeBtn, onLike) => {
    if (showLikeBtn) {
        return html`<a class="btn-like" @click=${onLike} href="javascript:void(0)">Like</a>`;
    } else {
        return null;
    }
};

export async function detailsPage(ctx) {
    const theatreId = ctx.params.id;
    const userData = getUserData();

    const [theatre, likes, hasLiked] = await Promise.all([
        getTheatreById(theatreId),
        getTheatreLikes(theatreId),
        userData ? getMyLikeByTheaterId(userData.id, theatreId) : 0
    ]);

    const isOwner = userData && userData.id == theatre._ownerId;
    const showLikeBtn = userData != null && isOwner == false && hasLiked == false;

    ctx.render(template(theatre, isOwner, onDelete, showLikeBtn, onLike, likes));

    async function onDelete() {
        const choice = confirm('Are you sure you want to delete this item?');

        if (choice) {
            await deleteTheatreById(theatreId);
            ctx.page.redirect('/profile');
        }
    }

    async function onLike() {
        const response = await likeTheatre(theatreId);
        console.log(response);
        ctx.page.redirect('/details/' + theatreId);
    }
}

// VM488: 1 Uncaught(in promise) SyntaxError: Unexpected end of JSON input
//     at request(api.js: 17)
//     at async HTMLAnchorElement.onLike(detailsPage.js: 76)
// request @api.js: 17
// async function (async)
// onLike @detailsPage.js: 76
// handleEvent @parts.ts: 532
// EventPart.__boundHandleEvent @parts.ts: 490