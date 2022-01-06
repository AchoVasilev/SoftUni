function solve() {
  let textAreaElement = document.getElementById('input');

  let text = textAreaElement.value;

  let sentances = text.split('.')
                      .filter(x => x !== '')
                      .map(x => x + '.');
  
  let paragraphRoof = Math.ceil(sentances.length / 3);

  let resultDiv = document.getElementById('output');

  for(let i = 0; i < paragraphRoof; i++) {
    resultDiv.innerHTML += `<p>${sentances.splice(0, 3).join('')}</p>`;
  }
}