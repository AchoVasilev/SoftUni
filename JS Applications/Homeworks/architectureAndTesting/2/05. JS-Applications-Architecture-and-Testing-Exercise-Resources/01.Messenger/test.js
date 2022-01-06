const { chromium } = require('playwright-chromium');
const { expect } = require('chai');


let browser, page; // Declare reusable variables
let clientUrl = 'http://127.0.0.1:5500'

function fakeResponse(data) {
    return {
        status: 200,
        headers: {
            'Access-Control-Allow-Origin': '*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data),
    }
}

let testMessages = {
    1: {
        author: 'Pesho',
        content: 'My message'
    },
    2: {
        author: 'George',   
        content: 'My george message'
    }
}
let testCreateMessage = {
    3: {
        author: 'Mite Test',
        content: 'Mite message',
        _id: 3
    },
}

describe('E2E tests', function () {
    before(async () => { 
        // debug only
        // browser = await chromium.launch( { headless: false, slowMo: 2000}); 
        browser = await chromium.launch(); 
    });
    after(async () => { await browser.close(); });
    beforeEach(async () => { page = await browser.newPage(); });
    afterEach(async () => { await page.close(); });

    describe('load messages', () => {
        it('should call server', async () => {
            await page.route('**/jsonstore/messenger', route => {
                route.fulfill(fakeResponse(testMessages))
            });

            await page.goto(clientUrl);


            const [response] = await Promise.all([
                page.waitForResponse('**/jsonstore/messenger'),
                page.click('#refresh')
            ]);
            let result = await response.json();
            expect(result).to.eql(testMessages);
        });

        it('should show data in text area', async () => {
            await page.route('**/jsonstore/messenger', route => {
                route.fulfill(fakeResponse(testMessages))
            });

            await page.goto(clientUrl);

            const [response] = await Promise.all([
                page.waitForResponse('**/jsonstore/messenger'),
                page.click('#refresh')
            ]);
            
            let textAreaText = await page.$eval('#messages', (textArea) => textArea.value);

            let text = Object.values(testMessages).map(v => `${v.author}: ${v.content}`).join('\n');
            console.log(text)
            expect(textAreaText).to.eql(text);
        });

        it('should create message when fill form', async () => {
            let requestData = undefined;
            await page.route('**/jsonstore/messenger', (route, request) => {
                if(request.method().toLowerCase() == 'post'){
                    requestData = request.postData()
                    route.fulfill(fakeResponse(testCreateMessage))
                }
            });

            await page.goto(clientUrl);
            await page.fill('#author', "Mite Test"),
            await page.fill('#content', "Mite message")

            const [response] = await Promise.all([
                page.waitForResponse('**/jsonstore/messenger'),
                page.click('#submit')
                
            ]);
            let result = await response.json();
            expect(result).to.eql(testCreateMessage);
        });
    })
});
