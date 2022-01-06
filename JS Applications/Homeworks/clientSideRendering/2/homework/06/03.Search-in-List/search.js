import { render } from './../node_modules/lit-html/lit-html.js';
import { ulTownsTemplate } from './templates/townsTemplate.js';
import { towns } from './towns.js';

let baseTowns = towns.map(t => ({name:t}));
const townsDiv = document.getElementById('towns');
render(ulTownsTemplate(baseTowns),townsDiv);

const serachBtn = document.getElementById('search-btn');
serachBtn.addEventListener('click', search);

function search() {
   let inputEl = document.getElementById('searchText');
   let searchText = inputEl.value.toLowerCase();

   let allTowns = towns.map(t => ({name:t}));

   let matchedTowns = allTowns.filter(t => t.name.toLowerCase().includes(searchText));
   matchedTowns.forEach(t => t.class = 'active');

   render(ulTownsTemplate(allTowns),townsDiv);
}

