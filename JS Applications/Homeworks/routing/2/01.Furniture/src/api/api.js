import { getUserData, setUserData, clearUserData } from "../util.js";

const host = 'http://localhost:3030';

async function request(url, options) {

    try {
        const response = await fetch(host + url, options);

        if (response.ok != true) {
            if (response.status == 403) {
                clearUserData();
            }
            const error = await response.json();
            throw new Error(error.message);
        }

        if (response.status == 204) {

            return response;
        } else {
            return response.json();
        }



    } catch (err) {

        alert(err.message);
        throw err;

    }

}

function createOptions(method = 'get', data) {
    const options = {
        method,
        headers: {}
    };
    if (data != undefined) {

        options.headers['Content-Type'] = 'application/json';
        options.body = JSON.stringify(data);
    }

    const userData = getUserData();

    if (userData != null) {
        options.headers['X-Authorization'] = userData.token;

    }

    return options;
}

export async function get(url) {
    return request(url, createOptions());
}
export async function post(url, data) {
    return request(url, createOptions('post', data));
}
export async function put(url, data) {
    return request(url, createOptions('put', data));
}
export async function del(url) {
    return request(url, createOptions('delete'));
}

export async function login(email, password) {
    const result = await post('/users/login', { email, password });
    const userData = {
        email: result.email,
        id: result._id,
        token: result.accessToken
    }
    setUserData(userData);
}

export async function register(email, password) {
    const result = await post('/users/register', { email, password });
    const userData = {
        email: result.email,
        id: result._id,
        token: result.accessToken
    }
    setUserData(userData);
}

export async function logout() {
    await get('/users/logout');
    clearUserData('userData');
}

export function validateData(data, missing) {
    let errors = {};
    let error = [];
    if (missing.length > 0) {
        errors = missing.reduce((a, [k]) => Object.assign(a, { [k]: 'is-invalid' }), {})
        throw {
            error: new Error('Fill all required fields'),
            errors
        };
    }

    data.year = Number(data.year);
    data.price = Number(data.price);

    if (data.make.length < 4) {

        Object.assign(errors, { make: "is-invalid" });
        error.push('Make must have at least 4 symbols!');
    } else {
        Object.assign(errors, { make: "is-valid" });
    }




    if (data.model.length < 4) {

        error.push('Model must have at least 4 symbols!');
        Object.assign(errors, { model: "is-invalid" });
    } else {
        Object.assign(errors, { model: "is-valid" });
    }


    if (data.year < 1950 || data.year > 2050) {
        error.push('Year must be between 1950 and 2050!');
        Object.assign(errors, { year: "is-invalid" });
    } else {
        Object.assign(errors, { year: "is-valid" });
    }

    if (data.description.length < 10) {
        error.push('Description must be more than 10 symbols long!');
        Object.assign(errors, { description: "is-invalid" });
    } else {
        Object.assign(errors, { description: "is-valid" });
    }

    if (data.price < 0) {
        error.push('Price must be a positive number!');
        Object.assign(errors, { price: "is-invalid" });
    } else {
        Object.assign(errors, { price: "is-valid" });
    }

    if (error.length > 0) {
        const allErrors = error.join("\n");
        throw {
            error: new Error(allErrors),
            errors

        }
    }
}
