const { expect } = require('chai');
const cinema = require('./cinema');

describe('cinema tests', () => {
    describe('show movies tests', () => {
        it('should return message if no movies are in input', () => {
            expect(cinema.showMovies([])).to.equal('There are currently no movies to show.');
        });

        it('should return correct movies', () => {
            let movies = ['King Kong', 'The Tomorrow War', 'Joker'];
            let result = movies.join(', ');
            expect(cinema.showMovies(movies)).to.equal(result);
            expect(cinema.showMovies(movies)).to.be.string;
        });

        it('single movie array', () => {
            expect(cinema.showMovies(['King Kong'])).to.equal('King Kong');
        });
    });

    describe('ticketPrice', () => {
        it('should return correct price if projection type is correct', () => {
            expect(cinema.ticketPrice('Premiere')).to.equal(12.00);
        });

        it('should return correct price if projection type is correct', () => {
            expect(cinema.ticketPrice('Normal')).to.equal(7.50);
        });

        it('should return correct price if projection type is correct', () => {
            expect(cinema.ticketPrice('Discount')).to.equal(5.50);
        });

        it('should throw error if input is invalid', () => {
            expect(() => cinema.ticketPrice('gosho')).to.throw();
        });

        it('should throw error if input is invalid', () => {
            expect(() => cinema.ticketPrice(1)).to.throw();
        });

        it('should throw error if input is invalid', () => {
            expect(() => cinema.ticketPrice(null)).to.throw();
        });

        it('should throw error if input is invalid', () => {
            expect(() => cinema.ticketPrice(undefined)).to.throw();
        });
    });

    describe('swapSeatsInHall tests', () => {
        it('should return success message if correct', () => {
            expect(cinema.swapSeatsInHall(5, 10)).to.equal('Successful change of seats in the hall.');
        });

        it('should throw error if inputs are invalid', () => {
            expect(cinema.swapSeatsInHall(-1, 5)).to.equal('Unsuccessful change of seats in the hall.');
            expect(cinema.swapSeatsInHall(1)).to.equal('Unsuccessful change of seats in the hall.');
            expect(cinema.swapSeatsInHall(1.25, 5)).to.equal('Unsuccessful change of seats in the hall.');
            expect(cinema.swapSeatsInHall('a', 5)).to.equal('Unsuccessful change of seats in the hall.');
            expect(cinema.swapSeatsInHall(undefined, 5)).to.equal('Unsuccessful change of seats in the hall.');
            expect(cinema.swapSeatsInHall(null, 5)).to.equal('Unsuccessful change of seats in the hall.');
            expect(cinema.swapSeatsInHall(21, 5)).to.equal('Unsuccessful change of seats in the hall.');
            expect(cinema.swapSeatsInHall(0, 5)).to.equal('Unsuccessful change of seats in the hall.');

            expect(cinema.swapSeatsInHall(5, -5)).to.equal('Unsuccessful change of seats in the hall.');
            expect(cinema.swapSeatsInHall(5, 1.25)).to.equal('Unsuccessful change of seats in the hall.');
            expect(cinema.swapSeatsInHall(5, 'a')).to.equal('Unsuccessful change of seats in the hall.');
            expect(cinema.swapSeatsInHall(5, undefined)).to.equal('Unsuccessful change of seats in the hall.');
            expect(cinema.swapSeatsInHall(5, null)).to.equal('Unsuccessful change of seats in the hall.');
            expect(cinema.swapSeatsInHall(5, 21)).to.equal('Unsuccessful change of seats in the hall.');
            expect(cinema.swapSeatsInHall(5, 0)).to.equal('Unsuccessful change of seats in the hall.');
            expect(cinema.swapSeatsInHall(5, 5)).to.equal('Unsuccessful change of seats in the hall.');
        })
    })
})