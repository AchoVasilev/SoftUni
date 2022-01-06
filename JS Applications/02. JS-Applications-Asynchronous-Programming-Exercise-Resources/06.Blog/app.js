function attachEvents() {
    let loadBtn = document.getElementById('btnLoadPosts');
    loadBtn.addEventListener('click', getAllPosts);

    let viewBtn = document.getElementById('btnViewPost');
    viewBtn.addEventListener('click', displayPost);
}

attachEvents();

async function displayPost(ev) {
    let btn = ev.target;
    btn.disabled = true;
    btn.style.background = 'grey';

    let titleElement = document.getElementById('post-title')
    let bodyElement = document.getElementById('post-body');
    let ulElement = document.getElementById('post-comments');

    titleElement.textContent = 'Loading...';
    bodyElement.textContent = '';
    ulElement.replaceChildren();

    let selectedId = document.getElementById('posts').value;

    let [post, comments] = await Promise.all([
        getPostById(selectedId),
        getCommentsByPostId(selectedId)
    ]);

    titleElement.textContent = post.title;
    bodyElement.textContent = post.body;


    comments.forEach(c => {
        let liElement = document.createElement('li');
        liElement.textContent = c.text;
        ulElement.appendChild(liElement);
    });

    btn.disabled = false;
    btn.style.background = '#45A049';
}

async function getAllPosts() {
    let loadBtn = document.getElementById('btnLoadPosts');
    loadBtn.disabled = true;
    loadBtn.style.background = 'grey';

    const url = 'http://localhost:3030/jsonstore/blog/posts';
    let response = await fetch(url);

    let posts = await response.json();

    let selectElement = document.getElementById('posts');
    selectElement.replaceChildren();

    Object.values(posts).forEach(p => {
        let optionElement = document.createElement('option');
        optionElement.textContent = p.title;

        optionElement.value = p.id;

        selectElement.appendChild(optionElement);
    });

    loadBtn.disabled = false;
    loadBtn.style.background = '#45A049';
}

async function getPostById(postId) {
    let url = `http://localhost:3030/jsonstore/blog/posts/${postId}`;

    let response = await fetch(url);

    let post = await response.json();

    return post;
}

async function getCommentsByPostId(postId) {
    const url = 'http://localhost:3030/jsonstore/blog/comments';

    let response = await fetch(url);

    let comments = await response.json();

    let filteredComments = Object.values(comments).filter(c => c.postId === postId);

    return filteredComments;
}