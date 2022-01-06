import { showSection } from "../helper.js";

let ctx = null;
const section = document.getElementById('homePage');
section.querySelector('#getStartedLink').addEventListener('click', (event) => {
    event.preventDefault();
    ctx.goTo('getStartedLink');
})


export function showHome(contex) {
    ctx = contex;
    showSection(section);
    ctx.updateNav();
}