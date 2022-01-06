function attachEvents() {

    const postsURL = 'http://localhost:3030/jsonstore/blog/posts';
    const commentsURL = 'http://localhost:3030/jsonstore/blog/comments';

    const loadPostsButton = document.getElementById('btnLoadPosts');
    const viewPostButton = document.getElementById('btnViewPost');
    const postsMenuButton = document.getElementById('posts');

    const postTitleEl = document.getElementById('post-title');
    const postContentEl = document.getElementById('post-body');
    const postCommentsUL = document.getElementById('post-comments');

    const postsInfo = {};
    const loadingEl = newEl('option', {}, 'Loading...');
    const errorEl = newEl('option', {}, 'Error');

    loadPostsButton.addEventListener('click', onClickLoadButton);
    viewPostButton.addEventListener('click', onClickViewButton);

    function onClickViewButton(ev) {
        const currentPostID = postsMenuButton.value;

        postTitleEl.textContent = 'Post Details';

        switchButtons();
        loadPost();

        getPostComments(currentPostID)
            .then(comments => {
                switchButtons();
                showPostAndComments(currentPostID, comments)
            })
            .catch(onErrorCatch);

    }

    function onClickLoadButton(ev) {
        switchButtons();
        clearMenu();
        loadMenu();
        loadPost();

        postTitleEl.textContent = 'Post Details';

        getPostsInfo()
            .then(posts => {
                switchButtons();
                clearMenu();
                clearComments();

                postContentEl.textContent = '';

                for (let postID in posts) {
                    const { body: content, id, title } = posts[postID];
                    attachPostOption(id, title);
                    savePostContent(id, content, title);
                }
            })
            .catch(onErrorCatch);
    }

    function onErrorCatch(err) {
        clearMenu();
        clearComments();
        switchButtons();
        displayError();
    }

    function loadPost() {
        clearComments();

        postContentEl.textContent = 'Loading...';
        const commentLI = newEl('p', {}, 'Loading...');
        postCommentsUL.appendChild(commentLI);
    }

    function showPostAndComments(currentPostID, comments) {
        postContentEl.textContent = postsInfo[currentPostID].content;
        postTitleEl.textContent = postsInfo[currentPostID].title;

        clearComments();

        comments.forEach(comment => {
            const commentLI = newEl('li', { id: comment.id }, comment.text);
            postCommentsUL.appendChild(commentLI);
        });
    }

    function clearComments() {
        postCommentsUL.replaceChildren();
    }

    function loadMenu() {
        postsMenuButton.appendChild(loadingEl);
    }

    function clearMenu() {
        postsMenuButton.replaceChildren();
    }

    function savePostContent(id, content, title) {
        postsInfo[id] =
        {
            content,
            title
        };
    }

    async function getPostsInfo() {
        const response = await fetch(postsURL);
        const posts = await response.json();

        return posts;
    }

    async function getPostComments(postID) {

        if (postID === 'Error' || postID === '') {
            throw new Error('Invalid ID');
        }

        const response = await fetch(commentsURL);
        const commentsInfo = await response.json();

        allCommentsInfo = Object.values(commentsInfo);

        comments =
            allCommentsInfo
                .filter(commentInfo => commentInfo.postId === postID);

        return comments;
    }

    function attachPostOption(id, title) {
        const optionEL = newEl('option', { value: id }, title);
        postsMenuButton.appendChild(optionEL);
    }

    function newEl(type, attr, ...content) {
        const element = document.createElement(type);

        for (const prop in attr) {
            if (prop === 'class') {
                let classes = attr[prop].split(' ');
                for (let cls of classes) {
                    element.classList.add(cls);
                }
            } else {
                element.setAttribute(prop, attr[prop]);
            }
        }

        for (let item of content) {
            if (typeof item == 'string' || typeof item == 'number') {
                item = document.createTextNode(item);
            }

            element.appendChild(item);
        }
        return element;
    }

    function switchButtons() {
        if (loadPostsButton.disabled && viewPostButton.disabled) {
            loadPostsButton.disabled = false;
            viewPostButton.disabled = false;
        } else {
            loadPostsButton.disabled = true;
            viewPostButton.disabled = true;
        }
    }

    function displayError() {
        postContentEl.textContent = 'Error';
        const commentLI = newEl('p', {}, 'Error');

        postCommentsUL.appendChild(commentLI);
        postsMenuButton.appendChild(errorEl);
    }
}

attachEvents();