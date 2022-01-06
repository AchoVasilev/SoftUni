function solve(num) {
    let result = 0;

    function inner(x) {
        result += x;

        return inner;
    }

    inner.toString = () => result;

    return inner(num);
}