class Ticket{
    private destination: string;
    private price: number;
    private status: string;

    constructor(destination: string, price: number, status: string) {
        this.destination = destination;
        this.price = price;
        this.status = status;
    }
}

function solve(arr = [], sortingType: string) {
    const result = [];

    for (let i = 0; i < arr.length; i++){
        const args = arr[i].split('|');

        const destination = args[0];
        const price = args[1];
        const status = args[2];

        result.push(new Ticket(destination, price, status));
    }

    const sortings = {
        destination: (a, b) => a.destination.localeCompare(b.destination),
        price: (a, b) => a - b,
        status: (a, b) => a.status.localeCompare(b.status)
    };

    result.sort(sortings[sortingType]);

    console.log(result);
}

solve([
    'Philadelphia|94.20|available',
    'New York City|95.99|available',
    'New York City|95.99|sold',
    'Boston|126.20|departed'
],
    'destination'
)