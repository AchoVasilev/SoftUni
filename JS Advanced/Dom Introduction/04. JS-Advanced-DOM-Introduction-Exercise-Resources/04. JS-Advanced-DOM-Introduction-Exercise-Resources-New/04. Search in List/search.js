function search() {
   let townsElement = document.getElementById('towns').children;
   let searchBoxElement = document.getElementById('searchText').value;
   let resultElement = document.getElementById('result');

   let count = 0;

   Array.from(townsElement).map(x => {
      if (x.innerHTML.includes(searchBoxElement)) {
         x = x.style.textDecoration = 'bold underline';
         count++;
      }

      return x;
   })

   resultElement.innerHTML = `${count} matches found`;
}
