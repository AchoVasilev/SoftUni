import { register } from "../api/api.js";

let registerDiv = undefined;
let mainContainer = undefined;
let registerContext = undefined


function initialize(domElement, mainElement){
    registerDiv = domElement;
    mainContainer = mainElement;
    let form = registerDiv.querySelector('form');
    form.addEventListener('submit', onSubmit);
}

async function showRegisterPage(context) {
    registerContext = context
     // Which is better to do ???????????
    let signInLink = registerDiv.querySelector('#sign-in-link');
    signInLink.addEventListener('click', registerContext.onNavigate);

    context.showSection(registerDiv,mainContainer)
}

async function onSubmit(event){
    event.preventDefault();
    const form = new FormData(event.target);
    const email = form.get('email').trim();
    const password = form.get('password').trim();
    const repeatPassword = form.get("repeatPassword").trim();

    if (email.length < 3 || email.match(/^(?=.{3,}$)(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*\W).*$/)){
        return alert("The email should be at least 3 characters long, have digits and special characters");
    };
    if (password.length < 3) {
        return alert('The password should be at least 3 characters long');
    };
    if (repeatPassword != password) {
        return alert("The repeat password should be equal to the password");
    };
    
    event.target.reset()
    let response = await register(email, password);
    registerContext.customizeNavigationBar()
    registerContext.redirectTo("dashboard")
}

const registerView = {
    initialize,
    showRegisterPage
}
export default registerView
