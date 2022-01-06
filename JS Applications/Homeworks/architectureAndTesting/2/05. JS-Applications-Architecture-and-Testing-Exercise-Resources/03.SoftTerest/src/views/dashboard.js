import { getAllIdeas } from "../api/data.js";
import { createElement } from "../helpers.js";

let dashboardDiv = undefined;
let mainContainer = undefined;
let dashboardContext = undefined;

function initialize(domElement, mainElement) {
    dashboardDiv = domElement;
    mainContainer = mainElement;
}

async function showDashboardPage(context) {
    dashboardContext = context
    const allIdeas = await loadAllIdeas()
    if (allIdeas.length == 0){
        dashboardDiv.replaceChildren(createElement('h1', {}, "No ideas yet! Be the first one :)"))
    }else{
        dashboardDiv.replaceChildren(...allIdeas.map(createIdea))
    }
    context.showSection(dashboardDiv, mainContainer)
}
async function loadAllIdeas() {
    const allIdeas = await getAllIdeas()
    return allIdeas
}

function createIdea(ideaObject) {
    let newIdea =
        createElement('div', { class: "card overflow-hidden current-card details", style: "width: 20rem; height: 18rem;" },
            createElement('div', { class: "card-body" },
                createElement('p', { class: "card-text" }, ideaObject.title
                )
            ),
            createElement('img', {class: "card-image", src: ideaObject.img, alt: "Card image cap"}),
            createElement('a', {class: 'btn', href: "", "data-id": ideaObject._id, id: "details-link"}, "Details")
        )
    newIdea.querySelector(".btn").addEventListener('click', onDetails)   
    return newIdea
}
async function onDetails(event) {
    event.preventDefault();
    const ideaLink = event.target.id
    const ideaId = event.target.dataset.id
    dashboardContext.redirectTo('details', ideaId)

}


const dashboardView = {
    initialize,
    showDashboardPage,
}
export default dashboardView

