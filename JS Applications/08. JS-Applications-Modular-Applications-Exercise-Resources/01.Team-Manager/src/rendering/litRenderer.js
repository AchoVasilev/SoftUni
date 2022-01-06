import { render } from '../lib.js';

export class LitRenderer {
    constructor() {
    }

    createRenderHandler(domElement) {
        return function (templateResult) {
            render(templateResult, domElement);
        };
    }
}