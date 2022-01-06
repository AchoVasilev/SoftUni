function solve() {;
    const info = document.querySelector('#info span')
    const departBtn = document.getElementById('depart');
    const arriveBtn= document.getElementById('arrive');
    let dataName;
    let dataNext;
    let url = `http://localhost:3030/jsonstore/bus/schedule/depot`;

    async function depart() {
        try {
            const res = await fetch(url);
            if (res.status != 200) {
                throw new Error('Next stop not found');
            }
            const data = await res.json();
            dataName = data.name;
            dataNext = data.next;
            url = `http://localhost:3030/jsonstore/bus/schedule/${dataNext}`;

            info.textContent = `Next stop ${dataName}`;
            departBtn.disabled = true;
            arriveBtn.disabled = false;
        }
        catch(error) {
            info.textContent = 'Error';
            departBtn.disabled = true;
            arriveBtn.disabled = true;
        }
    }

    function arrive() {
        info.textContent = `Arriving at ${dataName}`;

        departBtn.disabled = false;
        arriveBtn.disabled = true;
    }

    return {
        depart,
        arrive
    };
}

let result = solve();