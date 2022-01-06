class Story {
    constructor(title, creator) {
        this.title = title;
        this.creator = creator;
        this._comments = [];
        this._likes = [];
    }

    get likes() {
        if (this._likes.length === 0) {
            return `${this.title} has 0 likes`;
        }

        let username = this._likes[0];
        if (this._likes.length === 1) {
            return `${username} likes this story!`;
        }

        return `${username} and ${this._likes.length - 1} others like this story!`
    }

    like(username) {
        if (this._likes.includes(username)) {
            throw new Error(`You can't like the same story twice!`);
        }

        if (username === this.creator) {
            throw new Error(`You can't like your own story!`);
        }

        this._likes.push(username);

        return `${username} liked ${this.title}!`
    }

    dislike(username) {
        if (!this._likes.includes(username)) {
            throw new Error(`You can't dislike this story!`);
        }

        this._likes = this._likes.filter(x => x !== username);

        return `${username} disliked ${this.title}`;
    }

    comment(username, content, id) {
        if (id == undefined || !this._comments.some(c => c.Id === id)) {
            let newComment = {
                Id: this._comments.length + 1,
                Username: username,
                Content: content,
                Replies: []
            };

            this._comments.push(newComment);

            return `${username} commented on ${this.title}`;
        }

        let commentToReply = this._comments.find(c => c.Id == id);

        let replyId = Number(`${commentToReply.Id}.${commentToReply.Replies.length + 1}`);
        let reply = {
            Id: replyId,
            Username: username,
            Content: content
        };

        commentToReply.Replies.push(reply);

        return 'You replied successfully';
    }

    toString(sortingType) {
        const sortings = {
            asc: (a, b) => a.Id - b.Id,
            desc: (a, b) => b.Id - a.Id,
            username: (a, b) => a.Username.localeCompare(b.Username)
        };

        let comments = this._comments.sort(sortings[sortingType]);
        comments.forEach(c => c.Replies.sort(sortings[sortingType]));

        let resultStr = [];

        for (const comment of comments) {
            let commentStr = `-- ${comment.Id}. ${comment.Username}: ${comment.Content}`;

            let replies = comment.Replies
                .map(r => `--- ${r.Id}. ${r.Username}: ${r.Content}`)
                .join('\n');

            replies = comment.Replies.length > 0 ? `\n${replies}` : '';

            let combined = `${commentStr}${replies}`;
            resultStr.push(combined);
        }

        let result = this._comments.length > 0 ? `\n${resultStr.join('\n')}` : '';

        return `Title: ${this.title}
Creator: ${this.creator}
Likes: ${this._likes.length}
Comments:${result}`;
    }
}

let art = new Story("My Story", "Anny");
art.like("John");
console.log(art.likes);
art.dislike("John");
console.log(art.likes);
art.comment("Sammy", "Some Content");
console.log(art.comment("Ammy", "New Content"));
art.comment("Zane", "Reply", 1);
art.comment("Jessy", "Nice :)");
console.log(art.comment("SAmmy", "Reply@", 1));
console.log()
console.log(art.toString('username'));
console.log()
art.like("Zane");
console.log(art.toString('desc'));
