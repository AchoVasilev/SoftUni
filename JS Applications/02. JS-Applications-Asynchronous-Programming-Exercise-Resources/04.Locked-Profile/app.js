function lockedProfile() {
    (async () => {
        let profileRequest = await fetch(`http://localhost:3030/jsonstore/advanced/profiles`);
        let profiles = await profileRequest.json();

        let mainSection = document.getElementById('main');
        let templateProfile = document.querySelector('.profile');
        templateProfile.remove();

        Object.keys(profiles).forEach((key, i) => {
            let profile = profiles[key];
            let htmlProfile = createHtmlProfile(i + 1, profile.username, profile.email, profile.age);
            mainSection.appendChild(htmlProfile);
        });
    })();

    function createHtmlProfile(userIndex, username, email, age) {
        let profileDiv = document.createElement('div');
        profileDiv.classList.add('profile');

        let profileImg = document.createElement('img');
        profileImg.src = './iconProfile2.png';
        profileImg.classList.add('userIcon');

        let lockRadioLabel = document.createElement('label');
        lockRadioLabel.textContent = 'Lock';

        let lockRadioBtn = document.createElement('input');
        lockRadioBtn.type = 'radio';
        lockRadioBtn.name = `user${userIndex}Locked`;
        lockRadioBtn.value = 'lock';
        lockRadioBtn.checked = true;

        let unlockRadioLabel = document.createElement('label');
        unlockRadioLabel.textContent = 'Unlock';

        let unlockRadioBtn = document.createElement('input');
        unlockRadioBtn.type = 'radio';
        unlockRadioBtn.name = `user${userIndex}Locked`;
        unlockRadioBtn.value = 'unlock';

        let br = document.createElement('br');
        let hr = document.createElement('hr');

        let usernameLabel = document.createElement('label');
        usernameLabel.textContent = 'Username';

        let usernameInput = document.createElement('input');
        usernameInput.name = `user${userIndex}Username`;
        usernameInput.value = username;
        usernameInput.readOnly = true;
        usernameInput.disabled = true;

        let hiddenFieldsDiv = document.createElement('div');
        hiddenFieldsDiv.id = `user${userIndex}HiddenFields`;

        let hiddenFieldsHr = document.createElement('hr');

        let emailLabel = document.createElement('label');
        emailLabel.textContent = 'Email:';

        let emailInput = document.createElement('input');
        emailInput.type = 'email';
        emailInput.name = `user${userIndex}Email`;
        emailInput.value = email;
        emailInput.readOnly = true;
        emailInput.disabled = true;

        let ageLabel = document.createElement('label');
        ageLabel.textContent = 'Age:';

        let ageInput = document.createElement('input');
        ageInput.type = 'age';
        ageInput.name = `user${userIndex}Age`;
        ageInput.value = age;
        ageInput.readOnly = true;
        ageInput.disabled = true;

        hiddenFieldsDiv.appendChild(hiddenFieldsHr);
        hiddenFieldsDiv.appendChild(emailLabel);
        hiddenFieldsDiv.appendChild(emailInput);
        hiddenFieldsDiv.appendChild(ageLabel);
        hiddenFieldsDiv.appendChild(ageInput);

        let showMoreBtn = document.createElement('button');
        showMoreBtn.textContent = 'Show more';
        showMoreBtn.addEventListener('click', showHiddenInfoHandler);

        profileDiv.appendChild(profileImg);
        profileDiv.appendChild(lockRadioLabel);
        profileDiv.appendChild(lockRadioBtn);
        profileDiv.appendChild(unlockRadioLabel);
        profileDiv.appendChild(unlockRadioBtn);
        profileDiv.appendChild(br);
        profileDiv.appendChild(hr);
        profileDiv.appendChild(usernameLabel);
        profileDiv.appendChild(usernameInput);
        profileDiv.appendChild(hiddenFieldsDiv);
        profileDiv.appendChild(showMoreBtn);

        return profileDiv;
    }

    function showHiddenInfoHandler(ev) {
        let profile = ev.target.parentElement;
        let showMoreBtn = ev.target;
        let hiddenFieldsDiv = ev.target.previousElementSibling;
        let radioBtn = profile.querySelector('input[type="radio"]:checked');

        if (radioBtn.value !== 'unlock') {
            return;
        }

        showMoreBtn.textContent = showMoreBtn.textContent === 'Show more' ?
            'Hide it' :
            'Show more';

        hiddenFieldsDiv.style.display = hiddenFieldsDiv.style.display === 'block' ?
            'none' :
            'block';
    }
}