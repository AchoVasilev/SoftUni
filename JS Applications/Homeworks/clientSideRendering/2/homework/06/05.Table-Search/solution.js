import { render } from '../node_modules/lit-html/lit-html.js';
import { allStudentsTemplate } from './templates/tableTemplates.js';

document.querySelector('#searchBtn').addEventListener('click', onClick);
const table = document.getElementById('table');

let studentsData = [];
loadStudents();

async function loadStudents() {
   const url = 'http://localhost:3030/jsonstore/advanced/table';
   const response = await fetch(url);
   const data = await response.json();
   
   studentsData = Object.values(data)
   
   render(allStudentsTemplate(studentsData),table);
}

function onClick() {
   let searchInput = document.getElementById('searchField');
   let searchText = searchInput.value.toLowerCase();

   let allStudents = studentsData.map(s => Object.assign({},s));

   let matchedStudents = allStudents
   .filter(s => Object.values(s).some(val => val.toLowerCase().includes(searchText)));

   matchedStudents.forEach(s => s.class = 'select');

   searchInput.value = '';
   render(allStudentsTemplate(allStudents),table);
}
