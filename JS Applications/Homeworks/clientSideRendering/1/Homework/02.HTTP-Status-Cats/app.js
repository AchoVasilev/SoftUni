import { html, render } from './node_modules/lit-html/lit-html.js';
import { cats as catData} from './catSeeder.js';

// template:
// constains cat info 
// has toggle button

const catCard = (cat) => html` 
<li>
    <img src="./images/${cat.imageLocation}.jpg" width="250" height="250" alt="Card image cap">
    <div class="info">
        <button class="showBtn">Show status code</button>
        <div class="status" style="display: none" id="${cat.id}">
            <h4>Status Code: ${cat.statusCode}</h4>
            <p>${cat.statusMessage}</p>
        </div>
    </div>
</li>`

//start:
//parse impoerted data
//pass to template

const root = document.getElementById('allCats');
render(html`<ul>${catData.map(catCard)}</ul>`,root)

root.addEventListener('click', (ev) => {
 if(ev.target.tagName == "BUTTON"){
     const element = ev.target.parentNode.querySelector('.status');
     if(element.style.display == 'none'){
         element.style.display = 'block';
         ev.target.textContent = 'Hide status code';
     }else{
         element.style.display = 'none';
         ev.target.textContent = 'Show status code'
     }

 }
});
// My solve 

/*
import { html, render } from '../node_modules/lit-html/lit-html.js';
import { styleMap } from '../node_modules/lit-html/directives/style-map.js';
import { cats } from './catSeeder.js';

const allCats = document.getElementById('allCats');

allCats.addEventListener('click', toggle)

const cardTemplate = (data) => html`
<ul>
    ${data.map(cat => html`
    <li>
        <img src="./images/${cat.imageLocation}.jpg" width="250" height="250" alt="Card image cap">
        <div class="info">
            <button class="showBtn">${cat.info ? 'Hide' : 'Show'} status code</button>
            <div class="status" style=${styleMap(cat.info ? {} : {display : 'none'})} id=${cat.id}>
                <h4>Status Code: ${cat.statusCode}</h4>
                <p>${cat.statusMessage}</p>
            </div>
        </div>
    </li>`)}
</ul>`;

cats.forEach(c => c.info = false);
update();

function update() {
    const result = cardTemplate(cats);
    render(result, allCats);
}

function toggle(event) {
    const elementId = event.target.parentNode.querySelector('.status').id;
    const cat = cats.find(c => c.id == elementId);
    cat.info = !cat.info;
    update();
}
*/