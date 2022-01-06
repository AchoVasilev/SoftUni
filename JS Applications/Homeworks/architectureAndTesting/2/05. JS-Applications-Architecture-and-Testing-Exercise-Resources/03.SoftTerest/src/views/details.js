import { deleteIdea, getIdeaById } from "../api/data.js";
import { createElement } from "../helpers.js";

let detailsDiv = undefined;
let mainContainer = undefined;
let detailsContext = undefined

function initialize(domElement, mainElement) {
    detailsDiv = domElement;
    mainContainer = mainElement;
}

async function showDetailsPage(context, id) {
    detailsContext = context
    const ideaObject = await getIdeaById(id)
    detailsDiv.replaceChildren(createIdeaDetails(ideaObject))
    context.showSection(detailsDiv, mainContainer)
}
function createIdeaDetails(ideaObject) {
    const fragment = document.createDocumentFragment()
    const ideaDetails = createElement('img', { class: "det-img", src: ideaObject.img });
    const detailsView = createElement('div', { class: "desc" },
        createElement('h2', { class: "display-5" }, ideaObject.title),
        createElement('p', { class: "infoType" }, "Description:"),
        createElement('p', { class: "idea-description" }, ideaObject.description)
    )
    fragment.appendChild(ideaDetails)
    fragment.appendChild(detailsView)
    const deleteButtonElement = createElement('div', { class: "text-center" },
        createElement('a', { class: "btn detb", href: "", "data-id": ideaObject._id }, "Delete")
    )
    const userData = sessionStorage.getItem('userData');
    if (userData && JSON.parse(sessionStorage.getItem('userData')).id== ideaObject._ownerId) {
        fragment.appendChild(deleteButtonElement);
        deleteButtonElement.addEventListener('click', onDelete);
    };
    return fragment
}

async function onDelete(event) {
    event.preventDefault();
    const imageId = event.target.dataset.id
    const confirmation = confirm("Are you sure you wan\'t to delete Idea");
    if (confirmation) {
        await deleteIdea(imageId);
        detailsContext.redirectTo('dashboard')
    }
}


const detailsView = {
    initialize,
    showDetailsPage
}
export default detailsView
