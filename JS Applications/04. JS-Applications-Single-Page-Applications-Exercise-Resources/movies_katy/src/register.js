//initialization
// - find relevant section
// - detach section from DOM //it remains in memory

import { showHome } from "./home.js";
import { showView } from "./dom.js";
import { updateNav } from "./app.js";


const section = document.getElementById('form-sign-up');
const form = section.querySelector('form');
form.addEventListener('submit', onRegister);
section.remove();

//display logic

export function showRegister() {
    showView(section)
}

async function onRegister(event) {
    event.preventDefault();
    const formData = new FormData(form);
    
    let email = formData.get('email').trim();
    let password = formData.get('password').trim();
    let repeatPassword = formData.get('repeatPassword').trim();
    
    if (email == '' || password !== repeatPassword || password.length < 6) {
        if (email = '') {
            alert('Enter valid email!')
            throw new Error();
        } else if (password.length < 6) {
            alert('Password must be at least 6 characters!')
            throw new Error();
        } else if (password != repeatPassword) {
            alert('Passwords must match!')
            throw new Error();
        }
        showRegister()
    }

    try {
        const res = await fetch('http://localhost:3030/users/register', {
            method: 'post',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ email, password, repeatPassword })
        });
        if (res.ok != true) {
            const error = await res.json();
            throw new Error(error.message)
        }

        const data = await res.json();
        sessionStorage.setItem('userData', JSON.stringify({
            email: data.email,
            id: data._id,
            token: data.accessToken
        }));

        form.reset();
        updateNav();
        showHome();

    } catch (err) {
        alert(err.message)
    }

}