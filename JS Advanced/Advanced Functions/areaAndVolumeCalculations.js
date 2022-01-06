function area() {
    return Math.abs(this.x * this.y);
};

function vol() {
    return Math.abs(this.x * this.y * this.z);
};

function solve(area, vol, input) {
    let objects = JSON.parse(input);

    function calc(obj) {
        let areaObj = Math.abs(area.call(obj));
        let volObj = Math.abs(vol.call(obj));

        return {
            area: areaObj,
            volume: volObj
        };
    }

    return objects.map(calc);
}
