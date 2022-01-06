using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parking
{
    public class Parking
    {
        private List<Car> data;
        public Parking(string type, int capacity)
        {
            this.Type = type;
            this.Capacity = capacity;
            data = new List<Car>();
        }
        public string Type { get; set; }
        public  int Capacity { get; set; }
        public int Count => data.Count;

        public void Add(Car car)
        {
            if (this.Capacity > Count)
            {
                data.Add(car);
            }
        }

        public bool Remove(string manufacturer, string model)
        {
            Car car = data.Where(x => x.Manufacturer == manufacturer)
                .Where(x => x.Model == model)
                .FirstOrDefault();

            return data.Remove(car);
        }

        public Car GetLatestCar()
        {
            Car car = data.OrderByDescending(c => c.Year).FirstOrDefault();

            return car;
        }

        public Car GetCar(string manufacturer, string model)
        {
            Car car = data.Where(c => c.Manufacturer == manufacturer)
                .Where(c => c.Model == model)
                .FirstOrDefault();

            return car;
        }

        public string GetStatistics()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"The cars are parked in {this.Type}:");

            foreach (var car in data)
            {
                sb.AppendLine(car.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
