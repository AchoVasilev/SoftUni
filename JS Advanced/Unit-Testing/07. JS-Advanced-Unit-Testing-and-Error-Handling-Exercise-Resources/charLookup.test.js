let { expect } = require('chai');
let lookupChar = require('./charLookup');

describe('charLookup function tests', () => {
    it('should return undefined if input string is not a string', () => {
        expect(undefined).to.equal(lookupChar(1, 1));
        expect(undefined).to.equal(lookupChar(undefined, 1));
    });

    it('should return undefined if input index is not an integer', () => {
        expect(undefined).to.equal(lookupChar('char', 'char'));
        expect(undefined).to.equal(lookupChar('char', undefined));
        expect(undefined).to.equal(lookupChar('char', 3.5));
    });

    it('should return "Incorrect index" if index is not correct', () => {
        expect('Incorrect index').to.equal(lookupChar('char', 5));
        expect('Incorrect index').to.equal(lookupChar('char', 4));
        expect('Incorrect index').to.equal(lookupChar('char', -1));
    });

    it('it should return correct index', () => {
        expect('r').to.equal(lookupChar('char', 3))
    })
})