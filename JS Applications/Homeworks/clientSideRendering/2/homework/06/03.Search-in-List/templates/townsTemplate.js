import { html } from '../../node_modules/lit-html/lit-html.js';
import { ifDefined } from '../../node_modules/lit-html/directives/if-defined.js';

let liTownTemplate = (town) => html`<li class=${ifDefined(town.class)}>${town.name}</li>`;

export let ulTownsTemplate = (towns) => html`
<ul>
    ${towns.map(t => liTownTemplate(t))}
</ul>`