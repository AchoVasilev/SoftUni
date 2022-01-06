import { createCommentForGame, deleteGameById, getCommentsForGameById, getGameById } from '../api/data.js';
import { getUserData } from '../api/storage.js';
import { html } from '../lib.js';

const template = (game, isOwner, onDelete, isLoggedIn, comments, onComment) => html`
<section id="game-details">
    <h1>Game Details</h1>
    <div class="info-section">

        <div class="game-header">
            <img class="game-img" src="images/MineCraft.png" />
            <h1>${game.title}</h1>
            <span class="levels">MaxLevel: ${game.maxLevel}</span>
            <p class="type">${game.category}</p>
        </div>

        <p class="text">
            ${game.summary}
        </p>

        <!-- Bonus ( for Guests and Users ) -->
        <div class="details-comments">
            <h2>Comments:</h2>
            <ul>
                ${comments.length == 0
                    ? html`<p class="no-comment">No comments.</p>`
                    : comments.map(commentTemplate)
                }
            </ul>
        </div>

        <!-- Edit/Delete buttons ( Only for creator of this game )  -->
        ${isOwner
        ? html`<div class="buttons">
            <a href="/edit/${game._id}" class="button">Edit</a>
            <a href="javascript:void(0)" @click=${onDelete} class="button">Delete</a>
        </div>`
        : null
        }
    </div>

    ${isOwner || !isLoggedIn ? null : html`<article class="create-comment">
        <label>Add new comment:</label>
        <form @submit=${onComment} class="form">
            <textarea name="comment" placeholder="Comment......"></textarea>
            <input class="btn submit" type="submit" value="Add Comment">
        </form>
    </article>`}
</section>`;

const commentTemplate = (comment) => html`
                <li class="comment">
                    <p>Content: ${comment.comment}</p>
                </li>`;

export async function detailsPage(ctx) {
    const gameId = ctx.params.id;
    const game = await getGameById(gameId);
    const userData = getUserData();

    const isLoggedIn = userData ? true : false;
    const isOwner = userData && userData.id == game._ownerId;

    const comments = await getCommentsForGameById(gameId);

    ctx.render(template(game, isOwner, onDelete, isLoggedIn, comments, onComment));

    async function onDelete() {
        const choice = confirm('Are you sure you want to delete this game?');

        if (choice) {
            await deleteGameById(gameId);
            ctx.page.redirect('/');
        }
    }

    async function onComment(ev) {
        ev.preventDefault();
        const form = new FormData(ev.target);
        const comment = form.get('comment');

        await createCommentForGame({ gameId, comment });
        ctx.page.redirect('/details/' + gameId);
    }
}