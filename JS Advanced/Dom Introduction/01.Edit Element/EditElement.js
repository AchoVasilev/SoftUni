function editElement(htmlElement, match, replace) {
    let content = htmlElement.textContent;

    let matcher = new RegExp(match, 'g');

    let edited = content.replace(matcher, replace);

    htmlElement.textContent = edited;
}