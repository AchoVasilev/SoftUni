function solve(arr, startIndex, endIndex) {
    startIndex = arr[startIndex] == undefined ? 0 : startIndex;
    endIndex = arr[endIndex] == undefined ? arr.length - 1 : endIndex;

    try {
        return (arr.slice(startIndex, endIndex + 1)
            .reduce((acc, curr) => Number(acc) + Number(curr), 0));
    } catch (e) {
        return NaN;
    }
}

solve([10, 20, 30, 40, 50, 60], 3, 300)