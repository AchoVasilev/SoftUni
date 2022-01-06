function addItem() {
   let ulElement = document.getElementById('items');
   let inputTextElement = document.getElementById('newItemText');

   let liElement = document.createElement('li');

   liElement.textContent = inputTextElement.value;
   let button = document.createElement('a');

   button.href = '#';
   button.textContent = '[Delete]';

   button.addEventListener('click', deleteElement);

   liElement.appendChild(button);

   ulElement.appendChild(liElement);

   function deleteElement(ev) {
       let parent = ev.target.parentNode;

       parent.remove();
   }
}