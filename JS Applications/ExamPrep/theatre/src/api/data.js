import * as api from './api.js';

export const login = api.login;
export const register = api.register;
export const logout = api.logout;

export async function getAllTheaters() {
    return api.get('/data/theaters?sortBy=_createdOn%20desc&distinct=title');
}

export async function createTheatre(data) {
    return api.post('/data/theaters', data);
}

export async function getTheatreById(id) {
    return api.get('/data/theaters/' + id);
}

export async function likeTheatre(theaterId) {
    return api.post('/data/likes', { theaterId });
}

export async function getMyLikeByTheaterId(userId, theaterId) {
    return api.get(`/data/likes?where=theaterId%3D%22${theaterId}%22%20and%20_ownerId%3D%22${userId}%22&count`);
}

export async function getTheatreLikes(theaterId) {
    return api.get(`/data/likes?where=theaterId%3D%22${theaterId}%22&distinct=_ownerId&count`);
}

export async function editTheatreById(id, data) {
    return api.put('/data/theaters/' + id, data);
}

export async function deleteTheatreById(id) {
    return api.del('/data/theaters/' + id);
}

export async function getMyTheaters(userId) {
    return api.get(`/data/theaters?where=_ownerId%3D%22${userId}%22&sortBy=_createdOn%20desc`);
}