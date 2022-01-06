function solve(number) {
    let lastDigit = number % 10;
    let equalCount = 0;
    let sum = 0;

    while (number / 10 > 0) {
        let digit = number % 10;
        sum += digit;

        if (digit !== lastDigit) {
            equalCount++;
        }

        lastDigit = digit;
        number = Math.floor(number / 10);
    }

    console.log(equalCount === 0 ? true : false);
    console.log(sum);
}

solve(2222222)