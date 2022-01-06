const { expect } = require('chai');
const testNumbers = require('./testNumbers');

describe('testNumbers', () => {
    describe('sum number tests', () => {
        it('should return undefined if inputs are not a number', () => {
            expect(testNumbers.sumNumbers('a', 5)).to.equal(undefined);
            expect(testNumbers.sumNumbers(undefined, 5)).to.equal(undefined);
            expect(testNumbers.sumNumbers(5, '5')).to.equal(undefined);
            expect(testNumbers.sumNumbers(5, undefined)).to.equal(undefined);

            expect(testNumbers.sumNumbers(null, 5)).to.equal(undefined);
            expect(testNumbers.sumNumbers(5, null)).to.equal(undefined);
            expect(testNumbers.sumNumbers(5, true)).to.equal(undefined);
            expect(testNumbers.sumNumbers(false, 5)).to.equal(undefined);
        });

        it('should return correct sum', () => {
            expect(testNumbers.sumNumbers(5, 5)).to.equal('10.00');
            expect(testNumbers.sumNumbers(5, -5)).to.equal('0.00');
            expect(testNumbers.sumNumbers(5, 2.50)).to.equal('7.50');
        });
    });

    describe('numberChecker tests', () => {
        it('should throw error if input is NaN', () => {
            expect(() => testNumbers.numberChecker('a')).to.throw();
            expect(() => testNumbers.numberChecker(undefined)).to.throw();
            expect(() => testNumbers.numberChecker(Number.isNaN)).to.throw();
        });

        it('should return message if number is odd', () => {
            expect(testNumbers.numberChecker(5)).to.be.string;
            expect(testNumbers.numberChecker('5')).to.be.string;
        });

        it('should return message if number is even', () => {
            expect(testNumbers.numberChecker(4)).to.be.string;
            expect(testNumbers.numberChecker('4')).to.be.string;
        });
    });

    describe('averageSumArray', () => {
        it('should return correct result', () => {
            expect(testNumbers.averageSumArray([5, 5, 5, 5, 5])).to.equal(5);
        });
    });
})