// function solve() {
//   let table = document.querySelector('table.table tbody');

//   let [input, output] = Array.from(document.querySelectorAll('textarea'));
//   let [generateBtn, buyBtn] = Array.from(document.querySelectorAll('button'));

//   generateBtn.addEventListener('click', generate);
//   buyBtn.addEventListener('click', buy);

//   function generate(e) {
//     let data = JSON.parse(input.value);

//     for (const item of data) {
//       let row = document.createElement('tr');

//       row.appendChild(createCell('img', { src: item.img }));
//       row.appendChild(createCell('p', {}, item.name));
//       row.appendChild(createCell('img', {}, item.price));
//       row.appendChild(createCell('img', {}, item.decFactor));
//       row.appendChild(createCell('img', { type: 'checkbox' }));

//       table.appendChild(row);
//     }
//   }

//   function createCell(nestedTag, props, content) {
//     let cell = document.createElement('td');
//     let nested = document.createElement(nestedTag);

//     for (let prop in props) {
//       nested[prop] = props[prop];
//     }

//     if (content) {
//       nested.textContent = content;
//     }

//     cell.appendChild(nested);

//     return cell;
//   }

//   function buy(e) {
//     let furniture = Array.from(document.querySelectorAll('input[type="checkbox"]:checked'))
//       .map(b => b.parentElement.parentElement)
//       .map(b => ({
//         name: b.children[1].textContent,
//         price: Number(b.children[2].textContent),
//         decFactor: Number(b.children[3].textContent)
//       }))
//       .reduce((acc, curr, i, arr) => {
//         acc.names.push(curr.name);
//         acc.total += curr.price;
//         acc.decFactor += curr.decFactor / arr.length;

//         return acc;
//       }, { names: [], total: 0, decFactor: 0 });

//     let result = `Bougth furniture ${furniture.names.join(', ')}
//     Total price: ${furniture.total.toFixed(2)}
//     Average decoration factor: ${furniture.decFactor}`;
//   }
// }

function solve() {
  const [generateBtn, buyBtn] = document.getElementsByTagName("button")
  const [generateInput, buyOutput] = document.getElementsByTagName("textarea")
  const tableBody = document.querySelector("tbody")

  const htmlTemplate = ({ img, name, price, decFactor }) => {
      const row = document.createElement("tr")

      row.innerHTML = `<td><img src=${img}></td>
<td><p>${name}</p></td>
<td><p>${price}</p></td>
<td><p>${decFactor}</p></td>
<td><input type="checkbox"/></td>`

      return row
  }

  const generate = () =>
      JSON.parse(generateInput.value).forEach(x =>
          tableBody.appendChild(htmlTemplate(x))
      )

  const buy = () => {
      const braindeadData = Array.from(
          tableBody.querySelectorAll("input[type=checkbox]:checked")
      ).map(x =>
          Array.from(x.parentNode.parentNode.children)
              .slice(1, 4)
              .map(
                  x =>
                      Number(x.children[0].innerHTML) ||
                      x.children[0].innerHTML
              )
      )
      const getSum = arr => arr.reduce((a, v) => a + v, 0)

      const names = braindeadData.map(x => x[0]).join(", ")
      const prices = getSum(braindeadData.map(x => x[1]))
      const avFactors =
          getSum(braindeadData.map(x => x[2])) / braindeadData.length

      buyOutput.value = `Bought furniture: ${names}
Total price: ${prices.toFixed(2)}
Average decoration factor: ${avFactors}`
  }

  generateBtn.addEventListener("click", generate)
  buyBtn.addEventListener("click", buy)
}