window.addEventListener('DOMContentLoaded', () => {
    const form = document.querySelector('form');
    form.addEventListener('submit', reg);
    document.getElementById('logout').style.display = 'none';

});
async function reg(ev) {
    ev.preventDefault();
    const formData = new FormData(ev.target);
    const email = formData.get("email");
    const password = formData.get("password");
    const rePass = formData.get("rePass");
    const notification = document.querySelector('.notification');
    const submitBTN = ev.target.querySelector('button');


    try {
        submitBTN.disabled = true;
        notification.textContent = "Loading...";
        const url = "http://localhost:3030/users/register";
        const data = {
            email,
            password,
            rePass
        }
        const options = {
            method: 'post',
            heades: {
                'Content-Type': 'applications/json'
            },
            body: JSON.stringify(data)
        }

        const res = await fetch(url, options);

        if (res.ok == false) {
            const error = await res.json();
            throw new Error(error.message);
        }
        const result = await res.json();
        notification.replaceChildren();
        submitBTN.disabled = false;
        const userData = {
            email,
            password,
            rePass,
            id: result._id,
            token: result.accessToken
        };
        sessionStorage.setItem('userData', JSON.stringify(userData));
        window.location = '/05.Fisher-Game/index.html';





    } catch (err) {
        notification.textContent = err.message;
        submitBTN.disabled = false;
    }
}
