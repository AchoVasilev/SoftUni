function solve() {
    let str = '';

    return {
        append: v => (str += v),
        removeStart: v => (str = str.slice(v)),
        removeEnd: v => (str = str.slice(0, -v)),
        print: () => console.log(str)
    };
}