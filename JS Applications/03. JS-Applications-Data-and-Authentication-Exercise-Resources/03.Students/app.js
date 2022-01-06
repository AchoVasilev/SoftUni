function attachEvents() {
    displayStudents();

    document.addEventListener('submit', onSubmit);
}

attachEvents();

async function onSubmit(ev) {
    ev.preventDefault();
    const form = new FormData(ev.target);

    const data = [...form.entries()];

    if (validateInputs(data) == false) {
        alert('Invalid input');
        return;
    }

    await submitStudent(data);
    displayStudents()

    let fields = [...document.querySelectorAll('#form div.inputs input[type="text"]')];

    clearFields(fields);
}

function loadStudents() {

}

async function displayStudents() {
    const tableBody = document.querySelector('#results > tbody');
    tableBody.replaceChildren();

    const students = await getStudents();

    Object.values(students)
        .forEach(student => {
            const tr = document.createElement('tr');

            Object.entries(student)
                .forEach(([key, value]) => {
                    if (key == '_id') {
                        return;
                    }

                    const td = document.createElement('td');
                    td.textContent = value;
                    tr.appendChild(td);
                });

            tableBody.appendChild(tr);
        });
}

async function getStudents() {
    const url = 'http://localhost:3030/jsonstore/collections/students';
    const response = await fetch(url);
    const data = await response.json();

    return data;
}

async function submitStudent(data) {
    const url = 'http://localhost:3030/jsonstore/collections/students';
    const response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(Object.fromEntries(data))
    });

    const result = await response.json();

    return result;
}

function validateInputs(data) {
    for (const [key, value] of Object.values(data)) {
        if (value == '') {
            return false;
        }

        if (key == 'facultyNumber' || key == 'grade') {
            let num = Number(value);

            if (isNaN(num)) {
                return false;
            }
        }
    }

    return true;
}

function clearFields(data) {
    return data.forEach(x => x.value = '');
}