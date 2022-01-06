window.addEventListener('load', solve);
const url = 'http://localhost:3030/jsonstore/collections/books';
const bookTable = document.querySelector('tbody');
const formElement = document.querySelector('form');
let globBookId = '';
const submitBtn = formElement.querySelector('button');
const formHead = formElement.querySelector('h3');

function solve() {
    document.getElementById('loadBooks').addEventListener('click', getAllBooks);
    formElement.addEventListener('submit', addBook);
    getAllBooks();
}


async function deleteRecord(id) {
    const urlBook = `${url}/${id}`;

    const options = {
        method: 'delete'
    }
    bookTable.textContent = "Loading ...";
    try {
        const res = await fetch(urlBook, options);
        if (res.ok == false) {
            throw new Error(res.statusText);
        }
        const data = res.json();
        getAllBooks();
        return data;
    } catch {
        alert(err.message);
    }
}

async function addBook(ev) {
    ev.preventDefault();
    const data = new FormData(formElement);
    if ([...data.entries()].some(f => f[1] == '')) {
        alert('Please enter information for all fields!');
    }
    bookTable.textContent = 'Loading ...';

    let localUrl = '';
    let book = {};
    let method = '';
    if (submitBtn.textContent == 'Submit') {
        [...data.entries()].forEach(f => book[f[0]] = f[1]);
        localUrl = url;
        method = 'post';

    } else {
        [...data.entries()].forEach(f => book[f[0]] = f[1]);
        localUrl = `${url}/${globBookId}`;
        method = 'put';


    }
    const options = {
        method,
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(book)
    }

    const res = await fetch(localUrl, options);
    const dataResult = await res.json();
    formElement.reset();
    getAllBooks();
}

async function getAllBooks() {
    bookTable.textContent = "Loading ..."
    const res = await fetch(url);
    const data = await res.json();
    bookTable.replaceChildren();
    Object.entries(data).forEach(book => {
        const editBtn = e('button', {}, 'Edit');
        const deleteBtn = e('button', {}, 'Delete');
        const tr = e('tr', {},
            e('td', {}, book[1].title),
            e('td', {}, book[1].author),
            e('td', {}, editBtn, deleteBtn)
        )
        deleteBtn.addEventListener('click', deleteRecord.bind(null, book[0]));
        editBtn.addEventListener('click', updateBook.bind(null, book));
        bookTable.appendChild(tr);

        submitBtn.textContent = 'Submit';
        formHead.textContent = "FORM";

        formElement.reset();
    });
}

function updateBook(book) {
    globBookId = book[0];
    submitBtn.textContent = 'Save';
    formHead.textContent = "Edit FORM";
    const data = new FormData(formElement);
    const inputs = document.querySelectorAll('input[type="text"]')
    inputs[0].value = book[1].title;
    inputs[1].value = book[1].author;

}



function e(type, attr, ...content) {
    const element = document.createElement(type);
    for (let prop in attr) {
        element[prop] = attr[prop];
    }
    for (let item of content) {
        if (typeof item == 'string' || typeof item == 'number') {
            item = document.createTextNode(item);
        }
        element.appendChild(item);
    }
    return element;
}