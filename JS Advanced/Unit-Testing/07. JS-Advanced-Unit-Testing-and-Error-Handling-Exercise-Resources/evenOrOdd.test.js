let { expect } = require('chai');
let isOddOrEven = require('./evenOrOdd');

describe('isOddOrEven function tests', () => {
    it('should return undefined if input is not a string', () => {
        expect(undefined).to.equal(isOddOrEven(1));
    });

    it('should return undefined if input is undefined', () => {
        expect(undefined).to.equal(isOddOrEven(undefined));
    });

    it('should return even if string is of even length', () => {
        expect('even').to.equal(isOddOrEven('even'));
    });

    it('should return odd if string is of odd length', () => {
        expect('odd').to.equal(isOddOrEven('odd'));
    });

    it('should return correct results when passing even or odd string', () => {
        expect('odd').to.equal(isOddOrEven('odd'));
        expect('even').to.equal(isOddOrEven('even'));
    });
})