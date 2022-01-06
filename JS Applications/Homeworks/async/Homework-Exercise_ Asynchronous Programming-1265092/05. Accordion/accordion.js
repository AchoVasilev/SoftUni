
// /* 
//     2 SOLUTIONS

// SOLUTION NUMBER 1 - WITH PARTLY FETCHING 
// SOLUTION NUMBER 2 - WITH FULL FETCHING BEFORE DISPLAYING HTML ELEMENTS

// CHECK COMMENTED CODE BELOW

// */

// /*
//     SOLUTION NUMBER 1
// */

// function solution() {
//     const articlesURL = 'http://localhost:3030/jsonstore/advanced/articles/list';
//     const articleDetailsURL = "http://localhost:3030/jsonstore/advanced/articles/details/";

//     const mainEl = document.getElementById('main');
//     const loadingEl = newEl('p', {}, 'Loading...');

//     mainEl.appendChild(loadingEl);

//     getArticlesInfo()
//         .then(articlesInfo => {
//             loadingEl.remove();

//             articlesInfo.forEach(articleInfo => {
//                 const { _id: id, title } = articleInfo;
//                 const articleDiv = createArticle(id, title);
//                 mainEl.appendChild(articleDiv);
//             });

//             mainEl.addEventListener('click', onClick);
//         })
//         .catch(err => {
//             loadingEl.remove();

//             const errorEl = newEl('p', {}, `Error: ${err.message}`);
//             mainEl.appendChild(errorEl);
//         });

//     async function getArticlesInfo() {
//         const articlesResponse = await fetch(articlesURL);
//         const articles = await articlesResponse.json();

//         return articles;
//     }

//     async function onClick(ev) {
//         if (ev.target.classList.contains('button')) {
//             const button = ev.target;
//             const extraEl = button.parentElement.nextElementSibling
//             if (button.textContent === 'More' || button.textContent === 'Error') {
//                 button.textContent = 'Loading...';
//                 button.disabled = true;
//                 try {
//                     const articleContent = await getArticle(button.id);
//                     extraEl.textContent = articleContent;
//                     extraEl.style.display = 'block';
//                     button.textContent = 'Less';
//                     button.disabled = false;

//                 } catch (err) {
//                     button.disabled = false;
//                     button.textContent = 'Error';
//                 }
//             } else {
//                 extraEl.style.display = 'none';
//                 extraEl.textContent = '';
//                 button.textContent = 'More';
//             }
//         }
//     }

//     async function getArticle(id) {
//         const articleURL = articleDetailsURL + id;

//         const articleResponse = await fetch(articleURL);
//         const { content } = await articleResponse.json();

//         return content;
//     }

//     function createArticle(id, title) {
//         const spanEl = newEl('span', {}, title);
//         const buttonEl = newEl('button', { class: 'button', id: id }, 'More');
//         const paragraphEl = newEl('p', {}, '');

//         const headDiv = newEl('div', { class: 'head' }, spanEl, buttonEl);
//         const extraDiv = newEl('div', { class: 'extra' }, paragraphEl);

//         const accordionDiv = newEl('div', { class: 'accordion' }, headDiv, extraDiv);

//         return accordionDiv;
//     }

//     function newEl(type, attr, ...content) {
//         const element = document.createElement(type);

//         for (const prop in attr) {
//             if (prop === 'class') {
//                 let classes = attr[prop].split(' ');
//                 for (let cls of classes) {
//                     element.classList.add(cls);
//                 }
//             } else {
//                 element.setAttribute(prop, attr[prop]);
//             }
//         }

//         for (let item of content) {
//             if (typeof item == 'string' || typeof item == 'number') {
//                 item = document.createTextNode(item);
//             }
            
//             element.appendChild(item);
//         }
//         return element;
//     }
// }

// solution();



/*
    SOLUTION NUMBER 2
*/

function solution() {
    const URL = 'http://localhost:3030/jsonstore/advanced/articles/list';

    const mainEl = document.getElementById('main');
    const loadingEl = newEl('p', {}, 'Loading...');

    mainEl.appendChild(loadingEl);

    getArticles()
        .then(articles => {
            loadingEl.remove();

            articles.forEach(articleInfo => {
                const articleDiv = createArticle(articleInfo);
                mainEl.appendChild(articleDiv);
            });

            mainEl.addEventListener('click', function (ev) {
                if (ev.target.tagName === 'BUTTON') {
                    const button = ev.target;
                    const extraEl = button.parentElement.nextElementSibling;
                    if (button.textContent === 'More') {
                        extraEl.style.display = 'block';
                        button.textContent = 'Less';
                    } else {
                        extraEl.style.display = 'none';
                        button.textContent = 'More';
                    }
                }
            });
        })
        .catch(err => {
            loadingEl.remove();

            mainEl.appendChild(
                newEl('p', {}, 'Error: ' + err.message)
            );
        });

    async function getArticles() {
        const articlesIDs = await getArticlesIDs();
        let articles = [];

        for (let id of articlesIDs) {
            const article = await getArticle(id);
            articles.push(article);
        }

        return articles;
    }

    async function getArticlesIDs() {
        const articlesResponse = await fetch(URL);
        const articles = await articlesResponse.json();

        const IDs = articles.map(article => article._id);

        return IDs;
    }

    async function getArticle(id) {
        const articlesURL = "http://localhost:3030/jsonstore/advanced/articles/details/";
        const articleURL = articlesURL + id;

        const articleResponse = await fetch(articleURL);
        const article = await articleResponse.json();

        return article;
    }

    function createArticle({ _id: id, title, content }) {
        const spanEl = newEl('span', {}, title);
        const buttonEl = newEl('button', { class: 'button', id: id }, 'More');
        const paragraphEl = newEl('p', {}, content);

        const headDiv = newEl('div', { class: 'head' }, spanEl, buttonEl);
        const extraDiv = newEl('div', { class: 'extra' }, paragraphEl);

        const accordionDiv = newEl('div', { class: 'accordion' }, headDiv, extraDiv);

        return accordionDiv;
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
}

solution();