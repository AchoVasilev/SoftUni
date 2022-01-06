function evenPostions(arr) {
    let elements = [];

    for(let i = 0; i < arr.length; i++) {
        if (i % 2 == 0) {
            elements.push(arr[i]);
        }
    }

    return elements.join(' ');
}

evenPostions(['20', '30', '40', '50', '60'])

function lastElements(n, k) {
    let result = [1];

    for (let i = 1; i < n; i++) {
        let lastKElements = result.slice(-k);
        let sum = lastKElements.reduce((a, x) => a + x, 0);

        result.push(sum);
    }

    return result;
}

lastElements(6, 3)

function sumFirstLast(arr) {
    let sum = 0; 

    if (arr.length == 1) {
        sum = parseInt(arr[0]);
    } else {
        sum = parseInt(arr[0]) + parseInt(arr[arr.length - 1]);
    }

    return sum;
}

sumFirstLast(['20', '30', '40'])


function positiveNegative(arr) {
    let result = [];

    for(let i = 0; i < arr.length; i++) {
        if (arr[i] < 0) {
            result.unshift(arr[i]);
        } else {
            result.push(arr[i]);
        }
    }

    for (const element of result) {
        // console.log(element);
    }
}

positiveNegative([7, -2, 8, 9])


function smallestTwoNumbers(arr) {
    let result = arr.sort((a, b) => a - b)
                    .slice(0, 2)
                    .join(' ');

   // console.log(result);
}

smallestTwoNumbers([30, 15, 50, 5])


function biggerHalf(arr) {
    let result = arr.sort((a, b) => a - b)
                    .slice(-Math.ceil(arr.length / 2));

    return result;
}

biggerHalf([4, 7, 2, 5])


function pieceOfPie(arr, firstFlavor, secondFlavor) {
    let indexOfFirstFlavour = arr.indexOf(firstFlavor);
    let indexOfSecondFlavor = arr.indexOf(secondFlavor);

    let result = arr.slice(indexOfFirstFlavour, indexOfSecondFlavor + 1);

    return result;
}

pieceOfPie(['Pumpkin Pie',
'Key Lime Pie',
'Cherry Pie',
'Lemon Meringue Pie',
'Sugar Cream Pie'],
'Key Lime Pie',
'Lemon Meringue Pie'
)


function processOddPostitions(arr) {
    let result = [];

    for(let i = 0; i < arr.length; i++) {
        if (i % 2 != 0) {
            result.push(arr[i] * 2);
        }
    }

    result.reverse();

    return result;
}

processOddPostitions([10, 15, 20, 25])


function biggestElement(matrix) {
    let numbers = matrix.map(row => Math.max(...row))
                        .reduce((a, x) => Math.max(a, x), Number.MIN_SAFE_INTEGER);

    return numbers;
}

biggestElement(
    [[20, 50, 10],
    [8, 33, 145]]
   )


   function solve(matrix){
    let primarySum = 0;
    let secondarySum = 0;

    let elementIndex = 0;

    matrix.forEach((row, i) =>{
        primarySum += row[elementIndex];
        secondarySum += row[row.length - 1 - elementIndex];

        elementIndex++;
    });

    console.log(primarySum + ' ' + secondarySum);
}

solve([[20, 40],
    [10, 60]]
   )


   function solve(matrix) {
    let pairs = 0;

    matrix.forEach((row, i) => {
        row.forEach((el, j) => {
            if (el === row[j + 1]) {
                pairs++;
            }

            if (matrix[i + 1] && el === matrix[i + 1][j]) {
                pairs++;
            }
        });
    });

    console.log(pairs);
}

solve([['2', '3', '4', '7', '0'],
['4', '0', '5', '3', '4'],
['2', '3', '5', '4', '2'],
['9', '8', '7', '5', '4']]
)