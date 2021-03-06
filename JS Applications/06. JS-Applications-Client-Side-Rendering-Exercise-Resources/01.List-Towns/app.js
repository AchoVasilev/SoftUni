import { html, render } from './node_modules/lit-html/lit-html.js';

const root = document.getElementById('root');
document.querySelector('form').addEventListener('submit', (ev) => {
    ev.preventDefault();

    const towns = document.getElementById('towns').value
        .split(',')
        .map(t => t.trim());
    
    const result = listTemplate(towns);
    render(result, root);
});

const listTemplate = (townsInput) => html`
<ul>
    ${townsInput.map(t => html`<li>${t}</li>`)}
</ul>
`