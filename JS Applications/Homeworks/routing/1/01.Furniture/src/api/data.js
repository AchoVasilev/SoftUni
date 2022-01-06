import { getUserData } from '../util.js';
import * as api from './api.js';

export const login = api.login;
export const regster = api.register;
export const logout = api.logout;

const endpoints = {
    getAll: '/data/catalog',
    createFurniture: '/data/catalog',
    getById: (id) => `/data/catalog/${id}`,
    getUserFurniture: (id) => `/data/catalog?where=_ownerId%3D%22${id}%22`
};

export async function getAllFurniture() {
    return api.get(endpoints.getAll);
}

export async function createFurniture(furniture) {
    return api.post(endpoints.createFurniture, furniture);
}

export async function getFurnitureById(id) {
    return api.get(endpoints.getById(id));
}

export async function editFurniture(id, furniture) {
    return api.put(endpoints.getById(id), furniture);
}

export async function deleteFurniture(id) {
    return api.del(endpoints.getById(id));
}

export async function getMyFurniture() {
    const userId = getUserData().id;
    return api.get(endpoints.getUserFurniture(userId));
}