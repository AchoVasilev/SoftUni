async function solution(){
    const table = document.querySelector('tbody');
    const form = document.getElementById('form');
    form.addEventListener('submit', onSubmit);

    const url = `http://localhost:3030/jsonstore/collections/students`;
    const res = await fetch(url);
    const data = await res.json();

    for (let obj of Object.values(data)) {
        table.innerHTML += `<tr>
        <td>${obj['firstName']}</td>
        <td>${obj['lastName']}</td>
        <td>${obj['facultyNumber']}</td>
        <td>${obj['grade']}</td>
        </tr>`;
    }

    function validateForm(form) {
        const letters = /^[A-Za-z]+$/;
        const numbers = /^\d+(\.\d+)?$/;
        const inputData = new FormData(form);

        if (!inputData.get('firstName').match(letters)){
            return false;
        } else if(!inputData.get('lastName').match(letters)){
            return false;
        } else if(!inputData.get('facultyNumber').match(numbers)){
            return false;
        } else if(!inputData.get('grade').match(numbers) || Number(inputData.get('grade')) < 2 || Number(inputData.get('grade')) > 6){
            return false;
        } else {
            return true;
        }
    }

    async function onSubmit(e) {
        e.preventDefault(); // prevents making a post request
        const formData = new FormData(e.target);

        if (validateForm(e.target)) {
            await fetch(url, {
                method: "POST",
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    "firstName": formData.get('firstName'),
                    "lastName": formData.get('lastName'),
                    "facultyNumber": formData.get('facultyNumber'),
                    "grade": formData.get('grade'),
                })
            });
            e.target.reset();
            
            const res2 = await fetch(url);
            const data2 = await res2.json();
    
            table.innerHTML = '';
            for (let obj of Object.values(data2)) {
                table.innerHTML += `<tr>
                <td>${obj['firstName']}</td>
                <td>${obj['lastName']}</td>
                <td>${obj['facultyNumber']}</td>
                <td>${obj['grade']}</td>
                </tr>`;
            }
        } else {
            alert('Invalid form data! Try again!');
            e.target.reset();
        }        
    }
}

solution();