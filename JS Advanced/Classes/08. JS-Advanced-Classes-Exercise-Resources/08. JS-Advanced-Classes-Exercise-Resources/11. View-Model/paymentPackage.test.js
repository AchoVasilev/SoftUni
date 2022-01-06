const { expect } = require('chai');
const PaymentPackage = require('./paymentPackage');

// describe('test class PaymentPackage functionality', () => {
//     describe('constructor tests', () => {
//         it('should throw error if name is empty', () => {
//             expect(() => new PaymentPackage('', 5)).to.throw(Error);
//         });

//         it('should throw error if name is undefined', () => {
//             expect(() => new PaymentPackage(undefined, 5)).to.throw(Error);
//         });

//         it('should throw error if name is a number', () => {
//             expect(() => new PaymentPackage(5, 5)).to.throw(Error);
//         });

//         it('should throw error if value is a string', () => {
//             expect(() => new PaymentPackage('HR', '5')).to.throw(Error);
//         });

//         it('should throw error if value is undefined', () => {
//             expect(() => new PaymentPackage('HR', undefined)).to.throw(Error);
//         });

//         it('should throw error if value is less than 0', () => {
//             expect(() => new PaymentPackage('HR', -5)).to.throw(Error);
//         });

//         it('should not throw error if value is 0', () => {
//             expect(() => new PaymentPackage('HR', 0)).not.to.throw(Error);
//         });

//         it('should throw error if both parameters are wrong', () => {
//             expect(() => new PaymentPackage(undefined, undefined)).to.throw(Error);
//         });

//         it('should instanciate a class with correct parameters', () => {
//             let paymentPackage = new PaymentPackage('HR', 5);

//             expect(paymentPackage.name).to.equal('HR');
//             expect(paymentPackage.value).to.equal(5);
//             expect(paymentPackage.VAT).to.equal(20);
//             expect(paymentPackage.active).to.be.true;
//         });
//     });

//     describe('changing values of VAT', () => {
//         let paymentPackage = new PaymentPackage('HR', 5);

//         it('should throw error if VAT is a string', () => {
//             expect(() => paymentPackage.VAT = '5').to.throw(Error);
//         });

//         it('should throw error if VAT is undefined', () => {
//             expect(() => paymentPackage.VAT = undefined).to.throw(Error);
//         });

//         it('should throw error if VAT is less than 0', () => {
//             expect(() => paymentPackage.VAT = -5).to.throw(Error);
//         });

//         it('should not throw error if VAT is 0', () => {
//             expect(() => paymentPackage.VAT = 0).not.to.throw(Error);
//         });

//         it('should set VAT correctly', () => {
//             paymentPackage.VAT = 10;
//             expect(paymentPackage.VAT).to.equal(10);
//         });
//     });

//     describe('changing values of active', () => {
//         let paymentPackage = new PaymentPackage('HR', 5);
//         it('should throw error if active is a string', () => {
//             expect(() => paymentPackage.active = 'true').to.throw(Error);
//         });

//         it('should throw error if active is null', () => {
//             expect(() => paymentPackage.active = null).to.throw(Error);
//         });

//         it('should throw error if active is undefined', () => {
//             expect(() => paymentPackage.active = undefined).to.throw(Error);
//         });

//         it('should throw error if active is a number', () => {
//             expect(() => paymentPackage.active = -5).to.throw(Error);
//         });

//         it('should set active correctly', () => {
//             paymentPackage.active = false;
//             expect(paymentPackage.active).to.be.false;
//         });
//     });

//     describe('toString tests', () => {
//         it('should return correct output', () => {
//             let package = new PaymentPackage('HR Services', 1500);
//             let testString = `Package: HR Services
// - Value (excl. VAT): 1500
// - Value (VAT 20%): 1800`;

//             expect(package.toString()).to.equal(testString)
//         });

