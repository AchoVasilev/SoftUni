import { login } from '../api/data.js';

let ctx = null;
const section = document.getElementById('loginPage');
section.querySelector('#sign-in').addEventListener('click', (event) => {
    event.preventDefault();
    ctx.goTo('registerLink');
});
const form = section.querySelector('form');
form.addEventListener('submit', onLogin)

export function showLogin(contex) {
    ctx = contex;
    ctx.showSection(section);
}

async function onLogin(event) {
    event.preventDefault();

    const formData = new FormData(form);

    const email = formData.get('email');
    const password = formData.get('password');

    try {
        if (email == '' || password == '') {
            throw new Error('Please enter your email and password.')
        }
        await login(email, password);
        form.reset();
        ctx.goTo('homeLink')

    } catch (error){
        alert(error.message);
    }
    
}