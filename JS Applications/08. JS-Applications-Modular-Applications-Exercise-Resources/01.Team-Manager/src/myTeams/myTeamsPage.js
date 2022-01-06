import { getAllMembers, getAllMemberships, getMyMemberships } from '../api/data.js';
import { myTeamsTemplate } from './myTeamsTemplate.js';

let _router = undefined;
let _renderHandler = undefined;

function initialize(router, renderHandler) {
    _router = router;
    _renderHandler = renderHandler;
}

async function getView(context) {
    const user = context.user;
    const [myTeams, allMembers] = await Promise.all([
        getMyMemberships(user.id),
        getAllMembers()
    ]);

    const teams = myTeams.map(x => ({ ...x, membersCount: allMembers.filter(m => m.teamId == x.team._id).length}));

    _renderHandler(myTeamsTemplate(teams));
}

export default {
    initialize,
    getView
};