function solve() {
    
    const tableInfoBox = document.querySelector('#info .info');
    const departButton = document.querySelector('#depart');
    const arriveButton = document.querySelector('#arrive');

    let nextStop = 'depot';
    let currentStop;

    function depart() {
        tableInfoBox.textContent = 'Departing...';
        departButton.disabled = true;

        let url = `http://localhost:3030/jsonstore/bus/schedule/${nextStop}`;

        getNextStop(url)
            .then(stop => {
                tableInfoBox.textContent = `Next stop ${stop.name}`;

                nextStop = stop.next;
                currentStop = stop.name;

                arriveButton.disabled = false;
            })
            .catch(err => {
                departButton.disabled = false;

                tableInfoBox.textContent = 'Error: ' + err.message;
            });
    }

    function arrive() {
        departButton.disabled = false;
        arriveButton.disabled = true;
        tableInfoBox.textContent = `Arriving ${currentStop}`;
    }

    async function getNextStop(url) {

        const response = await fetch(url);
        const stop = await response.json();

        return stop;
    };

    return {
        depart,
        arrive
    };
}

let result = solve();