(function () {
    // @ts-ignore
    String.prototype.ensureStart = function (str) {
        return this.startsWith(str) ? this.toString() : `${str}${this}`;
    };

    // @ts-ignore
    String.prototype.ensureEnd = function (str) {
        return this.endsWith(str) ? this.toString() : `${this}${str}`;
    };

    // @ts-ignore
    String.prototype.isEmpty = function () {
        return this.toString() === '';
    };

    // @ts-ignore
    String.prototype.truncate = function (n) {
        if (this.length <= n) {
            return this.toString();
        }

        if (this.includes(' ')) {
            let words = this.split(' ');

            while (words.join(' ').length + 3 > n) {
                words.pop();
            }

            let sentance = `${words.join(' ')}...`;

            return sentance;
        }

        if (n > 3) {
            let string = `${this.slice(0, n - 3)}...`;

            return string;
        }

        return '.'.repeat(n);
    };

    // @ts-ignore
    String.format = function (str, ...params) {
        let replaceRegex = /{(\d+)}/g;

        let replacedString = str.replace(replaceRegex, (match, group1) => {
            let index = Number(group1);

            if (params[index] !== undefined) {
                return params[index];
            }

            return match;
        });

        return replacedString;
    }
}())

let str = 'my string';
// @ts-ignore
str = str.ensureStart('my');
console.log(str);
