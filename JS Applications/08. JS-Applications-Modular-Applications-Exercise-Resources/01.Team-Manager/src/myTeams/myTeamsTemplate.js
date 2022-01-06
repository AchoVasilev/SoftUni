import { html } from '../lib.js';

export const myTeamsTemplate = (myTeams) => html`
<section id="my-teams">

    <article class="pad-med">
        <h1>My Teams</h1>
    </article>

    ${!myTeams ? html`<article class="layout narrow">
        <div class="pad-med">
            <p>You are not a member of any team yet.</p>
            <p><a href="/browse-teams">Browse all teams</a> to join one, or use the button bellow to cerate your own
                team.</p>
        </div>
        <div class=""><a href="#" class="action cta">Create Team</a></div>
    </article>` : myTeams.map(t => teamTemplate(t.team, t.membersCount))}

</section>`;

const teamTemplate = (team, count) => html`
    <article class="layout">
        <img src=${team.imageUrl} class="team-logo left-col">
        <div class="tm-preview">
            <h2>${team.name}</h2>
            <p>${team.description}</p>
            <span class="details">${count} Members</span>
            <div><a href="/details/${team._id}" class="action">See details</a></div>
        </div>
    </article>`;