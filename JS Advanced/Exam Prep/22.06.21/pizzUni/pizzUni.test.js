let { expect } = require('chai');
let pizzUni = require('./pizzUni');

describe('pizzUni tests', () => {
    describe('makeAndOrder tests', () => {
        it('should return confirmation msg when ordered only pizza', () => {
            let obj = {};
            obj.orderedPizza = 'Peperoni';

            expect(pizzUni.makeAnOrder(obj)).to.equal(`You just ordered Peperoni`);
        });

        it('should return message if ordered pizza and dring', () => {
            let obj = {};
            obj.orderedPizza = 'Peperoni';
            obj.orderedDrink = 'Water';

            expect(pizzUni.makeAnOrder(obj)).to.equal(`You just ordered Peperoni and Water.`);
        });

        it('should throw error if no pizza is ordered', () => {
            let obj = {};
            obj.orderedPizza = '';

            expect(() => pizzUni.makeAnOrder(obj)).to.throw();
        });

        it('should throw when no order is given', () => {
            expect(() => pizzUni.makeAnOrder()).to.throw();
        });

        it('should throw error if only drink is ordered', () => {
            let obj = {};
            obj.orderedDrink = 'Water';

            expect(() => pizzUni.makeAnOrder(obj)).to.throw();
        });
    });

    describe('getRemainingWork tests', () => {
        it('should return success message if array is empty', () => {
            expect(pizzUni.getRemainingWork([])).to.equal('All orders are complete!');
        });

        it('should return success message if pizzas are ready', () => {
            let arr = [{pizzaName: 'Margarita', status: 'ready'}, {pizzaName: 'Peperoni', status: 'ready'}];
            expect(pizzUni.getRemainingWork(arr)).to.equal('All orders are complete!');
        });

        it('should return success message if pizzas are ready', () => {
            let arr = [{pizzaName: 'Margarita', status: 'ready'}];
            expect(pizzUni.getRemainingWork(arr)).to.equal('All orders are complete!');
        });

        it('should return pizza message if pizzas are not ready', () => {
            let arr = [{pizzaName: 'Margarita', status: 'preparing'}, {pizzaName: 'Peperoni', status: 'preparing'}];
            expect(pizzUni.getRemainingWork(arr)).to.equal(`The following pizzas are still preparing: Margarita, Peperoni.`)
        });

        it('should return pizza message if one pizza is not ready', () => {
            let arr = [{pizzaName: 'Margarita', status: 'preparing'}, {pizzaName: 'Peperoni', status: 'ready'}];
            expect(pizzUni.getRemainingWork(arr)).to.equal(`The following pizzas are still preparing: Margarita.`)
        });

        it('should return pizza message if one pizza is in arr', () => {
            let arr = [{pizzaName: 'Margarita', status: 'preparing'}];
            expect(pizzUni.getRemainingWork(arr)).to.equal(`The following pizzas are still preparing: Margarita.`)
        });
    });

    describe('orderType tests', () => {
        it('should return correct sum if type is Carry Out', () => {
            expect(pizzUni.orderType(100, 'Carry Out')).to.equal(90);
        });

        it('should return correct sum if type is Delivery', () => {
            expect(pizzUni.orderType(100, 'Delivery')).to.equal(100);
        });

        it('should return correct sum if type not written', () => {
            expect(pizzUni.orderType(100)).to.equal(undefined);
        });

        it('should return correct sum if type not written', () => {
            expect(pizzUni.orderType('a', 'Carry Out')).to.be.NaN;
        });
    })
})