function extensibleObject() {
    let protoObj = {};
    let extObj = Object.create(protoObj);

    extObj.extend = function(templateObj) {
        for (const key in templateObj) {
            if (typeof templateObj[key] === 'function') {
                let proto = Object.getPrototypeOf(this);
                proto[key] = templateObj[key];
            } else {
                this[key] = templateObj[key];
            }
        }
    }

    return extObj;
}