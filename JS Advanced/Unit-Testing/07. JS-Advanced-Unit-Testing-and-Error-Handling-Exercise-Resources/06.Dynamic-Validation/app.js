function validate() {
    let emailInputElement = document.getElementById('email');

    function validateEmail(email) {
        let emailRegExp = /^[a-z]+@[a-z]+\.[a-z]+/g;

        if (!emailRegExp.test(email)) {
            return false;
        }

        return true;
    }

    function applyStyle(element, email) {
        element.className = validateEmail(email) ? '' : 'error';
    }

    // @ts-ignore
    emailInputElement.addEventListener('change', ev => applyStyle(ev.target, ev.target.value));
}