function solve() {
    const label = document.querySelector('#info span');
    const departBtn = document.getElementById('depart');
    const arriveBtn = document.getElementById('arrive');

    let stop = {
        next: 'depot'
    };

    async function depart() {
        departBtn.disabled = true;

        let url = `http://localhost:3030/jsonstore/bus/schedule/${stop.next}`;

        let result = await fetch(url);
        stop = await result.json();
        
        label.textContent = `Next stop ${stop.name}`;

        arriveBtn.disabled = false;
    }

    function arrive() {
        arriveBtn.disabled = true;

        label.textContent = `Arriving at ${stop.name}`;
        
        departBtn.disabled = false;
    }

    return {
        depart,
        arrive
    };
}

let result = solve();