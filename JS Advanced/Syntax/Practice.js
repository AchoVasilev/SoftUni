// function solve(input){
//     console.log(input.length);
//     console.log(input);
// }

// solve('Hello, JavaScript!')

// String Length

// function stringLength(firstText, secondText, thirdText){
//     let sum = firstText.length + secondText.length + thirdText.length;
//     let average = Math.floor(sum / 3);

//     console.log(sum);
//     console.log(average);
// }

// stringLength('chocolate', 'ice cream', 'cake');

// Largest Number

// function solve(firstNum, secondNum, thirdNum){
//     let result = Math.max(firstNum, secondNum, thirdNum);

//     console.log(`The largest number is ${result}.`);
// }

// solve(5, -3, 16)

//Circle Area

// function solve(input){
//     let inputType = typeof(input);

//     if(inputType === 'number'){
//         let area = Math.pow(input, 2) * Math.PI;
//         console.log(area.toFixed(2));
//     } else {
//         console.log(`We can not calculate the circle area, because we receive a ${inputType}.`);
//     }
// }

// solve(5);
// solve('5')

//Math Operations

// function solve(firstNum, secondNum, operand) {
//     let result;

//     switch(operand) {
//         case '+':
//             result = firstNum + secondNum;
//         break;
//         case '-':
//             result = firstNum - secondNum;
//         break;
//         case '*':
//             result = firstNum * secondNum;
//         break;
//         case '%':
//             result = firstNum % secondNum;
//         break;
//         case '/':
//             result = firstNum / secondNum;
//         break;
//         case '**':
//             result = firstNum ** secondNum;
//         break;
//     }

//     console.log(result);
// }

// solve(5, 6, '+')
// solve(3, 5.5, '*')

//Sum Of Numbers

// function solve(n, m) {
//     let firstNum = Number(n);
//     let secondNum = Number(m);
//     let sum = 0;

//     for(let i = firstNum; i <= secondNum; i++) {
//         sum += i;
//     }

//     console.log(sum);
// }

// solve('1', '5');

// Day Of Week

// function solve(day) {
//     switch(day){
//         case 'Monday':
//             console.log(1);
//         break;
//         case 'Tuesday':
//             console.log(2);
//         break;
//         case 'Wednesday':
//             console.log(3);
//         break;
//         case 'Thursday':
//             console.log(4);
//         break;
//         case 'Friday':
//             console.log(5);
//         break;
//         case 'Saturday':
//             console.log(6);
//         break;
//         case 'Sunday':
//             console.log(7);
//         break;
//         default:
//             console.log('error');
//         break;
//     }
// }

// solve('Monday')

//Days In A Month

// function solve(month, year) {
//     let days = new Date(year, month, 0).getDate();

//     console.log(days);
// }

// solve(1, 2012)


//Square Of Stars

// function solve(size) {
//     let result = '';
//     let type = typeof(size);

//     if (size === 1) {
//         console.log('*');
//     } else if (type === 'undefined') {
//         for (let i = 0; i < 5; i++) {
//             for (let j = 0; j < 5; j++) {
//                 result += '*' + ' ';
//             }
//             result += "\n";
//         }
//     } else {
//         for (let i = 0; i < size; i++) {
//             for (let j = 0; j < size; j++) {
//                 result += '*' + ' ';
//             }
//             result += "\n";
//         }
//     }

//     console.log(result);
// }

// solve(7)


// Aggregate Elements

function solve(input) {
    let sum = 0;
    let inverseSum = 0;
    let concatString = '';

    for (let i = 0; i < input.length; i ++) {
        sum += input[i];
        inverseSum += 1 / input[i];
        concatString += input[i];
    }

    console.log(sum);
    console.log(inverseSum);
    console.log(concatString);
}

solve([1, 2, 3])