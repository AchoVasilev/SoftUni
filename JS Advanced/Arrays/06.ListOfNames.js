function solve(names) {
    return names.sort((a, b) => a.localeCompare(b))
                .forEach((x, i) => console.log(`${i + 1}.${x}`));
}

