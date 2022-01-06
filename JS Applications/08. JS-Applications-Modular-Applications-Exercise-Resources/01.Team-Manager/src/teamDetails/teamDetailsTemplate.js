import { html } from '../lib.js';

export const teamDetailsTemplate = (team) => html`
<section id="team-home">
    <article class="layout">
        <img src=${team.logoUrl} class="team-logo left-col">
        <div class="tm-preview">
            <h2>${team.name}</h2>
            <p>${team.description}</p>
            <span class="details">${team.membersCount} Members</span>
            <div>
                ${team.userStatus == 'owner' ? html`<a href="/edit/${team._id}" class="action">Edit
                    team</a>` : null}
                ${team.userStatus == 'nonMember' ? html`<a href="javascript:void(0)" class="action">Join team</a>`
        : null}
                ${team.userStatus == 'member' ? html`<a href="javascript:void(0)" class="action invert">Leave
                    team</a>` : null}
                ${team.userStatus == 'pending' ? html`Membership pending. <a href="javascript:void(0)">Cancel
                    request</a>` : null}
            </div>
        </div>
        <div class="pad-large">
            <h3>Members</h3>
            <ul class="tm-members">
                <li>My Username</li>
                ${team.members.map(m => memberTemplate(m, team.userStatus))}
            </ul>
        </div>
        <div class="pad-large">
            ${team.userStatus == 'owner' ? html`<h3>Membership Requests</h3>
            <ul class="tm-members">
                ${team.requests.map(m => requestsTemplate(m))}
            </ul>` : null}
        </div>
    </article>
</section>`;

const memberTemplate = (member, status) => html`
<li>${member.user.username}
    ${status == 'owner' ? html`<a href="javascript:void(0)" class="tm-control action">Remove from team</a></li>` : null}`;

const requestsTemplate = (member) => html`
<li>${member.user.username}
    <a href="javascript:void(0)" class="tm-control action">Approve</a><a href="javascript:void(0)"
        class="tm-control action">Decline</a></li>`;