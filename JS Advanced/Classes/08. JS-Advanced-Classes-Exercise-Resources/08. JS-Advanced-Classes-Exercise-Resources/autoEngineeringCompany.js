function solve(carsArr) {
    let cars = new Map();

    carsArr.forEach(x => {
        let [brand, model, producedCars] = x.split(' | ');

        if (!cars.has(brand)) {
            cars.set(brand, new Map());
        }

        let models = cars.get(brand);

        if (!models.has(model)) {
            models.set(model, 0);
        }

        let totalProducedCars = models.get(model) + Number(producedCars);

        models.set(model, totalProducedCars);
    });

    for (const brand of cars.keys()) {
        console.log(brand);

        let models = cars.get(brand);

        for (const model of models.keys()) {
            console.log(`###${model} -> ${models.get(model)}`);
        }
    }
}