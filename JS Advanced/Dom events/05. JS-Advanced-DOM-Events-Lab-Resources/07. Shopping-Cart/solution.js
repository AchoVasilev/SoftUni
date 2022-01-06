// function solve() {
//    document.getElementsByClassName('shopping-cart')[0].addEventListener('click', onClick);
//    document.getElementsByClassName('checkout')[0].addEventListener('click', checkout);

//    let cart = [];
//    let output = document.getElementsByTagName('textarea')[0];
//    output.value = '';

//    function onClick(ev) {
//       if (ev.target.tagName == 'BUTTON' && ev.target.classList.contains('add-product')) {
//          let product = ev.target.parentNode.parentNode;

//          let name = product.querySelector('.product-title').textContent;
//          let price = Number(product.querySelector('.product-line-price').textContent);

//          cart.push({
//             name,
//             price
//          });

//          output.vale += `Added ${name} for ${price.toFixed(2)} to the cart.\n`;
//       }
//    }

//    function checkout(ev) {
//       let products = new Set();

//       cart.forEach(p => products.add(p.name));

//       let total = cart.reduce((acc, curr) => acc + curr.price, 0);

//       output.value += `You bought ${[...products.keys()].join(', ')} for ${total.toFixed(2)}.`
//    }
// }

function solve() {
   const html = {
      products: document.getElementsByClassName("product"),
      output: document.querySelector("body > div > textarea"),
      checkout: document.getElementsByClassName("checkout")[0],
      buttons: document.getElementsByTagName("button"),
   }

   const state = {
      names: new Set(),
      price: 0,
   }

   function getSectionInfo(e) {
      return {
         name: e.getElementsByClassName("product-title")[0].innerHTML,
         button: e.getElementsByClassName("add-product")[0],
         price:
            Number(
               e.getElementsByClassName("product-line-price")[0].innerHTML
            ) || 0,
      }
   }

   function addToCart({ name, price }) {
      state.names.add(name)
      state.price += price

      return { name, price }
   }

   function displayToCart({ name, price }) {
      html.output.innerHTML += `Added ${name} for ${price.toFixed(
         2
      )} to the cart.\n`
   }

   Array.from(html.products)
      .map(getSectionInfo)
      .forEach(x =>
         x.button.addEventListener("click", () =>
            displayToCart(addToCart(x))
         )
      )

   html.checkout.addEventListener("click", () => {
      html.output.innerHTML += `You bought ${[...state.names.keys()].join(
         ", "
      )} for ${state.price.toFixed(2)}.`
      Array.from(html.buttons).map(x => (x.disabled = "true"))
   })
}