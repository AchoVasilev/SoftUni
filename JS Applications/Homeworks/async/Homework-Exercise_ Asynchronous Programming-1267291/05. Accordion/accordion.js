async function solution() {

    function onClick(ev) {
        if (ev.target.textContent == 'More') {
            ev.target.parentElement.parentElement.querySelector('.extra').style.display = 'block';
            ev.target.textContent = 'Less';
        }
        else {
            ev.target.parentElement.parentElement.querySelector('.extra').style.display = 'none';
            ev.target.textContent = 'More';

        }
    }

    let main = document.getElementById('main');
    let url = 'http://localhost:3030/jsonstore/advanced/articles/list';
    const res = await fetch(url);
    const data =  await res.json();
    const detailsRequest = await fetch('http://localhost:3030/jsonstore/advanced/articles/details');
    const detailsData = await detailsRequest.json();

    for (let object of data) {
        let detail;

        for (let [key, value] of Object.entries(detailsData)) {
            if (key == object._id) {
                detail = value.content;
            }
        }

        main.innerHTML += `<div class="accordion">
        <div class="head">
        <span>${object.title}</span>
        <button class="button" id="${object._id}">More</button>
        </div>
        <div class="extra">
        <p>${detail}</p>
        </div>
        </div>`;
    }

    const buttonsList = document.getElementsByClassName('button');

    for (let button of buttonsList) {
        button.addEventListener('click', onClick);
    }
}

window.onload = solution();