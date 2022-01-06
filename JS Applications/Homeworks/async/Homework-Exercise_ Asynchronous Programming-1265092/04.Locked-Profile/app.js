function lockedProfile() {

    const mainEl = document.getElementById('main');
    const profileTemplateDIV = document.querySelector('#main .profile');
    let totalReadyProfiles = 0;

    removeProfileTemplate();

    const loadingEl = newEl('p', {}, 'Loading...');
    mainEl.appendChild(loadingEl);

    getProfiles()
        .then(profiles => {
            loadingEl.remove();

            createProfileCards(profiles);
            mainEl.addEventListener('click', onClick);
        })
        .catch(err => {
            loadingEl.remove();
            const paragraphEl = document.createElement('p');
            paragraphEl.textContent = 'ERROR: ' + err.message;
            mainEl.appendChild(paragraphEl)
        });

    async function getProfiles() {
        const url = `http://localhost:3030/jsonstore/advanced/profiles`;
        const response = await fetch(url);
        const profiles = await response.json();

        return Object.values(profiles);
    }

    function createProfileCards(profiles) {
        profiles.forEach(function (profile) {
            const profileInfo = Object.values(profile).slice(1);
            const profileCard = profileTemplateDIV.cloneNode(true);
            totalReadyProfiles++;

            fillCard(profileCard, profileInfo, totalReadyProfiles);
            mainEl.appendChild(profileCard);
        });
    }

    function fillCard(profileCard, profileInfo, totalReadyProfiles) {
        const radioButtons = profileCard.querySelectorAll('input[type="radio"]');
        radioButtons.forEach((btn) => btn.name = `user${totalReadyProfiles}Locked`);

        const emptyInputs = profileCard.querySelectorAll('input[value=""]');

        for (let i = 0; i < profileInfo.length; i++) {
            emptyInputs[i].value = profileInfo[i];
            emptyInputs[i].setAttribute('value', profileInfo[i]);
        }
    }

    function onClick(ev) {
        if (ev.target.tagName === 'BUTTON') {
            const button = ev.target;
            const lockButton = button.parentElement.querySelector('input[value="lock"]');
            if (!lockButton.checked) {
                if (button.textContent === 'Show more') {
                    button.previousElementSibling.style.display = 'block';
                    button.textContent = 'Hide it';
                } else {
                    button.previousElementSibling.style.display = 'none';
                    button.textContent = 'Show more';
                }
            }
        }
    }

    function removeProfileTemplate() {
        profileTemplateDIV.remove();
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
}





