class Stringer {
    constructor(inputString, length) {
        this.innerString = inputString;
        this.innerLength = length;
    }

    increase(length) {
        this.innerLength += length;
    }

    decrease(length) {
        let result = this.innerLength - length;
        this.innerLength = result < 0 ? 0 : result;
    }

    toString() {
        if (this.innerLength === 0) {
            return '...';
        } else if (this.innerString.length > this.innerLength) {
            return `${this.innerString.slice(0, this.innerLength)}...`;
        }

        return this.innerString;
    }
}

let test = new Stringer("Test", 5);
console.log(test.toString()); // Test

test.decrease(3);
console.log(test.toString()); // Te...

test.decrease(5);
console.log(test.toString()); // ...

test.increase(4); 
console.log(test.toString()); // Test
