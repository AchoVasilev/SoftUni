function solve(...params) {
    let occurences = {};
    let result = [];

    params.forEach(el => {
        let type = typeof (el);

        result.push(`${type}: ${el}`);

        occurences[type] = occurences[type] !== undefined ? ++occurences[type] : occurences[type] = 1;
    });

    Object.keys(occurences)
        .sort((a, b) => occurences[b] - occurences[a])
        .forEach(el => result.push(`${el} = ${occurences[el]}`));
    
    return result.join('\n');
}