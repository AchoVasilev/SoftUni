function solve(inputArr) {
    const catalogue = {};

    inputArr.forEach((element) => {
        let [productName, price] = element.split(' : ');
        price = Number(price);

        const index = productName[0];

        if (!catalogue[index]) {
            catalogue[index] = {};
        }

        catalogue[index][productName] = price;
    });

    let initialSort = Object.keys(catalogue).sort((a, b) => a.localeCompare(b));

    for (const key of initialSort) {
        let products = Object.entries(catalogue[key])
                             .sort((a, b) => a[0].localeCompare(b[0]));

        console.log(key);

        products.forEach((el) => {
            console.log(`   ${el[0]}: ${el[1]}`);
        });
    }
}