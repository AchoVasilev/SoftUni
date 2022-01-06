const { chromium } = require('playwright-chromium');
const { expect } = require('chai');

let page, browser;

const mockDataJson = {
    "d953e5fb-a585-4d6b-92d3-ee90697398a0": {
        "author": "J.K.Rowling",
        "title": "Harry Potter and the Philosopher's Stone"
    },
    "d953e5fb-a585-4d6b-92d3-ee90697398a1": {
        "author": "Svetlin Nakov",
        "title": "C# Fundamentals"
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

    it('loads and displays all books', async () => {
        await page.route('**/jsonstore/collections/books*', (route, request) => {
            route.fulfill(makeRequest(mockDataJson))
        });

        await page.goto('http://localhost:5500/02.Book-Library/');

        await page.click('text=Load All Books');

        await page.waitForSelector('text=Harry Potter');

        const rows = await page.$$eval('tr', (rows) => rows.map(r => r.textContent.trim()));

        expect(rows[1]).to.contains('Harry Potter');
        expect(rows[1]).to.contains('Rowling');
        expect(rows[2]).to.contains('C# Fundamentals');
        expect(rows[2]).to.contains('Nakov');
    });

    it('can create a book', async () => {
        await page.goto('http://localhost:5500/02.Book-Library/');

        await page.fill('form#createForm >> input[name="title"]', 'Harry Potter and The Prisoner of Azkaban');
        await page.fill('form#createForm >> input[name="author"]', 'J.K. Rowling');

        const [request] = await Promise.all([
            page.waitForRequest(request => request.method() == 'POST'),
            page.click('form#createForm >> text=Submit')
        ]);

        const data = JSON.parse(request.postData());
        expect(data.title).to.equal('Harry Potter and The Prisoner of Azkaban');
        expect(data.author).to.equal('J.K. Rowling');
    });

    it('can delete a book', async () => {
        await page.route('**/jsonstore/collections/books*', (route, request) => {
            route.fulfill(makeRequest(mockDataJson));
        });

        await page.goto('http://localhost:5500/02.Book-Library/');

        await page.route('**/jsonstore/collections/books/*',
            request => request.fulfill(makeRequest({ message: 'book deleted' }))
        );

        await page.click('text=Load All Books');

        page.on('dialog', dialog => dialog.accept());

        const [response] = await Promise.all([
            page.waitForResponse(r => r.request().url().includes('/jsonstore/collections/books') &&
                r.request().method() == 'DELETE'),
            page.click('.deleteBtn')
        ]);

        const data = JSON.parse(await response.body())
        expect(data).to.deep.eq({ message: 'book deleted' })
    });

    it(`loads correct form`, async () => {
        await page.route('**/jsonstore/collections/books*', request =>
            request.fulfill(makeRequest(mockDataJson)));

        await page.goto('http://localhost:5500/02.Book-Library/');

        await page.click('#loadBooks');
        await page.click('.editBtn:nth-child(1)');


        const editFormDisplay = await page.$eval('#editForm', el => el.style.display);
        const createFormDisplay = await page.$eval('#createForm', el => el.style.display);

        expect(editFormDisplay).to.eq('block');
        expect(createFormDisplay).to.eq('none');
    });

    it(`loads correct information`, async () => {
        await page.route('**/jsonstore/collections/books*', request => {
            request.fulfill(makeRequest(mockDataJson))
        });

        await page.route('**/jsonstore/collections/books/*', request => {
            request.fulfill(makeRequest({ title: 'title', author: 'author' }))
        });

        await page.goto('http://localhost:5500/02.Book-Library/');

        await page.click('#loadBooks');
        await page.click('.editBtn:nth-child(1)');

        const [response] = await Promise.all([
            page.waitForResponse(r => r.request().url()
                .includes('/jsonstore/collections/books/')),
            page.click('text="Save"')
        ]);

        const data = JSON.parse(await response.body());

        expect(data.title).to.eq('title');
        expect(data.author).to.eq('author');
    });

    it(`sends correct request`, async () => {
        await page.route('**/jsonstore/collections/books*', request => {
            request.fulfill(makeRequest(mockDataJson))
        });

        await page.route(
            '**/jsonstore/collections/books/*',
            request => request.fulfill(makeRequest({
                'title': 'title',
                'author': 'author'
            }))
        );

        await page.goto('http://localhost:5500/02.Book-Library/');

        await page.click('#loadBooks');
        await page.click('.editBtn:nth-child(1)');

        const [response] = await Promise.all([
            page.waitForResponse(r => r.request().url()
                .includes('/jsonstore/collections/books/') && r.request().method() === 'PUT'),
            page.click('text="Save"')
        ]);

        const data = JSON.parse(await response.body());
        expect(data).to.deep
            .eq({
                title: 'title',
                author: 'author'
            });
    });
});