
let homeDiv = undefined;
let mainContainer = undefined;
let homeContext = undefined;


function initialize(domElement, mainElement){
    homeDiv = domElement;
    mainContainer = mainElement;

    let getStartedLink = homeDiv.querySelector('#get-started-link');
    getStartedLink.addEventListener('click', onRedirect);
    homeDiv.remove();
}
async function showHomePage(context) {
    homeContext = context
    context.showSection(homeDiv,mainContainer)
}

function onRedirect(event){
    event.preventDefault();
    homeContext.redirectTo('dashboard');  
}

const homeView = {
    initialize,
    showHomePage
}
export default homeView