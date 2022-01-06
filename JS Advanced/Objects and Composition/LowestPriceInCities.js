function solve(inputArr) {
    let products = {};

    for(let i = 0; i < inputArr.length; i ++) {
        let [townName, productName, productPrice] = inputArr[i].split(' | ');

        productPrice = Number(productPrice);

        if (!products.hasOwnProperty(productName)) {
            products[productName] = {};
        }

        products[productName][townName] = productPrice;
    }

    let result = [];

    for (const key in products) {
        let sortedTowns = Object.entries(products[key]).sort((a, b) => a[1] - b[1]);
        let cheapestTown = sortedTowns[0];

        result.push(`${key} -> ${cheapestTown[1]} (${cheapestTown[0]})`);
    }

    return result.join('\n');
}