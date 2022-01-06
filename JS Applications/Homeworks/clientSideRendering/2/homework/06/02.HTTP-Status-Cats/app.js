import { render } from "../node_modules/lit-html/lit-html.js";
import { cats } from "./catSeeder.js";
import { allCatsTemplate } from "./templates/catTemplate.js";

const allCatsSection = document.getElementById('allCats');
allCatsSection.addEventListener('click', showDetails);

function onRender() {
    render(allCatsTemplate(cats),allCatsSection);
}
onRender();

function showDetails(e) {
    if(e.target.tagName == 'BUTTON') {
        if(e.target.textContent == 'Show status code') {
            e.target.nextElementSibling.style.display = 'block';
            e.target.textContent = 'Hide status code'
        } else {
            e.target.nextElementSibling.style.display = 'none';
            e.target.textContent = 'Show status code'
        }
    }
}