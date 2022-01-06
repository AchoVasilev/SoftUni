import * as api from './api.js';

export const login = api.login;
export const register = api.register;
export const logout = api.logout;

const endpoints = {
    allTeams: '/data/teams',
    byId: '/data/teams/',
    allMembers: '/data/members?where=status%3D%22member%22',
    allMemberships: (teamId) => `/data/members?where=teamId%3D%22${teamId}%22&load=user%3D_ownerId%3Ausers`,
    myMemberships: (userId) => `/data/members?where=_ownerId%3D%22${userId}%22%20AND%20status%3D%22member%22&load=team%3DteamId%3Ateams`,
    create: '/data/teams',
    edit: '/data/teams/',
    del: '/data/teams/',
    addMember: '/data/members',
    approveMember: (memberId) => `/data/members/${memberId}`
};

export async function approveMemberRequest(memberId, data) {
    return api.put(endpoints.approveMember(memberId), data);
}

export async function makeMemberRequest(data) {
    return api.post(endpoints.addMember, data);
}

export async function getAllTeams() {
    return api.get(endpoints.allTeams);
}

export async function getAllMembers() {
    return api.get(endpoints.allMembers);
}

export async function getMyMemberships(userId) {
    return api.get(endpoints.myMemberships(userId));
}

export async function getById(id) {
    return api.get(endpoints.byId + id);
}

export async function getAllMemberships(teamId) {
    return api.get(endpoints.allMemberships(teamId));
}

export async function createItem(data) {
    return api.post(endpoints.create, data);
}

export async function editItem(id, data) {
    return api.put(endpoints.edit + id, data);
}

export async function deleteItem(id) {
    return api.del(endpoints.del + id);
}