function extract(content) {
    let parenthesis = document.getElementById(content).textContent;

    let pattern = /\(([^)]+)\)/g;

    let result = [];

    let match = pattern.exec(parenthesis);

    while(match) {
        result.push(match[1]);

        match = pattern.exec(parenthesis);
    }

    return result.join('; ');
}