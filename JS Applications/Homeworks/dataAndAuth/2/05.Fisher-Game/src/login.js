// when <script> is before <body>, in <head>, wait for the page/DOM to load first!
window.addEventListener('DOMContentLoaded', () => {
    const form = document.querySelector('form');
    form.addEventListener('submit', onSubmit);

    const home = document.getElementById('home');
    home.setAttribute('class', '');

    const guest = document.getElementById('guest');
    guest.children[0].setAttribute('class', 'active');
    guest.children[1].setAttribute('class', '');

    const user = document.getElementById('user');
    const logout = user.children[0];
    
    if (sessionStorage.getItem('accessToken') != null){ // logged
        guest.style.display = 'none';
        user.style.display = 'inline-block';
        logout.addEventListener('click', onLogout);
    } else { // not logged
        guest.style.display = 'inline-block';
        user.style.display = 'none';
    }
}); 

function formValidation(form){
    const email = /\S+@\S+\.\S+/;
    const formData = new FormData(form);

    if(!formData.get('email').match(email)) {
        return false;
    } else {
        return true;
    }
}

async function onLogout(e){
    // send authorized get request
    const url = 'http://localhost:3030/users/logout';
    const res = await fetch(url, {
        method: "GET",
        headers: {
            "X-Authorization": sessionStorage.getItem('accessToken')
        }
    });

    if (res.status == 204) {
        alert('Logged out!');
        sessionStorage.clear();
    } else {
        alert('Error with auth...');
    }

    location.href = 'index.html';
}

async function onSubmit(e){
    e.preventDefault();
    const formData = new FormData(e.target); // e.target because form is out of scope.
    const url = 'http://localhost:3030/users/login';

    if (formValidation(e.target)) {
        const res = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                "email": formData.get('email'),
                "password": formData.get('password')
            })
        });

        if (res.status != 403){
            // handle auth token
            const data = await res.json();
            const token = data.accessToken;

            sessionStorage.setItem('accessToken', token);
            sessionStorage.setItem('indicator', data.email);
            sessionStorage.setItem('_id', data['_id']);

            e.target.reset();
            location.href = 'index.html';
        } else {
            alert('Invalid email or password.Try again!');
            e.target.reset();
        }
        
    } else {
        alert('Invalid email or password. Try again!');
        e.target.reset();
    }
}