function solve( {model, power, color, carriage, wheelsize}) {
    const storedEngines = [
        { power: 90, volume: 1800 },
        { power: 120, volume: 2400 },
        { power: 200, volume: 3500 }
    ];

    let size = wheelsize % 2 === 0 ? --wheelsize : wheelsize;
    let wheels = [size, size, size, size];

    let enginePower = storedEngines.reduce((a, v) =>
        Math.abs(a.power - power) < Math.abs(v.power - power) ? a : v);

    let object = {
        model,
        engine: enginePower,
        carriage: {
            type: carriage,
            color: color
        },
        wheels
    }

    return object;
}

console.log(solve({ model: 'VW Golf II',
power: 90,
color: 'blue',
carriage: 'hatchback',
wheelsize: 14 }
))