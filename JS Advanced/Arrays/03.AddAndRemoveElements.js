function solve(input) {
    let arr = [];

    for(let i = 0; i < input.length; i++) {
        if (input[i] == 'add') {
            arr.push(i + 1);
        } else {
            arr.pop();
        }
    }

    if (arr.length == 0) {
        console.log('Empty');
    } else {
        for (const num of arr) {
            console.log(num);
        }
    }
}

solve(['remove', 
'remove', 
'remove']
)