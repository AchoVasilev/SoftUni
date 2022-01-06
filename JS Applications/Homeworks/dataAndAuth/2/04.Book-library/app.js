function solve(){
    const table = document.querySelector('tbody');
    const load = document.getElementById('loadBooks');
    const form = document.querySelector('form');

    load.addEventListener('click', onLoad);
    form.addEventListener('submit', onSubmit);

    function onSubmit(e) {
        if (form.children[5].textContent == 'Save'){
            update(e);
        } else if (form.children[5].textContent == 'Submit'){
            create(e);
        }
    }

    async function update(e){
        e.preventDefault();
        const formData = new FormData(e.target);
        const data = {
            "author": formData.get('author'),
            "title": formData.get('title')
        }
        const id = e.target.children[0].getAttribute('id');
        const url = `http://localhost:3030/jsonstore/collections/books/${id}`;

        const res = await fetch(url, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data)
        });

        e.target.reset();
        form.children[0].textContent = 'FORM';
        form.children[0].removeAttribute('id');
        form.children[5].textContent = 'Submit';
    }

    async function create(e) {
        e.preventDefault();
        const formData = new FormData(e.target);
        const data = {
            "author": formData.get('author'),
            "title": formData.get('title')
        }
        const url = 'http://localhost:3030/jsonstore/collections/books';

        await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        });

        e.target.reset();
    }

    async function onLoad(e) {
        table.innerHTML = '';
        const url = 'http://localhost:3030/jsonstore/collections/books';

        const res = await fetch(url);
        const data = await res.json();

        for (const [key, value] of Object.entries(data)) {
            table.innerHTML += `<tr>
            <td>${value.title}</td>
            <td>${value.author}</td>
            <td>
            <button name="edit" id="${key}">Edit</button>
            <button name="del" id="${key}">Delete</button>
            </td>
            </tr>`;
        }
        const edit_buttons = Array.from(document.querySelectorAll('button[name=edit]'));
        const del_buttons = Array.from(document.querySelectorAll('button[name=del]'));

        edit_buttons.forEach(butt => {
            butt.addEventListener('click', onEdit);
        });

        del_buttons.forEach(butt => {
            butt.addEventListener('click', onDelete);
        });

    }

    async function onEdit(e){
        const row = e.target.parentElement.parentElement;
        form.children[0].textContent = 'Edit FORM';
        form.children[5].textContent = 'Save';
        form.elements['title'].value = row.children[0].textContent;
        form.elements['author'].value = row.children[1].textContent;

        const id = e.target.getAttribute('id');
        form.children[0].setAttribute('id', id);
    }

    async function onDelete(e){
        const id = e.target.getAttribute('id');
        const url = `http://localhost:3030/jsonstore/collections/books/${id}`;

        await fetch(url, {
            method: "DELETE"
        });

        load.dispatchEvent(new Event('click'));
    }
}

solve();