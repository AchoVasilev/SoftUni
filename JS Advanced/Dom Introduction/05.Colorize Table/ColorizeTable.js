function colorize() {
   let rows = document.querySelectorAll('table tr:nth-child(even)');

   for (const row of rows) {
       row.style.background = 'teal';
   }
}