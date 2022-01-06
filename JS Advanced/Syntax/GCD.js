function solve(firstNum = 0, secondNum = 0) {
    let lowestNum = Math.min(firstNum, secondNum);
    let largestNum = Math.max(firstNum, secondNum);
    let output = 0;

    for(let i = lowestNum; i >= 0; i--) {
        if (largestNum % i == 0 && lowestNum % i == 0) {
            output = i;
            break;
        }
    }

    console.log(output);
}

solve(2154, 458)