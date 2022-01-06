attachEvents();

function attachEvents() {

    const locationInput = document.getElementById('location');
    const getWeatherButton = document.getElementById('submit');

    const forecastDIV = document.getElementById('forecast');
    const currentForecastDIV = document.getElementById('current');
    const upcomingForecastDIV = document.getElementById('upcoming');

    const degreesSymbol = getSymbol('Degrees');

    getWeatherButton.addEventListener('click', async function () {

        clearPage();
        loadingPage();

        getWeatherButton.disabled = true;

        if (forecastDIV.style.display === 'none') {
            forecastDIV.style.display = 'block';
        }

        const location = locationInput.value;
        locationInput.value = '';

        try {
            const forecast = await getForecast(location);
            const { name, currentForecast, upcomingForecast } = forecast;

            clearPage();

            showCurrentForecast(name, currentForecast);
            showUpcomingForecast(upcomingForecast);

        } catch (e) {
            clearPage();

            currentForecastDIV.appendChild(
                newEl('span', {}, 'Error')
            );

            upcomingForecastDIV.appendChild(
                newEl('span', {}, 'Error')
            );
        } finally {
            getWeatherButton.disabled = false;
        }
    });

    function clearPage() {

        while (currentForecastDIV.children.length > 1) {
            currentForecastDIV.removeChild(currentForecastDIV.lastElementChild);
        }

        while (upcomingForecastDIV.children.length > 1) {
            upcomingForecastDIV.removeChild(upcomingForecastDIV.lastElementChild);
        }

    }

    function loadingPage() {
        currentForecastDIV.appendChild(newEl('span', {}, 'Loading...'));
        upcomingForecastDIV.appendChild(newEl('span', {}, 'Loading...'));
    }

    async function getForecast(locName) {
        const locationCode = await getLocationCode(locName);

        const { name, currentForecast } = await getCurrentForecast(locationCode);

        const upcomingForecast = await getUpcomingForecast(locationCode);

        return {
            name,
            currentForecast,
            upcomingForecast
        };

    }

    async function getLocationCode(name) {
        const locationsURL = `http://localhost:3030/jsonstore/forecaster/locations`;

        const locationsResponse = await fetch(locationsURL);
        const locationsInfo = await locationsResponse.json();

        const { code } = locationsInfo.find((loc) => loc.name === name);

        return code;
    }

    async function getCurrentForecast(code) {
        const currentForecastURL = `http://localhost:3030/jsonstore/forecaster/today/${code}`;

        const currentConditionsResponse = await fetch(currentForecastURL);
        const { name, forecast: currentForecast } = await currentConditionsResponse.json();

        return { name, currentForecast };
    }

    async function getUpcomingForecast(code) {
        const upcomingForecastURL = `http://localhost:3030/jsonstore/forecaster/upcoming/${code}`;

        const conditionsAheadResponse = await fetch(upcomingForecastURL);
        const { forecast: upcomingForecast } = await conditionsAheadResponse.json();

        return upcomingForecast;
    }

    function showCurrentForecast(name, { condition, high: highTemp, low: lowTemp }) {
        const symbol = getSymbol(condition);
        currentForecastDIV.appendChild(
            newEl('div', { class: "forecasts" },
                newEl('span', { class: "condition symbol" }, symbol),
                newEl('span', { class: "condition" },
                    newEl('span', { class: "forecast-data" }, name),
                    newEl('span', { class: "forecast-data" },
                        `${lowTemp}${degreesSymbol}/${highTemp}${degreesSymbol}`),
                    newEl('span', { class: "forecast-data" }, condition)
                )
            ));
    }

    function showUpcomingForecast(threeDaysForecast) {

        upcomingForecastDIV.appendChild(
            newEl('div', { class: "forecast-info" })
        );
        const forecastInfoDIV = upcomingForecastDIV.querySelector('.forecast-info');

        threeDaysForecast.forEach((dayForecast) => {
            const { condition, high: highTemp, low: lowTemp } = dayForecast;
            const symbol = getSymbol(condition);

            forecastInfoDIV.appendChild(
                newEl('span', { class: "upcoming" },
                    newEl('span', { class: 'symbol' }, symbol),
                    newEl('span', { class: "forecast-data" },
                        `${lowTemp}${degreesSymbol}/${highTemp}${degreesSymbol}`),
                    newEl('span', { class: "forecast-data" }, condition))
            );
        });
    }

    function newEl(type, attr, ...content) {
        const element = document.createElement(type);

        for (const prop in attr) {
            if (prop === 'class') {
                let classes = attr[prop].split(' ');
                for (let cls of classes) {
                    element.classList.add(cls);
                }
            } else {
                element.setAttribute(prop, attr[prop]);
            }
        }

        for (let item of content) {
            if (typeof item == 'string' || typeof item == 'number') {
                item = document.createTextNode(item);
            }

            element.appendChild(item);
        }
        return element;
    }

    function getSymbol(condition) {
        const symbols = {
            "Sunny": "☀",
            "Partly sunny": "⛅",
            "Overcast": "☁",
            "Rain": "☂",
            "Degrees": "°"
        }

        return symbols[condition];
    }
}


