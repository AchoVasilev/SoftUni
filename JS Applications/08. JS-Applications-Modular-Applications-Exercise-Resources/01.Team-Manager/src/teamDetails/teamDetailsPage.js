import { getById, getAllMembers, getAllMemberships } from '../api/data.js';
import { teamDetailsTemplate } from './teamDetailsTemplate.js';

let _router = undefined;
let _renderHandler = undefined;

function initialize(router, renderHandler) {
    _router = router;
    _renderHandler = renderHandler;
}

async function getView(context) {
    const teamId = context.params.id;

    const [team, allMemberships] = await Promise.all([
        getById(teamId),
        getAllMemberships(teamId)
    ]);

    const user = context.user;

    team.members = allMemberships.filter(m => m.status == 'member');
    team.membersCount = team.members.length;
    team.requests = allMemberships.filter(m => m.status == 'pending');

    if (user == null) {
        team.userStatus = 'guest';
    } else if (user.id == team._ownerId) {
        team.userStatus = 'owner';
    } else {
        const teamUser = allMemberships.find(m => m._id == user.id);

        if (teamUser == undefined) {
            team.userStatus = 'nonMember';
        } else if (team.members.find(m => m._id == teamUser._id)) {
            team.userStatus = 'member';
        } else {
            team.userStatus = 'pending';
        }
    }

    const template = teamDetailsTemplate(team);
    _renderHandler(template);
}

export default {
    initialize,
    getView
};