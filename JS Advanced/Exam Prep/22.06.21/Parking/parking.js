class Parking {
    constructor(capacity) {
        this.capacity = Number(capacity);
        this.vehicles = [];
    }

    addCar(carModel, carNumber) {
        if (this.capacity == 0) {
            throw new Error('Not enough parking space.');
        }

        let car = {
            carModel: carModel,
            carNumber: carNumber,
            payed: false
        };

        this.vehicles.push(car);

        return `The ${carModel}, with a registration number ${carNumber}, parked.`;
    }

    removeCar(carNumber) {
        if (this.vehicles.some(c => c.carNumber == carNumber) == false) {
            throw new Error('The car, you\'re looking for, is not found.');
        }

        let vehicle = this.vehicles.find(c => c.carNumber == carNumber);

        if (vehicle.payed == false) {
            throw new Error(`${carNumber} needs to pay before leaving the parking lot.`);
        }

        let index = this.vehicles.indexOf(vehicle);

        this.vehicles = this.vehicles.splice(index, 1);

        return `${carNumber} left the parking lot.`;
    }

    pay(carNumber) {
        if (this.vehicles.some(c => c.carNumber == carNumber) == false) {
            throw new Error(`${carNumber} is not in the parking lot.`);
        }

        let vehicle = this.vehicles.find(c => c.carNumber == carNumber);

        if (vehicle.payed) {
            throw new Error(`${carNumber}'s driver has already payed his ticket.`);
        }

        vehicle.payed = true;

        return `${carNumber}'s driver successfully payed for his stay.`;
    }

    getStatistics(carNumber) {
        if (!carNumber) {
            return this._getFullStatistics();
        }

        let car = this.vehicles.find(x => x.carNumber == carNumber);

        return `${car.carModel} == ${car.carNumber} - ${car.payed ? 'Has payed' : 'Not payed'}`;
    }

    _getFullStatistics(){
        let resultStrArr = [];

        let remainingParkingSlots = this.capacity - this.vehicles.length;

        resultStrArr.push(`The Parking Lot has ${remainingParkingSlots} empty spots left.`);

        let vehiclesArr = this.vehicles
        .slice()
        .sort((a, b) => a.carModel.localeCompare(b.carModel))
        .forEach(v => resultStrArr.push(`${v.carModel} == ${v.carNumber} - ${v.payed ? 'Has payed' : 'Not payed'}`))

        return resultStrArr.join('\n');
    }
}

const parking = new Parking(12);

console.log(parking.addCar("Volvo t600", "TX3691CA"));
console.log(parking.getStatistics());

console.log(parking.pay("TX3691CA"));
console.log(parking.removeCar("TX3691CA"));
