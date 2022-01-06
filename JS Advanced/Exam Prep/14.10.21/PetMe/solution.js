function solve() {
    let inputFields = document.querySelectorAll('#container input');

    let addBtn = document.querySelector('#container button');

    let petList = document.querySelector('#adoption ul');

    let adoptedList = document.querySelector('#adopted ul');

    let input = {
        name: inputFields[0],
        age: inputFields[1],
        kind: inputFields[2],
        owner: inputFields[3]
    };

    addBtn.addEventListener('click', addPet);

    function addPet(ev) {
        ev.preventDefault();

        // @ts-ignore
        const name = input.name.value.trim();
        // @ts-ignore
        const age = Number(input.age.value.trim());
        // @ts-ignore
        const kind = input.kind.value.trim();
        // @ts-ignore
        const owner = input.owner.value.trim();

        // @ts-ignore
        if (name == '' || input.name.value.trim() == '' || Number.isNaN(age) || kind == '' || owner == '') {
            return;
        }

        const contactBtn = e('button', {}, 'Contact with owner');
        const pet = e('li', {},
            e('p', {},
                e('strong', {}, name),
                ' is a ',
                e('strong', {}, age),
                ' year old ',
                e('strong', {}, kind)
            ),
            e('span', {}, `Owner: ${owner}`),
            contactBtn
        );

        contactBtn.addEventListener('click', contact);

        petList.appendChild(pet);
        
        // @ts-ignore
        Array.from(inputFields).forEach(f => f.value = '');
        
        function contact() {
            let confirmationInput = e('input', {placeholder: 'Enter your names'});
            let confirmationBtn = e('button', {}, 'Yes! I take it!');
            const confirmationDiv = e('div', {}, 
                confirmationInput,
                confirmationBtn
            );

            confirmationBtn.addEventListener('click', adopt.bind(null, confirmationInput, pet));

            contactBtn.remove();

            pet.appendChild(confirmationDiv);
        }
    }

    function adopt(input, pet) {
        let newOwner = input.value.trim();

        if (newOwner == '') {
            return;
        }

        const checkBtn = e('button', {}, 'Checked');
        checkBtn.addEventListener('click', check.bind(null, pet));

        pet.querySelector('div').remove();
        pet.appendChild(checkBtn);

        pet.querySelector('span').textContent = `New Owner: ${newOwner}`;

        adoptedList.appendChild(pet);
    }

    function check(pet) {
        pet.remove();
    }

    function e(type, attribute, ...content) {
        const element = document.createElement(type);

        for (let prop in attribute) {
            element[prop] = attribute[prop];
        }

        for (let item of content) {
            if (typeof (item) == 'string' || typeof (item) == 'number') {
                // @ts-ignore
                item = document.createTextNode(item);
            }

            element.appendChild(item);
        }

        return element;
    }
}