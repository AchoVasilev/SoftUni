function createCard(card, suit) {
    let cardsArr = ['2', '3', '4', '5', '6', '7', '8', '9', '10', 'J', 'Q', 'K', 'A'];

    let suits = {
        S: '\u2660',
        H: '\u2665',
        D: '\u2666',
        C: '\u2663'
    };

    if (!cardsArr.includes(card)) {
        throw new Error('Error');
    }

    if (!Object.keys(suits).includes(suit)) {
        throw new Error('Invalid suit');
    }

    return {
        card,
        suit: suits[suit],
        toString() {
            return this.card + this.suit;
        }
    }
}

let card = createCard('A', 'S').toString();
console.log(card);