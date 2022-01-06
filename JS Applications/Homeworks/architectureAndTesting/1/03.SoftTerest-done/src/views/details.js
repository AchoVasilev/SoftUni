import { showSection, create } from "../helper.js";
import { getIdeaById } from '../api/data.js';
import { deleteIdeaById } from '../api/data.js';

const section = document.getElementById('detailsPage');
let ctx = null;

export function showDetails(contex, id) {
    ctx = contex;
    showSection(section);
    loadIdea(id)
}

async function loadIdea(id) {
    const idea = await getIdeaById(id);

    section.innerHTML = `<img class="det-img" src="${idea.img}" />
    <div class="desc">
        <h2 class="display-5">${idea.title}</h2>
        <p class="infoType">Description:</p>
        <p class="idea-description">${idea.description}</p>
    </div>`;

    const userInfo = JSON.parse(sessionStorage.getItem('userInfo'));
    if (userInfo != null && userInfo.id == idea._ownerId) {
        const delBtn = create('div', {class: 'text-center'});
        const aElement = create('a', {class: 'btn detb', href: '', id}, 'Delete');
        delBtn.appendChild(aElement);
        section.appendChild(delBtn);
        aElement.addEventListener('click', onDelete)
    }
}

async function onDelete(event) {
    event.preventDefault();
    
    const id = event.target.id;
    const confirmed = confirm('Delete this idea?Are you sure?')
    if (confirmed) {

        await deleteIdeaById(id);
        ctx.goTo('dashboardLink');
    }
}