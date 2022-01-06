function lockedProfile() {
    Array.from(document.querySelectorAll('.profile button'))
        .forEach(x => x.addEventListener('click', onToggle));
    
    function onToggle(ev) {
        let profile = ev.target.parentElement;
        let isActive = profile.querySelector('input[type="radio"][value="unlock"]').checked;

        if (isActive) {
            let infoDiv = ev.target.parentElement.querySelector('div');

            if (ev.target.textContent == 'Show more') {
                infoDiv.style.display = 'block';
                ev.target.textContent = 'Hide it';
            } else {
                infoDiv.style.display = '';
                ev.target.textContent = 'Show more';
            }
        }
    }
}