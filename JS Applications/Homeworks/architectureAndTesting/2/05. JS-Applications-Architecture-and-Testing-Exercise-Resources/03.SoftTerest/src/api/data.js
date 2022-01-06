import * as api from "./api.js"

export const login = api.login;
export const register = api.register;
export const logout = api.logout

export async function getAllIdeas() {
    return api.getRequest('/data/ideas?select=_id%2Ctitle%2Cimg&sortBy=_createdOn%20desc');
};

export async function getIdeaById(id) {
    return api.getRequest('/data/ideas/' + id);
};

export async function createIdea(ideaObject) {
    return api.postRequest('/data/ideas', ideaObject);
};

export async function deleteIdea(id) {
    return api.deleteRequest('/data/ideas/' + id)
}