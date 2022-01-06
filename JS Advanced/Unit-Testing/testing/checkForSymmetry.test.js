const { expect } = require('chai');
const isSymmetric = require('./checkForSymmetry');

describe('Symmetry checker', () => {
    it('returns false if input is not an array', () => {
        expect(isSymmetric(5)).to.equal(false);
    });

    it('returns true if array is symmetric', () => {
        expect(isSymmetric([1, 2, 2, 1])).to.equal(true);
    });

    it('returns false if array is not symmetric', () => {
        expect(isSymmetric([1, 2, 3])).to.equal(false);
    });

    it('returns false if elements of array are different types', () => {
        expect(isSymmetric([1, 2, '3'])).to.equal(false);
    });

    it('returns true if array is symmetric with odd number of elements', () => {
        expect(isSymmetric([1, 2, 1])).to.equal(true);
    });

    it('returns true if array is symmetric with odd number of elements but different types', () => {
        expect(isSymmetric([1, 2, '1'])).to.equal(false);
    });

    it('returns true for symmetric array with strings', () => {
        expect(isSymmetric(['a', 'b', 'b', 'a'])).to.equal(true);
    });

    it('returns false if array is not symmetric array of strings', () => {
        expect(isSymmetric(['a', 'b', 'c'])).to.equal(false);
    });

    it('returns false for only one string parameter', () => {
        expect(isSymmetric('abba')).to.equal(false);
    });
})