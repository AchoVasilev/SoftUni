import { showSection } from "../helper.js";
import { createIdea } from "../api/data.js";

const section = document.getElementById('createPage');
const form = section.querySelector('form');
form.addEventListener('submit', onSubmit);
let ctx = null;

export function showCreate(contex) {
    ctx = contex;
    showSection(section);
}

async function onSubmit(event) {
    event.preventDefault();

    const formData = new FormData(form);
    const title = formData.get('title');
    const description = formData.get('description');
    const img = formData.get('imageURL');

    try {
        if (title == '' || description == '' || img == '') {
            throw new Error('All fields are required');
        }
        if (title.length < 6) {
            throw new Error('Title shoud be at least 6 characters long');
        }

        if (description.length < 10) {
            throw new Error('Description shoud be at least 10 characters long');
        }
        
        if (img.length < 5) {
            throw new Error('Image field shoud be at least 5 characters long');
        }
        createIdea({title, description, img})
        form.reset();
        ctx.goTo('dashboardLink');

    } catch (error) {
        alert(error.message);
    }
}