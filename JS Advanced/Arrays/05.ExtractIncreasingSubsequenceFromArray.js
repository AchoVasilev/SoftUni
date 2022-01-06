function solve(arr) {
    let maxNum = Number.MIN_SAFE_INTEGER;
    let result = [];

    for(let i = 0; i < arr.length; i++) {
        if (arr[i] >= maxNum) {
            maxNum = arr[i];
            result.push(maxNum);
        }
    }

    return result;
}

solve([1, 
    3, 
    8, 
    4, 
    10, 
    12, 
    3, 
    2, 
    24]
    )