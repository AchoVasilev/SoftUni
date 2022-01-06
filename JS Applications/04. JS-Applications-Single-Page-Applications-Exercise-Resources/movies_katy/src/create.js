import { createDetails } from "./details.js";
import { showView } from "./dom.js";
import { showHome } from "./home.js";

const section = document.getElementById('add-movie');
section.addEventListener('submit', onCreate);
section.remove();

//display logic

export function showCreate() {
    showView(section)
}
// --------------------------------------------------------------
async function onCreate(e) {
    e.preventDefault();

    const userData = JSON.parse(sessionStorage.getItem('userData'));

    if (userData == null) {
        return
    }

    const formData = new FormData(e.target);

    let title = formData.get('title');
    let description = formData.get('description');
    let imageUrl = formData.get('imageUrl');

    if (title == '' || description == '' || imageUrl == '') {
        if (title = '') {
            alert('Add movie title!')
            throw new Error();
        } else if (description == '') {
            alert('Add movie description!')
            throw new Error();
        } else if (imageUrl == '') {
            alert('Add image url!')
            throw new Error();
        }
        showCreate()
    }
    
    const result = await createMovie({ title, description, imageUrl });

    e.target.reset();

    showHome();
}

async function createMovie(data) {

        let userData = JSON.parse(sessionStorage.getItem('userData'));
    
        let token = userData.token
    
        console.log(token)
    
        const response = await fetch('http://localhost:3030/data/movies', {
            method: 'post',
            headers: {
                'Content-Type': 'application/json',
                'X-Authorization': token
    
            },
            body: JSON.stringify(data)
        });
    
        return response
    }