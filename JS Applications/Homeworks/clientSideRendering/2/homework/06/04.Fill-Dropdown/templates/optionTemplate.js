import { html } from '../../node_modules/lit-html/lit-html.js';

export let optionsTemplate = (option) => html`
<option .value = ${option._id}>${option.text}</option>`;