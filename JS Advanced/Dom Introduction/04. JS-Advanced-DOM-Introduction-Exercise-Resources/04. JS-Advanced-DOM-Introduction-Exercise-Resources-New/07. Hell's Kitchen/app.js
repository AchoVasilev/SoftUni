// function solve() {
//    document.querySelector('#btnSend').addEventListener('click', onClick);

//    function onClick() {
//       let textAreaElement = document.querySelector('#inputs textarea');

//       let text = textAreaElement.value;

//       let inputArr = JSON.parse(text);

//       let restaurants = {};

//       for (let i = 0; i < inputArr.length; i++) {
//          let [restaurantName, workerString] = inputArr[i].split(' - ');

//          let inputWorkers = workerString.split(', ')
//             .map(x => {
//                let [name, salary] = x.split(' ');
//                return { name, salary: Number(salary) };
//             });

//          if (!restaurants[restaurantName]) {
//             restaurants[restaurantName] = {
//                workers: [],
//                restaurantName: restaurantName,
//                getAverageSalary: function () {
//                   return this.workers.reduce((acc, el) => acc + el.salary, 0) / this.workers.length;
//                }
//             }
//          }

//          restaurants[restaurantName].workers = restaurants[restaurantName].workers.concat(inputWorkers);
//       }

//       let sortedRestaurants = Object.values(restaurants)
//             .sort((a, b) => b.getAverageSalary() - a.getAverageSalary());

//       let bestRestaurant = sortedRestaurants[0];
//       let sortedWorkers = bestRestaurant.workers.sort((a, b) => b.salary - a.salary);

//       let averageSalary = bestRestaurant.getAverageSalary().toFixed(2);
//       let workersSalary = sortedWorkers[0].salary.toFixed(2);

//       let topRestaurantString = `Name: ${bestRestaurant.restaurantName} Average Salary: ${averageSalary} Best Salary: ${workersSalary}`;
//       let workersString = sortedWorkers
//          .map(x => `Name: ${x.name} With Salary ${x.salary}`)
//          .join(' ');

//       let bestRestaurantElement = document.querySelector('#bestRestaurant p');

//       let workersElement = document.querySelector('#workers p');

//       bestRestaurantElement.textContent = topRestaurantString;
//       workersElement.textContent = workersString;
//    }
// }

function solve() {
   const html = {
       inputField: document.querySelector("#inputs textarea"),
       outputBestName: document.querySelector("#bestRestaurant p"),
       outputBestWorkers: document.querySelector("#workers p"),
   }

   const getBest = data =>
       Object.entries(data).sort(
           (x, y) => getAverage(y[1]) - getAverage(x[1])
       )[0]

   const getAverage = workersData =>
       workersData.reduce((a, v) => a + v[1], 0) / workersData.length

   function deserialize(data) {
       const getWorkers = data =>
           data
               .split(", ")
               .map(x => x.split(" ").map(y => (isNaN(y) ? y : Number(y))))

       return JSON.parse(data)
           .map(x => x.split(" - "))
           .reduce((a, v) => {
               const [name, workers] = v

               a[name] = a[name]
                   ? [...a[name], ...getWorkers(workers)]
                   : getWorkers(workers)
               return a
           }, {})
   }

   function displayResult(data) {
       let [name, workers] = data
       workers = workers.sort((x, y) => y[1] - x[1])

       html.outputBestName.innerHTML = `Name: ${name} Average Salary: ${getAverage(
           workers
       ).toFixed(2)} Best Salary: ${workers[0][1].toFixed(2)}`

       html.outputBestWorkers.innerHTML = `${workers
           .map(x => `Name: ${x[0]} With Salary: ${x[1]}`)
           .join(" ")}`
   }

   document
       .getElementById("btnSend")
       .addEventListener("click", () =>
           displayResult(getBest(deserialize(html.inputField.value)))
       )
}