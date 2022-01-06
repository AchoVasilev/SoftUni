function calculator() {
    let html = {
        s1: '',
        s2: '',
        output: ''
    };

    function calculate(a, b, sign) {
        let signs = {
            '+': (a, b) => a + b,
            '-': (a, b) => a - b
        };

        return signs[sign](Number(a), Number(b));
    }

    return {
        init: (firstSelector, secondSelector, outputSelector) => {
            html.s1 = document.querySelector(firstSelector);
            html.s2 = document.querySelector(secondSelector);
            html.output = document.querySelector(outputSelector);
        },
        // @ts-ignore
        add: () => (html.output.value = calculate(html.s1.value, html.s2.value, '+')),
        // @ts-ignore
        subtract: () => (html.output.value = calculate(html.s1.value, html.s2.value, '-'))
    };
}




