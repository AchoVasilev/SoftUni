function attachGradientEvents() {
    let gradientElement = document.getElementById('gradient');
    let resultElement = document.getElementById('result');

    gradientElement.addEventListener('mousemove', onMove);


    function onMove(ev) {
        let box = ev.target;

        let offset = Math.floor(ev.offsetX / box.clientWidth * 100);

        resultElement.textContent = `${offset}%`;
    }
}