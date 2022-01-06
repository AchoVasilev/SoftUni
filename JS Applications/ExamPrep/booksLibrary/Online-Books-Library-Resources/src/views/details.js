import { deleteBookById, getBookById, getBookLikes, getMyLikeByBookId, likeBook } from '../api/data.js';
import { getUserData } from '../api/storage.js';
import { html } from '../lib.js';

const detailsTemplate = (book, isOwner, onDelete, likes, showLikeBtn, onLike) => html`
<section id="details-page" class="details">
    <div class="book-information">
        <h3>${book.title}</h3>
        <p class="type">Type: ${book.type}</p>
        <p class="img"><img src=${book.imageUrl}></p>
        <div class="actions">
            ${
                bookControlsTemplate(book, isOwner, onDelete)
            }

            ${
                likesControlsTemplate(showLikeBtn, onLike)
            }

            <div class="likes">
                <img class="hearts" src="/images/heart.png">
                <span id="total-likes">Likes: ${likes}</span>
            </div>
           
        </div>
    </div>
    <div class="book-description">
        <h3>Description:</h3>
        <p>${book.description}</p>
    </div>
</section>`;

const bookControlsTemplate = (book, isOwner, onDelete) => {
    if (isOwner) {
        return html`<a class="button" href="/edit/${book._id}">Edit</a>
        <a class="button" @click=${onDelete} href="javascript:void(0)">Delete</a>`;
    } else {
        return null;
    }
};

const likesControlsTemplate = (showLikeBtn, onLike) => {
    if (showLikeBtn) {
        return html`<a class="button" @click=${onLike} href="javascript:void(0)">Like</a>`;
    } else {
        return null;
    }
};

export async function detailsPage(ctx) {
    const bookId = ctx.params.id;
    const userData = getUserData();

    const [book, likes, hasLike] = await Promise.all([
        getBookById(bookId),
        getBookLikes(bookId),
        userData ? getMyLikeByBookId(userData.id, bookId) : 0
    ]);
 
    const isOwner = userData && userData.id == book._ownerId;
    const showLikeBtn =  userData != null && isOwner == false && hasLike == false;

    ctx.render(detailsTemplate(book, isOwner, onDelete, likes, showLikeBtn, onLike));

    async function onDelete() {
        const choice = confirm('Are you sure you want to delete this book?');

        if (choice) {
            await deleteBookById(bookId);
            ctx.page.redirect('/');
        }
    }

    async function onLike() {
        await likeBook(bookId);
        ctx.page.redirect('/details/' + bookId);
    }
}