async function getInfo() {
    const stopNameElement = document.getElementById('stopName');
    const timetableElement = document.getElementById('buses');

    const stopIdElement = document.getElementById('stopId');
    // @ts-ignore
    const stopId = stopIdElement.value;

    const url = `http://localhost:3030/jsonstore/bus/businfo/${stopId}`;
    
    let btn = document.getElementById('submit');
    btn.disabled = true;
    btn.style.background = 'grey';

    try {
        stopNameElement.textContent = 'Loading...';
        timetableElement.replaceChildren();
        let result = await fetch(url);

        if (result.status != 200) {
            throw new Error('Stop ID not found');
        }

        let data = await result.json();
        stopNameElement.textContent = data.name;

        Object.entries(data.buses).forEach(b => {
            let liElement = document.createElement('li');
            liElement.textContent = `Bus ${b[0]} arrives in ${b[1]} minutes`;

            timetableElement.appendChild(liElement);
        });
    } catch (error) {
        stopNameElement.textContent = 'Error';
    }

    btn.disabled = false;
    btn.style.background = '#45A049';
}