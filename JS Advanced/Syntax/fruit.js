function solve(fruit, weightInGrams, pricePerKg) {
    let weightInKg = weightInGrams / 1000;
    let sum = pricePerKg * weightInKg;

    console.log(`I need $${sum.toFixed(2)} to buy ${weightInKg.toFixed(2)} kilograms ${fruit}.`);
}

solve('apple', 1563, 2.35)