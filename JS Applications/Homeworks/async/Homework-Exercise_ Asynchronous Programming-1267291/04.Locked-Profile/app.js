async function lockedProfile() {
    function createProfile(data, number) {
        const divElement = document.createElement('div');
        divElement.className = 'profile';

        const imgElement = document.createElement('img');
        imgElement.src = "./iconProfile2.png";
        imgElement.className = 'userIcon';

        const labelLock = document.createElement('label');
        labelLock.textContent = 'Lock';

        const lockBtn = document.createElement('input');
        lockBtn.type = 'radio';
        lockBtn.name = `user${number}Locked`;
        lockBtn.value = 'lock';
        lockBtn.checked = true;

        const labelUnlock = document.createElement('label');
        labelUnlock.textContent = 'Unlock';

        const unlockBtn = document.createElement('input');
        unlockBtn.type = 'radio';
        unlockBtn.name = `user${number}Locked`;
        unlockBtn.value = 'unlock';

        const br = document.createElement('br');
        const hr = document.createElement('hr');

        const username = document.createElement('label');
        username.textContent = 'Username';

        const usernameValue = document.createElement('input');
        usernameValue.type = 'text';
        usernameValue.name = `user${number}Username`;
        usernameValue.value = data.username;
        usernameValue.disabled = true
        usernameValue.readOnly = true;


        const secondDiv = document.createElement('div');
        secondDiv.className = `hiddenInfo`;
        
        const secondHr = document.createElement('hr');

        const emailLabel = document.createElement('label');
        emailLabel.textContent = 'Email:';

        const emailInput = document.createElement('input');
        emailInput.type = 'email';
        emailInput.name = `user${number}Email`;
        emailInput.value = data.email;
        emailInput.disabled = true;
        emailInput.readOnly = true;

        const ageLabel = document.createElement('label');
        ageLabel.textContent = 'Age:'

        const ageInput = document.createElement('input');
        ageInput.type = 'email';
        ageInput.name = `user${number}Age`;
        ageInput.value = data.age;
        ageInput.disabled = true;
        ageInput.readOnly = true;

        const showMore = document.createElement('button');
        showMore.textContent = 'Show more';
        showMore.addEventListener('click', onClick);

        secondDiv.appendChild(secondHr);
        secondDiv.appendChild(emailLabel);
        secondDiv.appendChild(emailInput);
        secondDiv.appendChild(ageLabel);
        secondDiv.appendChild(ageInput);

        divElement.appendChild(imgElement);
        divElement.appendChild(labelLock);
        divElement.appendChild(lockBtn);
        divElement.appendChild(labelUnlock);
        divElement.appendChild(unlockBtn);
        divElement.appendChild(br);
        divElement.appendChild(hr);
        divElement.appendChild(username);
        divElement.appendChild(usernameValue);
        divElement.appendChild(secondDiv);
        divElement.appendChild(showMore);

        document.getElementById('main').appendChild(divElement);
    }

    function onClick(ev) {
        const lockedButton = ev.target.parentElement.querySelector('input');
        const hiddenElements = ev.target.parentElement.querySelector('.hiddenInfo');

        if (lockedButton.checked == false) {
            if (ev.target.textContent == 'Show more') {
                for (let child of hiddenElements.children) {
                    child.style.display = 'block';
                }
                ev.target.textContent = 'Hide it'
            }
            else {
                for (let child of hiddenElements.children) {
                    child.style.display = 'none';
                }
                ev.target.textContent = 'Show more';
            }
        }
    }

    document.querySelector('#main .profile button').addEventListener('click', onClick);
    let url = 'http://localhost:3030/jsonstore/advanced/profiles';

    const res = await fetch(url);
    const data = await res.json();

    let i = 2;
    for (let profile of Object.values(data)) {
        createProfile(profile, i);
        i++;
    }
}