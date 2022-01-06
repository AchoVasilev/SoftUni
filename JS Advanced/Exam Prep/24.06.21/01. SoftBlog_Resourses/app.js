function solve() {
   let createBtn = document.querySelector('.site-content aside button.btn.create')

   createBtn.addEventListener('click', createArticleHandler);

   function createArticleHandler(ev) {
      ev.preventDefault();

      let creatorElement = document.getElementById('creator');
      let titleElement = document.getElementById('title');
      let categoryElement = document.getElementById('category');
      let contentElement = document.getElementById('content');

      let articleElement = document.createElement('article');

      let titleHeadingElement = document.createElement('h1');
      // @ts-ignore
      titleHeadingElement.textContent = titleElement.value;

      let categoryParagraphElement = document.createElement('p');
      categoryParagraphElement.textContent = 'Category:';

      let categoryStrongElement = document.createElement('strong');
      // @ts-ignore
      categoryStrongElement.textContent = categoryElement.value;
      categoryParagraphElement.appendChild(categoryStrongElement);

      let creatorParagraphElement = document.createElement('p');
      creatorParagraphElement.textContent = 'Creator:';
      let creatorStrongElement = document.createElement('strong');
      // @ts-ignore
      creatorStrongElement.textContent = creatorElement.value;
      creatorParagraphElement.appendChild(creatorStrongElement);

      let contentParagraph = document.createElement('p');
      // @ts-ignore
      contentParagraph.textContent = contentElement.value;

      let divElement = document.createElement('div');
      divElement.classList.add('buttons');

      let deleteBtn = document.createElement('button');
      deleteBtn.classList.add('btn', 'delete');
      deleteBtn.textContent = 'Delete';
      deleteBtn.addEventListener('click', deleteArticleHandler);

      let archiveBtn = document.createElement('button');
      archiveBtn.classList.add('btn', 'archive');
      archiveBtn.textContent = 'Archive';
      archiveBtn.addEventListener('click', archiveArticleHandler);

      divElement.appendChild(deleteBtn);
      divElement.appendChild(archiveBtn);

      articleElement.appendChild(titleHeadingElement);
      articleElement.appendChild(categoryParagraphElement);
      articleElement.appendChild(creatorParagraphElement);
      articleElement.appendChild(contentParagraph);
      articleElement.appendChild(divElement);

      let postsSection = document.querySelector('.site-content main section');
      postsSection.appendChild(articleElement);
   }

   function deleteArticleHandler(e) {
      let deleteBtn = e.target;
      let article = deleteBtn.parentElement.parentElement;
      article.remove();
   }

   function archiveArticleHandler(e) {
      let articleToArchive = e.target.parentElement.parentElement;
      let olElement = document.querySelector('.archive-section ol');

      let archiveLis = Array.from(olElement.querySelectorAll('li'));
      let articleHeading = articleToArchive.querySelector('h1');
      let articleTitle = articleHeading.textContent;

      let newTitleLi = document.createElement('li');
      newTitleLi.textContent = articleTitle;

      articleToArchive.remove();

      archiveLis.push(newTitleLi);
      archiveLis.sort((a, b) => a.textContent.localeCompare(b.textContent))
         .forEach(li => olElement.appendChild(li));
   }
}