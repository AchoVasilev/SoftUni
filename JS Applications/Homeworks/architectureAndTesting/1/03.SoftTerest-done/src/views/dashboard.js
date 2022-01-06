import { showSection , create} from "../helper.js";
import { getAllIdeas } from '../api/data.js';

let ctx = null;
const section = document.getElementById('dashboard-holder');
section.addEventListener('click', onDetails)

export function showDashboard(contex) {
    ctx = contex;
    showSection(section);
    loadAllIdeas();
}

async function loadAllIdeas() {
    const ideas = await getAllIdeas();
    console.log(ideas)
    if ([...ideas].length == 0) {
        section.innerHTML = `<h1>No ideas yet! Be the first one :)</h1>`;

    } else {
        section.replaceChildren();
        ideas.map(createIdeaElement).forEach(el => section.appendChild(el))
    }
}

function createIdeaElement(idea) {
    const element = create('div', {class: 'card overflow-hidden current-card details'});
    element.style.width = '20rem';
    element.style.height = '18rem';

    element.innerHTML = `
<div class="card-body">
    <p class="card-text">${idea.title}</p>
</div>
<img class="card-image" src="${idea.img}" alt="Card image cap">
<a data-id="${idea._id}" class="btn" href="">Details</a>`;

    return element;
}

function onDetails(event) {
    event.preventDefault();

    if (event.target.tagName == 'A') {
        const id = event.target.dataset.id;
        ctx.goTo('detailsLink', id)
    }
}