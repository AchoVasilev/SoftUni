import { html } from '../lib.js';

export const modalTemplate = html`
<div class="overlay">
    <div class="modal">
        <p>Overlay message</p>
        <a href="#" class="action">Action</a>
    </div>
</div>`;