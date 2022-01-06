import { createIdea } from "../api/data.js";

let createDiv = undefined;
let mainContainer = undefined;
let createContext = undefined

function initialize(domElement, mainElement){
    createDiv = domElement;
    mainContainer = mainElement;
    let form = createDiv.querySelector('form');
    form.addEventListener('submit', onSubmit);
}

async function showCreatePage(context) {
    createContext = context
    context.showSection(createDiv,mainContainer)
}

async function onSubmit(event) {
    event.preventDefault();

    const form = new FormData(event.target);
    const title = form.get('title');
    const description = form.get('description');
    const img = form.get('imageURL');

    if (title.length < 6) {
        return alert("The title should be at least 6 characters long");
    };
    if (description.length < 10) {
        return alert("The description should be at least 10 characters long");
    };
    if (img.length < 5) {
        return alert ("The image should be at least 5 characters long.")
    };

    await createIdea({title, description, img})
    createContext.redirectTo('dashboard')

    event.target.reset()
}

const createView = {
    initialize,
    showCreatePage
}
export default createView