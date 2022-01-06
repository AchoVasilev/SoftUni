function solution() {
    let firstName = document.querySelector('input[name="firstName"]');
    let lastName = document.querySelector('input[name="lastName"]');
    let facultyNumberInput = document.querySelector('input[name="facultyNumber"]');
    let gradeInput = document.querySelector('input[name="grade"]');


    let url = 'http://localhost:3030/jsonstore/collections/students';
 

    let submitButton = document.querySelector('#submit');
    submitButton.addEventListener('click', (e) => {
        e.preventDefault();
 

        let studentBody = {
            firstName: firstName.value,
            lastName: lastName.value,
            facultyNumber: facultyNumberInput.value,
            grade: Number(gradeInput.value),
        };
 
        fetch(url, {
            method: 'post',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(studentBody),
        })
            .then(r => loadTableContents())
            .catch(err => console.log(err));
    })
 
    function loadTableContents() {
        let tableBody = document.querySelector('#results tbody');
 
        while (tableBody.firstChild) {
            tableBody.firstChild.remove();
        }
 
        fetch(url)
            .then(res => res.json())
            .then(students => {
                Object.values(students)
                    .forEach(student => {
                        let tr = document.createElement('tr');
 
                        let firstName = document.createElement('td');
                        firstName.textContent = student.firstName;
                        tr.appendChild(firstName);
 
                        let lastName = document.createElement('td');
                        lastName.textContent = student.lastName;
                        tr.appendChild(lastName);
 
                        let facultyNumber = document.createElement('td');
                        facultyNumber.textContent = student.facultyNumber;
                        tr.appendChild(facultyNumber);
 
                        let grade = document.createElement('td');
                        grade.textContent = student.grade;
                        tr.appendChild(grade);
 
                        tableBody.appendChild(tr);
                    })
            })
            .catch(err => console.log(err))
    }
}
solution();