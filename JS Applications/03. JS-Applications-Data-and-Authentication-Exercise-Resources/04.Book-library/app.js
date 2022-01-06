const tbody = document.querySelector('tbody');

const loadBtn = document.getElementById('loadBooks');
loadBtn.addEventListener('click', loadBooks);

const createForm = document.getElementById('createForm');
createForm.addEventListener('submit', onCreate);

const editForm = document.getElementById('editForm');
editForm.addEventListener('submit', onEdit);

const backBtn = document.getElementById('back');
backBtn.addEventListener('click', onBack);

tbody.addEventListener('click', onTableClick);

loadBooks();

async function loadBooks() {
    const books = await request('http://localhost:3030/jsonstore/collections/books');

    let result = Object.entries(books).map(([id, book]) => createRow(id, book));

    tbody.replaceChildren(...result);
    editForm.style.display = 'none';
}

function onBack() {
    createForm.style.display = 'block';
    editForm.style.display = 'none';
}

async function loadBookById(id) {
    const book = await request(`http://localhost:3030/jsonstore/collections/books/${id}`);

    return book;
}

function onTableClick(ev) {
    if (ev.target.className == 'delete') {
        onDelete(ev.target);
    } else if (ev.target.className == 'edit') {
        onEditBtnClick(ev.target);
    }
}

async function onEdit(ev) {
    ev.preventDefault();
    const formData = new FormData(ev.target);

    const author = formData.get('author');
    const title = formData.get('title');
    const id = formData.get('id');

    const result = await updateBook(id, { author, title });
    
    ev.target.reset();
    createForm.style.display = 'block';
    editForm.style.display = 'none';

    loadBooks();
}

async function onEditBtnClick(button) {
    const id = button.parentElement.dataset.id;
    const book = await loadBookById(id);

    createForm.style.display = 'none';
    editForm.style.display = 'block';

    editForm.querySelector('[name="author"]').value = book.author;
    editForm.querySelector('[name="title"]').value = book.title;
    editForm.querySelector('[name="id"]').value = id;
}

async function onDelete(button) {
    const id = button.parentElement.dataset.id;

    await deleteBook(id);

    button.parentElement.parentElement.remove();
}

async function onCreate(ev) {
    ev.preventDefault();

    createForm.style.display = 'block';
    editForm.style.display = 'none';

    const formData = new FormData(ev.target);

    if ([...formData.entries()].some(f => f[1] == '')) {
        alert('Please enter information for all fields!');
        return;
    }

    const author = formData.get('author');
    const title = formData.get('title');

    const book = {
        author,
        title
    };

    let authorStr = JSON.stringify(author);
    let titleStr = JSON.stringify(title);

    const result = await createBook(book);
    console.log(result);
    let row = createRow(result._id, result);
    tbody.appendChild(row);

    ev.target.reset();
}

function createRow(bookId, book) {
    let tr = document.createElement('tr');
    let titleTd = document.createElement('td');
    titleTd.textContent = book.title;

    let authorTd = document.createElement('td');
    authorTd.textContent = book.author;

    let td = document.createElement('td');
    td.setAttribute('data-id', bookId);

    let deleteBtn = document.createElement('button');
    deleteBtn.textContent = 'Delete';
    deleteBtn.classList.add('delete');

    let editBtn = document.createElement('button');
    editBtn.textContent = 'Edit';
    editBtn.classList.add('edit');

    td.appendChild(editBtn);
    td.appendChild(deleteBtn);

    tr.appendChild(titleTd);
    tr.appendChild(authorTd);
    tr.appendChild(td);

    return tr;
}

async function createBook(data) {
    const response = await request('http://localhost:3030/jsonstore/collections/books', {
        method: 'POST',
        body: JSON.stringify(data)
    });

    return response;
}

async function updateBook(bookId, data) {
    const response = await request(`http://localhost:3030/jsonstore/collections/books/${bookId}`, {
        method: 'PUT',
        body: JSON.stringify(data)
    });

    return response;
}

async function deleteBook(bookId) {
    const response = await request(`http://localhost:3030/jsonstore/collections/books/${bookId}`, {
        method: 'DELETE'
    });

    return response;
}

async function request(url, options) {
    if (options && options.body != undefined) {
        Object.assign(options, {
            headers: {
                'Content-Type': 'application/json'
            },
        });
    }

    const response = await fetch(url, options);
    const data = await response.json();

    if (response.ok != true) {
        alert(data.message);
        throw new Error(data.message);
    }

    return data;
}