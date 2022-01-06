function solve(input) {

    let matrix = input.map(row => row.split(' ').map(Number));
    let firstDiagonalSum = 0;
    let secondDiagonalSum = 0;

    for (let i = 0; i < matrix.length; i++) {
        firstDiagonalSum += matrix[i][i];
    }

    for (let i = 0; i < matrix.length; i++) {
        secondDiagonalSum += matrix[i][matrix.length - 1 - i];
    }

    if (firstDiagonalSum === secondDiagonalSum) {
        for (let i = 0; i < matrix.length; i++) {
            for (let j = 0; j < matrix.length; j++) {
                if (i !== j && i !== matrix.length - 1 - j) {
                    matrix[i][j] = firstDiagonalSum;
                }
            }
        }

        printMatrix(matrix);
    } else {
        printMatrix(matrix);
    }


    function printMatrix(matrix) {
        for (let i = 0; i < matrix.length; i++) {
            console.log(matrix[i].join(' '));
        }
    }
}