function solve(inputArr) {
    const parsers = str => 
        str.split('|')
            .filter(x => x !== '')
            .map(x => x.trim())
            .map(x => (isNaN(x) ? x : Number(Number(x).toFixed(2))));

    const headings = parsers(inputArr[0]);

    return JSON.stringify(
        inputArr.slice(1)
                .map(x => {
                    const line = parsers(x);
                    return headings.reduce((row, heading, entry) => {
                        row[heading] = line[entry];

                        return row;
                    }, {})
                })
    )
}