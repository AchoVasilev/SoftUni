window.addEventListener('load', solve);
const url = "http://localhost:3030/jsonstore/collections/students";

const table = document.querySelector('#results tbody');
function solve() {
    const formElement = document.querySelector('form');
    formElement.addEventListener('submit', addStudent.bind(null, formElement));
    getStudents();
}

async function addStudent(formElement, ev) {
    ev.preventDefault();
    const data = new FormData(formElement);
        if ([...data.entries()].some(f => f[1] == '')) {
            alert('Please enter information for all fields!');
        }
        table.textContent = 'Loading ...';

        const student = {};
        [...data.entries()].forEach(f => student[f[0]] = f[1]);

        const options = {
            method: 'post',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(student)
        }

        const res = await fetch(url, options);
        const dataResult = await res.json();

        formElement.reset();
        getStudents();
}


async function getStudents() {
    table.textContent = 'Loading ...';

    const res = await fetch(url);
    const data = await res.json();
    table.replaceChildren();
    Object.values(data).forEach(s => {
        const tr = e('tr', {},
            e('td', {}, s.firstName),
            e('td', {}, s.lastName),
            e('td', {}, s.facultyNumber),
            e('td', {}, Number(s.grade).toFixed(2)),
        );
        table.appendChild(tr);
    })
}


function e(type, attr, ...content) {
    const element = document.createElement(type);
    for (let prop in attr) {
        element[prop] = attr[prop];
    }
    for (let item of content) {
        if (typeof item == 'string' || typeof item == 'number') {
            item = document.createTextNode(item);
        }
        element.appendChild(item);
    }
    return element;
}