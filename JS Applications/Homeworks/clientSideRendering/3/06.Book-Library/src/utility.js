import { html, render } from '../node_modules/lit-html/lit-html.js';

export {
    html,
    render,
}

const host = 'http://localhost:3030/jsonstore/collections/'

async function request(url, method = 'get', data) {
    const options = {
        method,
        headers: {},
    };

    if(data != undefined) {
        options.headers['Content-Type'] = 'application/json';
        options.body = JSON.stringify(data);
    }

    const response = await fetch(url, options);
}