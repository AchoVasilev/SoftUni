
const host = 'http://localhost:3030';


async function makeRequest(url, options) {
    try {
        let response = await fetch(host + url, options);

        if (response.ok == false) {
            if (response.status == 403) {
                sessionStorage.removeItem("userData");
            }
            const error = response.json()
            throw new Error(`${error.status} ${error.message}`);
        }
        if (response.status == 204) {
            return response;
        }

        return response.json();
    } catch (error) {
        alert(error.message);
        throw error;
    }
}

function createOptions(method = "GET", data) {
    const options = {
        method,
        headers: {},
    };

    if (data) {
        options.headers["Content-Type"] = "application/json";
        options.body = JSON.stringify(data);
    };
    const userData = JSON.parse(sessionStorage.getItem("userData"));
    if (userData) {
        options.headers["X-Authorization"] = userData.token;
    };

    return options
}

export async function getRequest(url) {
    return makeRequest(url, createOptions());
}

export async function postRequest(url, data) {
    return makeRequest(url, createOptions("POST", data));
}

export async function putRequest(url, data) {
    return makeRequest(url, createOptions("PUT", data));
}

export async function deleteRequest(url) {
    return makeRequest(url, createOptions("DELETE"));
}

export async function login(email, password) {
    const response = await postRequest('/users/login', { email, password });
    const userData = {
        email: response.email,
        id: response._id,
        token: response.accessToken
    }
    sessionStorage.setItem("userData", JSON.stringify(userData))
}

export async function register(email, password) {
    const response = await postRequest('/users/register', { email, password });
    const userData = {
        email: response.email,
        id: response._id,
        token: response.accessToken
    }
    sessionStorage.setItem("userData", JSON.stringify(userData))
}

export async function logout() {
    const response = await getRequest('/users/logout');
    sessionStorage.removeItem('userData')
}