window.addEventListener('DOMContentLoaded', () => {
    const form = document.querySelector('form');
    form.addEventListener('submit', onLogin);
    document.getElementById('logout').style.display = 'none';

});

async function onLogin(ev) {
    ev.preventDefault();
    document.getElementById('logout').style.display = 'none';
    const formData = new FormData(ev.target);
    const notification = document.querySelector('.notification');
    const submitBTN = ev.target.querySelector('button');
    const email = formData.get('email');
    const password = formData.get('password');
    try {
        submitBTN.disabled = true;
        notification.textContent = "Loading...";
        const res = await fetch('http://localhost:3030/users/login', {
            method: 'post',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ email, password })
        });
        if (res.ok != true) {
            const error = await res.json();
            throw new Error(error.message);
        }

        const data = await res.json();
        notification.replaceChildren();
        submitBTN.disabled = false;
        const userData = {
            email: data.email,
            id: data._id,
            token: data.accessToken
        };
        sessionStorage.setItem('userData', JSON.stringify(userData));
        window.location = '/05.Fisher-Game/index.html';
    } catch(err) {
        notification.textContent = err.message;
        submitBTN.disabled = false;
    }
}