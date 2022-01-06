let { expect } = require('chai');
let library = require('./library');

describe('library tests', () => {
    describe('calcPrice tests', () => {
        it('should throw error if invalid input', () => {
            expect(() => library.calcPriceOfBook(1, 1995)).to.throw('Invalid input');
            expect(() => library.calcPriceOfBook(1, 1992.5)).to.throw('Invalid input');
            expect(() => library.calcPriceOfBook(1)).to.throw('Invalid input');
            expect(() => library.calcPriceOfBook('gosho', '1992')).to.throw('Invalid input');
            expect(() => library.calcPriceOfBook('gosho', 'gosho')).to.throw('Invalid input');
            expect(() => library.calcPriceOfBook(undefined, 1995)).to.throw('Invalid input');
            expect(() => library.calcPriceOfBook(null, 1995)).to.throw('Invalid input');
            expect(() => library.calcPriceOfBook('gosho', null)).to.throw('Invalid input');
            expect(() => library.calcPriceOfBook('gosho', undefined)).to.throw('Invalid input');
            expect(() => library.calcPriceOfBook('gosho')).to.throw('Invalid input');
        });

        it('should return correct result if book year is 1980 or earlier', () => {
            let price = 20 - (20 * 0.5);
            expect(library.calcPriceOfBook('gosho', 1980)).to.equal(`Price of gosho is ${price.toFixed(2)}`);
            expect(library.calcPriceOfBook('gosho', 1975)).to.equal(`Price of gosho is ${price.toFixed(2)}`);
            expect(library.calcPriceOfBook('gosho', 0)).to.equal(`Price of gosho is ${price.toFixed(2)}`);
        });

        it('should return correct result if book is later than 1980', () => {
            let price = 20;
            expect(library.calcPriceOfBook('gosho', 1981)).to.equal(`Price of gosho is ${price.toFixed(2)}`);
            expect(library.calcPriceOfBook('pesho', 1995)).to.equal(`Price of pesho is ${price.toFixed(2)}`);
        });
    });

    describe('find book tests', () => {
        it('should throw error if array is empty', () => {
            expect(() => library.findBook([], 'gosho')).to.throw('No books currently available');
        });

        it('should not return book if it is not found', () => {
            let books = ["Troy", "Life Style", "Torronto", 'Roma'];

            expect(library.findBook(books, 'Roma Invicta')).to.equal('The book you are looking for is not here!');
            expect(library.findBook(books, 'Life')).to.equal('The book you are looking for is not here!');
        });

        it('should return correct book', () => {
            let books = ["Troy", "Life Style", "Torronto", 'Roma'];

            expect(library.findBook(books, 'Troy')).to.equal('We found the book you want.');
            expect(library.findBook(books, 'Roma')).to.equal('We found the book you want.');
        });
    });

    describe('arrangeTheBooks tests', () => {
        it('should throw error if input is not an integer or is negative', () => {
            expect(() => library.arrangeTheBooks(-5)).to.throw('Invalid input');
            expect(() => library.arrangeTheBooks('5')).to.throw('Invalid input');
            expect(() => library.arrangeTheBooks(0.5)).to.throw('Invalid input');
            expect(() => library.arrangeTheBooks(null)).to.throw('Invalid input');
            expect(() => library.arrangeTheBooks(undefined)).to.throw('Invalid input');
        });

        it('should return success if space is enough', () => {
            expect(library.arrangeTheBooks(0)).to.equal('Great job, the books are arranged.');
            expect(library.arrangeTheBooks(5)).to.equal('Great job, the books are arranged.');
            expect(library.arrangeTheBooks(39)).to.equal('Great job, the books are arranged.');
            expect(library.arrangeTheBooks(40)).to.equal('Great job, the books are arranged.');
        });

        it('should return message if space is not enough', () => {
            expect(library.arrangeTheBooks(41)).to.equal('Insufficient space, more shelves need to be purchased.');
            expect(library.arrangeTheBooks(50)).to.equal('Insufficient space, more shelves need to be purchased.');
            expect(library.arrangeTheBooks(100)).to.equal('Insufficient space, more shelves need to be purchased.');
        });
    })
})