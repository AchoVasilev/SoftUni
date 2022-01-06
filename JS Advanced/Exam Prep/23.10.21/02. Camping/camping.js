class SummerCamp {
    constructor(organizer, location) {
        this.organizer = organizer;
        this.location = location;
        this._priceForTheCamp = {
            "child": 150,
            "student": 300,
            "collegian": 500
        };
        this._listOfParticipants = [];
    }

    registerParticipant(name, condition, money) {
        if (!this._priceForTheCamp[condition]) {
            throw new Error('Unsuccessful registration at the camp.');
        }

        if (this._listOfParticipants.some(p => p.name === name)) {
            return `The ${name} is already registered at the camp.`;
        }

        money = Number(money);

        if (money < this._priceForTheCamp[condition]) {
            return `The money is not enough to pay the stay at the camp.`;
        }

        let participant = {
            'name': name,
            'condition': condition,
            'power': 100,
            'wins': 0
        };

        this._listOfParticipants.push(participant);

        return `The ${name} was successfully registered.`;
    }

    unregisterParticipant(name) {
        if (!this._listOfParticipants.some(p => p.name === name)) {
            throw new Error(`The ${name} is not registered in the camp.`);
        }

        this._listOfParticipants = this._listOfParticipants.filter(p => p.name !== name);

        return `The ${name} removed successfully.`;
    }

    timeToPlay(typeOfGame, firstParticipant, secondParticipant) {
        if (!secondParticipant && typeOfGame === 'Battleship') {
            return this._timeToPlay(typeOfGame, firstParticipant);
        } else if (typeOfGame === 'WaterBalloonFights' && firstParticipant && secondParticipant) {
            if (!this._listOfParticipants.some(p => p.name === firstParticipant) ||
                !this._listOfParticipants.some(p => p.name === secondParticipant)) {
                throw new Error(`Invalid entered name/s.`);
            }

            let firstParticipantFound = this._listOfParticipants.find(p => p.name === firstParticipant);
            let secondParticipantFound = this._listOfParticipants.find(p => p.name === secondParticipant);

            if (firstParticipantFound.condition !== secondParticipantFound.condition) {
                throw new Error(`Choose players with equal condition.`);
            }

            if (firstParticipantFound.power > secondParticipantFound.power) {
                firstParticipantFound.wins++;

                return `The ${firstParticipant} is winner in the game ${typeOfGame}.`
            } else if (firstParticipantFound.power < secondParticipantFound.power) {
                secondParticipantFound.wins++
                return `The ${secondParticipant} is winner in the game ${typeOfGame}.`
            } else {
                return `There is no winner.`;
            }
        } else {
            return;
        }
    }

    _timeToPlay(typeOfGame, firstParticipant) {
        if (!this._listOfParticipants.some(p => p.name === firstParticipant)) {
            throw new Error(`Invalid entered name/s.`);
        }

        let participant = this._listOfParticipants.find(p => p.name === firstParticipant);
        participant.power += 20;

        return `The ${firstParticipant} successfully completed the game ${typeOfGame}.`;
    }

    toString() {
        let resultArr = [];

        resultArr.push(`${this.organizer} will take ${this._listOfParticipants.length} participants on camping to ${this.location}`);

        let participants = this._listOfParticipants.sort((a, b) => b.wins - a.wins);

        participants.forEach(p => {
            resultArr.push(`${p.name} - ${p.condition} - ${p.power} - ${p.wins}`)
        });

        return resultArr.join('\n');
    }
}

const summerCamp = new SummerCamp("Jane Austen", "Pancharevo Sofia 1137, Bulgaria");
console.log(summerCamp.registerParticipant("Petar Petarson", "student", 300));
console.log(summerCamp.unregisterParticipant("Petar Petarson"));

console.log(summerCamp.toString());