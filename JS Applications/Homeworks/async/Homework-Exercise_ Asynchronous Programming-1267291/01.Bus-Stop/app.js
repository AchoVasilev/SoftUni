async function getInfo() {
    const stopId = document.getElementById('stopId');
    const stopName = document.getElementById('stopName');
    const url = `http://localhost:3030/jsonstore/bus/businfo/${stopId.value}`;
    const buses = document.getElementById('buses');

    try {
        stopName.textContent = 'Loading...';
        buses.replaceChildren();

        const res = await fetch(url);
        if (res.status != 200 || stopId.value == '') {
            throw new Error('Stop ID not found');
        }

        const data = await res.json();

        stopName.textContent = data.name;
        Object.entries(data.buses).forEach(b => {
            const liElement = document.createElement('li');
            liElement.textContent = `Bus ${b[0]} arrives in ${b[1]} minutes`;
            buses.appendChild(liElement);
        })
    }
    catch(error) {
        stopName.textContent = 'Error';
    }
    
    stopId.value = '';
}