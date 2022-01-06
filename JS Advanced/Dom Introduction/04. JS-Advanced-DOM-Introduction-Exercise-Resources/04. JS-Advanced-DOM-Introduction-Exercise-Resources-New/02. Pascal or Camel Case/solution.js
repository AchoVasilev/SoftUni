function solve() {
    let inputElement = document.getElementById('text');
    let namingConventionElement = document.getElementById('naming-convention');

    let text = inputElement.value;
    let namingConvention = namingConventionElement.value;

    let result = applyConvetion(text, namingConvention);

    let spanElement = document.getElementById('result');
    spanElement.textContent = result;

    function applyConvetion(text, convention) {
      const conventionSwitch = {
        'Pascal Case': () => text.toLowerCase()
                                 .split(' ')
                                 .map(x => x[0].toUpperCase() + x.slice(1))
                                 .join(''),
        'Camel Case': () => text.toLowerCase()
                                .split(' ')
                                .map((x, i) => x = i !== 0 ? x[0].toUpperCase() + x.slice(1) : x)
                                .join(''),
        default: () => 'Error!'
      };

      return (conventionSwitch[convention] || conventionSwitch.default)();
    }
}