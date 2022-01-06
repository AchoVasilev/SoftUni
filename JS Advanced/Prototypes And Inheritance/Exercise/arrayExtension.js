(function () {
    // @ts-ignore
    Array.prototype.last = function () {
        return this[this.length - 1];
    };

    // @ts-ignore
    Array.prototype.skip = function (n) {
        return this.slice(n);
    };

    // @ts-ignore
    Array.prototype.take = function (n) {
        return this.slice(0, n);
    };

    // @ts-ignore
    Array.prototype.sum = function () {
        return this.reduce((acc, curr) => acc + curr, 0);
    };

    // @ts-ignore
    Array.prototype.average = function () {
        // @ts-ignore
        return this.sum() / this.length;
    }
}())

let arr = [1, 2, 3];
// @ts-ignore
console.log(arr.last());