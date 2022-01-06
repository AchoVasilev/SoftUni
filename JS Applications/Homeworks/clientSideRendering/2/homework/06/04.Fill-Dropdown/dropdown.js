import { optionsTemplate } from "./templates/optionTemplate.js";
import { render } from '../node_modules/lit-html/lit-html.js';

const selectElment = document.getElementById('menu');

onRender();

async function onRender() {
    const optionsResponse = await fetch('http://localhost:3030/jsonstore/advanced/dropdown');
    const optionsObj = await optionsResponse.json();
    const options = Object.values(optionsObj);
    let finalOptions = options.map(o => optionsTemplate(o));
    render(finalOptions,selectElment);
}

const form = document.getElementById('options-form');
form.addEventListener('submit', add);

async function add(e) {
    e.preventDefault();
    const formData = new FormData(form);
    const text = formData.get('input');
    
    const url = 'http://localhost:3030/jsonstore/advanced/dropdown';
    let options = {
        method: 'post',
        headers: {
            'Content-type': 'application-json'
        },
        body: JSON.stringify({text})
    }

    const resposne = await fetch(url,options);
    
    if (resposne.ok == true) {
         await onRender();
    }
    form.reset();
}