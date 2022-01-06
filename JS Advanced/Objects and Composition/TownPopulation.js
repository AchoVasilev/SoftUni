function solve(inputArr) {
    let towns = {};

    for (const args of inputArr) {
        let tokens = args.split(' <-> ');
        const name = tokens[0];
        let population = Number(tokens[1]);

        if (towns[name]) {
            population += towns[name]
        }

        towns[name] = population;
    }

    for (const [name, pop] of Object.entries(towns)) {
        console.log(`${name} : ${pop}`);
    }
}

solve(['Sofia <-> 1200000',
'Montana <-> 20000',
'New York <-> 10000000',
'Washington <-> 2345000',
'Las Vegas <-> 1000000']
);