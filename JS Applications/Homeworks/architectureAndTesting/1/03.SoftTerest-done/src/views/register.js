import { showSection } from "../helper.js";
import { register } from '../api/data.js';

let ctx = null;
const section = document.getElementById('registerPage');
section.querySelector('#sign-in').addEventListener('click', (event) => {
    event.preventDefault();
    ctx.goTo('loginLink');
})
const form = section.querySelector('form');
form.addEventListener('submit', onRegister);

export function showRegister(contex) {
    ctx = contex;
    showSection(section);
}

async function onRegister(event) {
    event.preventDefault();

    const formData = new FormData(form);

    const email = formData.get('email');
    const password = formData.get('password');
    const repass = formData.get('repeatPassword');

    try {
        if (email == '' || password == '' || repass == '') {
            throw new Error('All fields are required.');
        }
        if (password != repass) {
            throw new Error('Password don\'t match');
        }

        await register(email, password);
        form.reset();

        ctx.goTo('homeLink');

    } catch (error) {
        alert(error.message);
    }
}