class Restaurant {
    constructor(budget) {
        this.budgetMoney = Number(budget);
        this.menu = {};
        this.stockProducts = {};
        this.history = [];
    }

    loadProducts(arr) {
        for (let i = 0; i < arr.length; i++) {
            let [productName, productQuantity, productTotalPrice] = arr[i].split(' ');

            productQuantity = Number(productQuantity);
            productTotalPrice = Number(productTotalPrice);

            if (this.budgetMoney < productTotalPrice) {
                this.history.push(`There was not enough money to load ${productQuantity} ${productName}`);
            } else {
                if (!this.stockProducts[productName]) {
                    this.stockProducts[productName] = 0;
                }

                this.stockProducts[productName] += productQuantity;

                this.budgetMoney -= productTotalPrice;
                this.history.push(`Successfully loaded ${productQuantity} ${productName}`);
            }
        }

        return this.history.join('\n');
    }

    addToMenu(meal, productsArr, price) {
        price = Number(price);

        if (!this.menu[meal]) {
            this.menu[meal] = {
                products: {},
                price: price
            };

            productsArr.forEach(p => {
                let [productName, productQuantity] = p.split(' ');
                productQuantity = Number(productQuantity);
                this.menu[meal].products[productName] = productQuantity;
            });

            let mealsCount = Object.keys(this.menu).length;

            if (mealsCount == 1) {
                return `Great idea! Now with the ${meal} we have 1 meal in the menu, other ideas?`
            } else {
                return `Great idea! Now with the ${meal} we have ${mealsCount} meals in the menu, other ideas?`;
            }
        } else {
            return `The ${meal} is already in the our menu, try something different.`;
        }
    }

    showTheMenu() {
        if (Object.keys(this.menu).length == 0) {
            return 'Our menu is not ready yet, please come later...';
        }

        let stringArr = [];
        for (const meal in this.menu) {
            stringArr.push(`${meal} $ ${this.menu[meal].price}`)
        }

        return stringArr.join('\n');
    }

    makeTheOrder(meal) {
        if (!this.menu[meal]) {
            return `There is not ${meal} yet in our menu, do you want to order something else?`;
        }

        let neededProducts = {};
        for(const product in this.menu[meal].products){
            if (!this.stockProducts[product] || this.stockProducts[product] < this.menu[meal].products[product]) {
                return `For the time being, we cannot complete your order (${meal}), we are very sorry...`;
            } else {
                neededProducts[product] = this.menu[meal].products[product];
            }
        }

        for(const neededProduct in neededProducts) {
            this.stockProducts[neededProduct] -= neededProducts[neededProduct];
        }

        this.budgetMoney += this.menu[meal].price;
        return `Your order (${meal}) will be completed in the next 30 minutes and will cost you ${this.menu[meal].price}.`
    }
}

let kitchen = new Restaurant(1000);
kitchen.loadProducts(['Yogurt 1 1', 'Honey 1 1', 'Strawberries 10 1', 'Banana 1 1']);
kitchen.addToMenu('frozenYogurt', ['Yogurt 1', 'Honey 1', 'Banana 1', 'Strawberries 10'], 9.99);
console.log(kitchen.makeTheOrder('frozenYogurt'));
console.log(kitchen.budgetMoney)
