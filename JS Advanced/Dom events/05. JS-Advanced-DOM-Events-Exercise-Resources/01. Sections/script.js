function create(words) {
   let contentDivElement = document.getElementById('content');

   for (let i = 0; i < words.length; i++) {
      let div = document.createElement('div');
      let p = document.createElement('p');

      p.textContent = words[i];
      p.style.display = 'none';

      div.appendChild(p);

      contentDivElement.appendChild(div);
   }

   contentDivElement.addEventListener('click', showParagraph);

   function showParagraph(e) {
      if (e.target.matches('#content div')) {
         let p = e.target.children[0];
         p.style.display = 'block';
      }
   }
}