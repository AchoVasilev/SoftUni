const { expect } = require("chai");
const { chromium } = require('playwright-chromium');
let page, browser;

const mockDataJson = {
    "messages": {
        "-LxHVtajG3N1sU714pVj": {
            "author": "Spami",
            "content": "Hello, are you there?"
        },
        "-LxIDxC-GotWtf4eHwV8": {
            "author": "Garry",
            "content": "Yep, whats up :?"
        },
        "-LxIDxPfhsNipDrOQ5g_": {
            "author": "Spami",
            "content": "How are you? Long time no see? :)"
        },
        "-LxIE-dM_msaz1O9MouM": {
            "author": "George",
            "content": "Hello, guys! :))"
        },
        "-LxLgX_nOIiuvbwmxt8w": {
            "author": "Spami",
            "content": "Hello, George nice to see you! :)))"
        }
    }
};

function makeRequest(data) {
    return {
        status: 200,
        headers: {
            'Access-Control-Allow-Origin': '*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    };
}

describe('tests', async function () {
    this.timeout(60000);

    before(async () => {
        browser = await chromium.launch();
    });

    after(async () => {
        await browser.close();
    });

    beforeEach(async () => {
        page = await browser.newPage();
    });

    afterEach(async () => {
        await page.close();
    });

    it('loads messages on pressing the refresh button', async () => {
        await page.route(
            '**/jsonstore/messenger*',
            request => request.fulfill(makeRequest(mockDataJson.messages))
        );

        await page.goto('http://localhost:5500/01.Messenger/');
        await page.waitForSelector('#messages');

        const [response] = await Promise.all([
            page.waitForResponse(r => r.request().url()
                .includes('/jsonstore/messenger') && r.request().method() === 'GET'),
            page.click('#refresh')
        ]);

        const responseData = await response.json();

        expect(responseData).to.deep.eq(mockDataJson.messages);
    });

    it(`testing proper form submit`, async () => {
        await page.goto('http://localhost:5500/01.Messenger/');
        await page.waitForSelector('#controls');
        await page.route(
            '**/jsonstore/messenger*',
            request => request.fulfill(makeRequest({ author: 'Pesho', content: 'Pesho is Peshak' }))
        );

        await page.fill('#author', 'Pesho');
        await page.fill('#content', 'Pesho is Peshak');

        const [response] = await Promise.all([
            page.waitForRequest(r => r.url()
                .includes('/jsonstore/messenger') &&
                r.method() === 'POST'),
            page.click('#submit')
        ]);

        const responseData = JSON.parse(await response.postData());

        expect(responseData).to.deep.eq({ 'author': 'Pesho', 'content': 'Pesho is Peshak' });
    });
})