import { html } from '../lib.js';

export const editTeamTemplate = (item, onSubmit, errorMsg) => html`
<section @submit=${onSubmit} id="create">
    <article class="narrow">
        <header class="pad-med">
            <h1>New Team</h1>
        </header>
        <form id="create-form" class="main-form pad-large">
            ${errorMsg ? html`<div class="error">${errorMsg}</div>` : null}
            <label>Team name: <input type="text" name="name" .value=${item.name}></label>
            <label>Logo URL: <input type="text" name="logoUrl" .value=${item.logoUrl}></label>
            <label>Description: <textarea name="description" .value=${item.description}></textarea></label>
            <input class="action cta" type="submit" value="Edit Team">
        </form>
    </article>
</section>`;