export function createElement(type, attributes, ...content) {
    let newElement = document.createElement(type);

    for (let property in attributes) {
        newElement.setAttribute(property, attributes[property])
    }

    for (let item of content) {
        if (typeof item == 'string' || typeof item == "number") {
            item = document.createTextNode(item)
        }
        newElement.appendChild(item)
    }
    return newElement
}

export function showSection(divElement,mainView){
    mainView.replaceChildren(divElement)
}