//         it('should return correct output', () => {
//             let package = new PaymentPackage('HR Services', 1500);
//             package.active = false;
//             let testString = `Package: HR Services (inactive)
// - Value (excl. VAT): 1500
// - Value (VAT 20%): 1800`;

//             expect(package.toString()).to.equal(testString)
//         });
//     });
// });

describe('test class PaymentPackage functionality', () => {
    describe('constructor tests', () => {
        it('should throw error if name is empty', () => {
            expect(() => new PaymentPackage('', 5)).to.throw(Error);
        });

        it('should throw error if name is undefined', () => {
            expect(() => new PaymentPackage(undefined, 5)).to.throw(Error);
        });

        it('should throw error if name is a number', () => {
            expect(() => new PaymentPackage(5, 5)).to.throw(Error);
        });

        it('should throw error if value is a string', () => {
            expect(() => new PaymentPackage('HR', '5')).to.throw(Error);
        });

        it('should throw error if value is undefined', () => {
            expect(() => new PaymentPackage('HR', undefined)).to.throw(Error);
        });

        it('should throw error if value is less than 0', () => {
            expect(() => new PaymentPackage('HR', -5)).to.throw(Error);
        });

        it('should not throw error if value is 0', () => {
            expect(() => new PaymentPackage('HR', 0)).not.to.throw(Error);
        });

        it('should throw error if both parameters are wrong', () => {
            expect(() => new PaymentPackage(undefined, undefined)).to.throw(Error);
        });

        it('should instanciate a class with correct parameters', () => {
            let paymentPackage = new PaymentPackage('HR', 5);

            expect(paymentPackage.name).to.equal('HR');
            expect(paymentPackage.value).to.equal(5);
            expect(paymentPackage.VAT).to.equal(20);
            expect(paymentPackage.active).to.be.true;
        });
    });

    describe('changing values of VAT', () => {
        let paymentPackage = new PaymentPackage('HR', 5);

        it('should throw error if VAT is a string', () => {
            expect(() => paymentPackage.VAT = '5').to.throw(Error);
        });

        it('should throw error if VAT is undefined', () => {
            expect(() => paymentPackage.VAT = undefined).to.throw(Error);
        });

        it('should throw error if VAT is less than 0', () => {
            expect(() => paymentPackage.VAT = -5).to.throw(Error);
        });

        it('should not throw error if VAT is 0', () => {
            expect(() => paymentPackage.VAT = 0).not.to.throw(Error);
        });

        it('should set VAT correctly', () => {
            paymentPackage.VAT = 10;
            expect(paymentPackage.VAT).to.equal(10);
        });
    });

    describe('changing values of active', () => {
        let paymentPackage = new PaymentPackage('HR', 5);
        it('should throw error if active is a string', () => {
            expect(() => paymentPackage.active = 'true').to.throw(Error);
        });

        it('should throw error if active is null', () => {
            expect(() => paymentPackage.active = null).to.throw(Error);
        });

        it('should throw error if active is undefined', () => {
            expect(() => paymentPackage.active = undefined).to.throw(Error);
        });

        it('should throw error if active is a number', () => {
            expect(() => paymentPackage.active = -5).to.throw(Error);
        });

        it('should set active correctly', () => {
            paymentPackage.active = false;
            expect(paymentPackage.active).to.be.false;
        });
    });

    describe('toString tests', () => {
        it('should return correct output', () => {
            let package = new PaymentPackage('HR Services', 1500);
            let testString = `Package: HR Services
- Value (excl. VAT): 1500
- Value (VAT 20%): 1800`;

            expect(package.toString()).to.equal(testString)
        });

        it('should return correct output', () => {
            let package = new PaymentPackage('HR Services', 1500);
            package.active = false;
            let testString = `Package: HR Services (inactive)
- Value (excl. VAT): 1500
- Value (VAT 20%): 1800`;

            expect(package.toString()).to.equal(testString)
        });
    });
});