window.addEventListener('load', solve);

function solve() {
    let addBtn = document.getElementById('add-btn');

    addBtn.addEventListener('click', addSongHandler);

    function addSongHandler(ev) {
        ev.preventDefault();

        let genreElement = document.getElementById('genre');
        let songNameElement = document.getElementById('name');
        let authorElement = document.getElementById('author');
        let dateElement = document.getElementById('date');

        // @ts-ignore
        if (genreElement.value.trim() == '' || songNameElement.value.trim() == '' || authorElement.value.trim() == '' || dateElement.value.trim() == '') {
            return;
        }

        let divClass = document.createElement('div');
        divClass.classList.add('hits-info');

        let imgElement = document.createElement('img');
        imgElement.src = './static/img/img.png';

        let genreHeadingElement = document.createElement('h2');
        // @ts-ignore
        genreHeadingElement.textContent = `Genre: ${genreElement.value}`;

        let nameHeadingElement = document.createElement('h2');
        // @ts-ignore
        nameHeadingElement.textContent = `Name: ${songNameElement.value}`;

        let authorHeadingElement = document.createElement('h2');
        // @ts-ignore
        authorHeadingElement.textContent = `Author: ${authorElement.value}`;

        let dateHeadingElement = document.createElement('h3');
        // @ts-ignore
        dateHeadingElement.textContent = `Date: ${dateElement.value}`;

        let saveBtn = document.createElement('button');
        saveBtn.classList.add('save-btn');
        saveBtn.textContent = 'Save song';
        saveBtn.addEventListener('click', saveSongHandler);

        let likeBtn = document.createElement('button');
        likeBtn.classList.add('like-btn');
        likeBtn.textContent = 'Like song';
        likeBtn.addEventListener('click', likeSongHandler);

        let deleteBtn = document.createElement('button');
        deleteBtn.classList.add('delete-btn');
        deleteBtn.textContent = 'Delete';
        deleteBtn.addEventListener('click', deleteSongHandler);

        divClass.appendChild(imgElement);
        divClass.appendChild(genreHeadingElement);
        divClass.appendChild(nameHeadingElement);
        divClass.appendChild(authorHeadingElement);
        divClass.appendChild(dateHeadingElement);
        divClass.appendChild(saveBtn);
        divClass.appendChild(likeBtn);
        divClass.appendChild(deleteBtn);

        let allHitsDiv = document.querySelector('#all-hits .all-hits-container');
        allHitsDiv.appendChild(divClass);

        // @ts-ignore
        genreElement.value = '';
        // @ts-ignore
        songNameElement.value = '';
        // @ts-ignore
        authorElement.value = '';
        // @ts-ignore
        dateElement.value = '';
    }

    function saveSongHandler(ev) {
        let savedSongsDiv = document.querySelector('#saved-hits .saved-container');
        let songToSave = ev.target.parentElement;

        ev.target.nextElementSibling.remove();

        savedSongsDiv.appendChild(songToSave);

        ev.target.remove();
    }

    function likeSongHandler(ev) {
        let paragraph = document.querySelector('#total-likes .likes p');
        // @ts-ignore
        let number = Number(paragraph.textContent.split(': ')[1]);
        number = number + 1;
        paragraph.textContent = `Total Likes: ${number}`;

        ev.target.disabled = true;
    }

    function deleteSongHandler(ev) {
        let hitsDiv = ev.target.parentElement;

        hitsDiv.remove();
    }
}