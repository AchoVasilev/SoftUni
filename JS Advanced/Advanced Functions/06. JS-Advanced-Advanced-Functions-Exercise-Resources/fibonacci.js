function getFibonator() {
    let previousElement = 1;
    let currentElement = 0;

    function inner() {
        let nextElement = previousElement + currentElement;
        previousElement = currentElement;
        currentElement = nextElement;

        return currentElement;
    }

    return inner;
}