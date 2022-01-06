import { getAllMembers, getAllTeams } from '../api/data.js';
import { browseTeamsTemplate } from './browseTeamsTemplate.js';

let _router = undefined;
let _renderHandler = undefined;

function initialize(router, renderHandler) {
    _router = router;
    _renderHandler = renderHandler;
}

async function getView(context) {
    const teams = await getAllTeams();
    const members = await getAllMembers();

    teams.forEach(t => t.membersCount = members.filter(m => m.teamId === t._id).length);

    const viewModel = {
        teams: teams,
        isLoggedIn: context.user != null
    };

    const template = browseTeamsTemplate(viewModel);
    _renderHandler(template);
}

export default {
    initialize,
    getView
};