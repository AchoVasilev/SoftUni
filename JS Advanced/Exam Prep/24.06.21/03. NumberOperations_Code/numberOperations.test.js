const {expect} = require('chai');
const numberOperations = require('./numberOperations');

describe('number operations tests', () => {
    describe('pow numbers', () => {
        it('should return the power of the number', () => {
            expect(numberOperations.powNumber(1.5)).to.equal(1.5 * 1.5);
            expect(numberOperations.powNumber(-4)).to.equal(16);
            expect(numberOperations.powNumber(0)).to.equal(0);
            expect(numberOperations.powNumber(NaN)).to.be.NaN;
        });
    });

    describe('number checker tests', () => {
        it('should throw error if input is nan', () => {
            expect(() => numberOperations.numberChecker('a')).to.throw('The input is not a number!');
            expect(() => numberOperations.numberChecker()).to.throw('The input is not a number!');
            expect(() => numberOperations.numberChecker(undefined)).to.throw('The input is not a number!');
            expect(() => numberOperations.numberChecker(NaN)).to.throw('The input is not a number!');
        });

        it('should return correct message if input is below 100', () => {
            expect(numberOperations.numberChecker(99)).to.equal('The number is lower than 100!');
            expect(numberOperations.numberChecker(0)).to.equal('The number is lower than 100!');
            expect(numberOperations.numberChecker(-5)).to.equal('The number is lower than 100!');
            expect(numberOperations.numberChecker(99.9)).to.equal('The number is lower than 100!');
            expect(numberOperations.numberChecker('99.9')).to.equal('The number is lower than 100!');
            expect(numberOperations.numberChecker(true)).to.equal('The number is lower than 100!');
        });

        it('should return correct message if input is above 100', () => {
            expect(numberOperations.numberChecker(100)).to.equal('The number is greater or equal to 100!');
            expect(numberOperations.numberChecker(101)).to.equal('The number is greater or equal to 100!');
            expect(numberOperations.numberChecker(100.5)).to.equal('The number is greater or equal to 100!');
            expect(numberOperations.numberChecker('100.5')).to.equal('The number is greater or equal to 100!');
        });
    });

    describe('sumArrays tests', () => {
        it('should return empty arrays if called with empty arrays', () => {
            expect(numberOperations.sumArrays([], [])).to.deep.equal([]);
        });

        it('should return empty arrays if called with one empty array', () => {
            expect(numberOperations.sumArrays([], [1, 2])).to.deep.equal([1, 2]);
            expect(numberOperations.sumArrays([1, 2], [])).to.deep.equal([1, 2]);
        });

        it('should return correct result when both parameters are non empty equal arrays', () => {
            expect(numberOperations.sumArrays(['a', 'b'], ['c', 'd'])).to.deep.equal(['ac', 'bd']);
            expect(numberOperations.sumArrays([1, 2], [1, 2])).to.deep.equal([2, 4]);
        });

        it('should return correct result when both parameters are non empty arrays', () => {
            expect(numberOperations.sumArrays([1, 2, 3], [1, 2])).to.deep.equal([2, 4, 3]);
            expect(numberOperations.sumArrays([1, 2], [1, 2, 3])).to.deep.equal([2, 4, 3]);
            expect(numberOperations.sumArrays([1, 2], [1, 2, 3.3])).to.deep.equal([2, 4, 3.3]);

            expect(numberOperations.sumArrays(['a', 'b', 'c'], ['c', 'd'])).to.deep.equal(['ac', 'bd', 'c']);
            expect(numberOperations.sumArrays(['a', 'b'], ['c', 'd', 'e'])).to.deep.equal(['ac', 'bd', 'e']);
        });
    });
})