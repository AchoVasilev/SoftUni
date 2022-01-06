import * as api from './api.js';

export const login = api.login;
export const register = api.register;
export const logout = api.logout;

export async function getAllMemes() {
    return api.get('/data/memes?sortBy=_createdOn%20desc');
}

export async function createMeme(data) {
    return api.post('/data/memes', data);
}

export async function getMeme(memeId) {
    return api.get('/data/memes/' + memeId);
}

export async function deleteMeme(memeId) {
    return api.del('/data/memes/' + memeId);
}

export async function editMeme(memeId, data) {
    return api.put('/data/memes/' + memeId, data);
}

export async function getMyMemes(userId) {
    return api.get(`/data/memes?where=_ownerId%3D%22${userId}%22&sortBy=_createdOn%20desc`);
}