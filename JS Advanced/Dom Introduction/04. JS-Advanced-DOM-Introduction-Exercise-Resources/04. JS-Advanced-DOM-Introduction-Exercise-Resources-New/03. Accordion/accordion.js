function toggle() {
    let buttonElement = document.getElementsByClassName('button')[0];
    let divElement = document.getElementById('extra');

    if (buttonElement.innerHTML === 'More') {
        buttonElement.innerHTML = 'Less';
        divElement.style.display = 'block';
    } else {
        buttonElement.innerHTML = 'More';
        divElement.style.display = 'none';
    }
}