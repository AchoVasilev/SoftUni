//load data -> populate form -> validation -> async request
//initialization
// - find relevant section
// - detach section from DOM //it remains in memory

import { showView } from "./dom.js";

const section = document.getElementById('edit-movie');
section.remove();

//display logic

export function showEdit(){
    showView(section)
}

// async function onEditSubmit(e) {
//     e.preventDefault();
//     const formData = new FormData(e.target); // e tyrget bec we didnt take the form

//     const id = formData.get('id');
//     const author = formData.get('author');
//     const title = formData.get('title');

//     const result = await updateBook(id, { author, title });
//     // tbody.appendChild(createRow(result._id, result));
//     e.target.reset(); // reset the form
//     createForm.style.display = 'block';
//     editForm.style.display = 'none';

//     loadBooks();

// }