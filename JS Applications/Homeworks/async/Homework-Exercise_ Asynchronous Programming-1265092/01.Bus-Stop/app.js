function getInfo() {
    const checkButton = document.querySelector('#submit');
    const stopNameDiv = document.getElementById('stopName');
    const busesUL = document.getElementById('buses');
    const stopIdEl = document.getElementById('stopId');
    const url = `http://localhost:3030/jsonstore/bus/businfo/${stopIdEl.value}`;

    clearPage();

    getInformation()
        .then(fillPage)
        .catch(() => {
            stopNameDiv.textContent = "Error";
            checkButton.disabled = false;
        });

    function clearPage() {
        stopNameDiv.textContent = 'Loading...';
        busesUL.replaceChildren();
        stopIdEl.value = '';
    }

    async function getInformation() {
        checkButton.disabled = true;

        const response = await fetch(url);

        if (response.status !== 200) {
            throw new Error('Could not fetch the data');
        }

        const stopInfo = await response.json();

        return {
            stopName: stopInfo.name,
            busesInfo: Object.entries(stopInfo.buses)
        }
    }

    function fillPage(info) {
        stopNameDiv.textContent = info.stopName;
        checkButton.disabled = false;

        info.busesInfo.forEach((busInfo) => {
            const li = document.createElement('li');
            li.textContent = `Bus ${busInfo[0]} arrives in ${busInfo[1]} minutes`;
            busesUL.appendChild(li);
        });
    }
}