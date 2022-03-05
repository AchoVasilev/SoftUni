var Ticket = /** @class */ (function () {
    function Ticket(destination, price, status) {
        this.destination = destination;
        this.price = price;
        this.status = status;
    }
    return Ticket;
}());
function solve(arr, sortingType) {
    if (arr === void 0) { arr = []; }
    var result = [];
    for (var i = 0; i < arr.length; i++) {
        var args = arr[i].split('|');
        var destination = args[0];
        var price = args[1];
        var status_1 = args[2];
        result.push(new Ticket(destination, price, status_1));
    }
    var sortings = {
        destination: function (a, b) { return a.destination.localeCompare(b.destination); },
        price: function (a, b) { return a - b; },
        status: function (a, b) { return a.status.localeCompare(b.status); }
    };
    result.sort(sortings[sortingType]);
    console.log(result);
}
solve([
    'Philadelphia|94.20|available',
    'New York City|95.99|available',
    'New York City|95.99|sold',
    'Boston|126.20|departed'
], 'destination');
