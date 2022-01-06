import { login } from "../api/api.js";


let loginDiv = undefined;
let mainContainer = undefined;
let loginContext = undefined;



function initialize(domElement, mainElement){
    loginDiv = domElement;
    mainContainer = mainElement;
    let form = loginDiv.querySelector('form');
    form.addEventListener('submit', onSubmit);
}

async function showLoginPage(context) {
    loginContext = context;
    // Which is better to do ???????????
    let signUpLink = loginDiv.querySelector('#sign-up-link');
    signUpLink.addEventListener('click', loginContext.onNavigate);

    context.showSection(loginDiv,mainContainer);
}

async function onSubmit(event){
    event.preventDefault();
    const form = new FormData(event.target);
    const email = form.get('email').trim();
    const password = form.get('password').trim();
    if (email == '' || password == ""){
       return alert("Invalid username or password")
    };
    event.target.reset()
    let response = await login(email, password);
    loginContext.customizeNavigationBar()
    loginContext.redirectTo("dashboard")
}

const loginView = {
    initialize,
    showLoginPage
}
export default loginView

