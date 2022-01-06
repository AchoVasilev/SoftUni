const { expect } = require('chai');
const sum = require('./sum');

describe('Testing sum function', () => {
    it('Returns NaN if one of the arguments of the array is not a number', () => {
        expect(sum([1, 'pesho', 2])).to.be.NaN
    });

    it('Returns correct sum if array is given two numbers', () => {
        expect(sum([5, 3, 6, 10])).to.equal(24);
    });

    it('calculates 1 element array', () => {
        expect(sum([1])).to.equal(1)
    })
})