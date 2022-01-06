window.addEventListener('load', solve);

function solve() {
    const modelElement = document.getElementById('model');
    const yearElement = document.getElementById('year');
    const descriptionElement = document.getElementById('description');
    const priceElement = document.getElementById('price');

    const addBtnElement = document.getElementById('add');
    const furnitureList = document.getElementById('furniture-list');
    const totalPriceElement = document.getElementsByClassName('total-price')[0];

    addBtnElement.addEventListener('click', add);

    function add(ev) {
        ev.preventDefault();

        // @ts-ignore
        const model = modelElement.value.trim();
        // @ts-ignore
        const year = yearElement.value.trim();
        // @ts-ignore
        const description = descriptionElement.value.trim();
        // @ts-ignore
        const price = Number(priceElement.value.trim());

        if (model == '' || year < 0 || year == '' || description == '' || price < 0) {
            return;
        }

        let infoTr = document.createElement('tr');
        infoTr.classList.add('info');

        let furnitureTd = document.createElement('td');
        furnitureTd.textContent = model;

        let priceTd = document.createElement('td');
        priceTd.textContent = price.toFixed(2);

        let buttonsTd = document.createElement('td');

        let moreBtn = document.createElement('button');
        moreBtn.classList.add('moreBtn');
        moreBtn.textContent = 'More Info';
        moreBtn.addEventListener('click', getInfo);

        let buyBtn = document.createElement('button');
        buyBtn.classList.add('buyBtn');
        buyBtn.textContent = 'Buy it';

        buyBtn.addEventListener('click', buy);

        buttonsTd.appendChild(moreBtn);
        buttonsTd.appendChild(buyBtn);

        infoTr.appendChild(furnitureTd);
        infoTr.appendChild(priceTd);
        infoTr.appendChild(buttonsTd);

        let hideTr = document.createElement('tr');
        hideTr.classList.add('hide');
        let yearTd = document.createElement('td');
        yearTd.textContent = `Year: ${year}`;

        let descriptionTd = document.createElement('td');
        descriptionTd.colSpan = 3;
        descriptionTd.textContent = `Description: ${description}`;

        hideTr.appendChild(yearTd);
        hideTr.appendChild(descriptionTd);

        furnitureList.appendChild(infoTr);
        furnitureList.appendChild(hideTr);

        // @ts-ignore
        modelElement.value = '';
        // @ts-ignore
        yearElement.value = '';
        // @ts-ignore
        descriptionElement.value = '';
        // @ts-ignore
        priceElement.value = '';
    }

    function getInfo(e) {
        let btn = e.target;
        let trClass = e.target.parentElement.parentElement.nextElementSibling;

        if (btn.textContent == 'More Info') {
            btn.textContent = 'Less Info';
            trClass.style.display = 'contents';
        } else {
            btn.textContent = 'More Info';
            trClass.style.display = 'none';
        }
    }

    function buy(ev) {
        // @ts-ignore
        let totalPrice = Number(totalPriceElement.textContent);

        let tableRow = ev.target.parentElement.parentElement;
        let hideTr = tableRow.nextElementSibling;

        let price = Number(tableRow.querySelectorAll('td')[1].textContent);

        totalPrice += price;
        let infoTbody = tableRow.parentElement;

        infoTbody.removeChild(tableRow);
        infoTbody.removeChild(hideTr);

        totalPriceElement.textContent = totalPrice.toFixed(2);
    }
}
