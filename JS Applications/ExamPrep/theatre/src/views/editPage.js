import { editTheatreById, getTheatreById } from '../api/data.js';
import { html } from '../lib.js';

const template = (theatre, onSubmit) => html`
<section id="editPage">
    <form @submit=${onSubmit} class="theater-form">
        <h1>Edit Theater</h1>
        <div>
            <label for="title">Title:</label>
            <input id="title" name="title" type="text" placeholder="Theater name" value="${theatre.title}">
        </div>
        <div>
            <label for="date">Date:</label>
            <input id="date" name="date" type="text" placeholder="Month Day, Year" value="${theatre.date}">
        </div>
        <div>
            <label for="author">Author:</label>
            <input id="author" name="author" type="text" placeholder="Author" value="${theatre.author}">
        </div>
        <div>
            <label for="description">Theater Description:</label>
            <textarea id="description" name="description"
                placeholder="Description">${theatre.description}</textarea>
        </div>
        <div>
            <label for="imageUrl">Image url:</label>
            <input id="imageUrl" name="imageUrl" type="text" placeholder="Image Url"
                value="${theatre.imageUrl}">
        </div>
        <button class="btn" type="submit">Submit</button>
    </form>
</section>`;

export async function editPage(ctx) {
    const theatreId = ctx.params.id;

    const theatre = await getTheatreById(theatreId);

    ctx.render(template(theatre, onSubmit));

    async function onSubmit(ev) {
        ev.preventDefault();

        const form = new FormData(ev.target);
        const title = form.get('title').trim();
        const date = form.get('date').trim();
        const author = form.get('author').trim();
        const description = form.get('description').trim();
        const imageUrl = form.get('imageUrl').trim();

        if (title == '' || date == '' || author == '' || description == '' || imageUrl == '') {
            return alert('All fields are required!');
        }

        await editTheatreById(theatreId,
            {
            title,
            date,
            author,
            description,
            imageUrl
        });

        ctx.page.redirect('/details/' + theatreId);
    }
}