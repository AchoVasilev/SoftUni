using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Peripherals;
using OnlineShop.Common.Constants;

namespace OnlineShop.Models.Products.Computers
{
    public abstract class Computer : Product, IComputer
    {
        private List<IComponent> components;
        private List<IPeripheral> peripherals;
        private double average;

        protected Computer(int id, string manufacturer, string model, decimal price, double overallPerformance)
            : base(id, manufacturer, model, price, overallPerformance)
        {
            this.components = new List<IComponent>();
            this.peripherals = new List<IPeripheral>();
        }

        public override double OverallPerformance
        {
            get
            {
                if (Components.Count == 0)
                {
                    return base.OverallPerformance;
                }
                else
                {
                    foreach (var component in components)
                    {
                        this.average += component.OverallPerformance;
                    }

                    this.average /= components.Count;
                    return this.average + base.OverallPerformance;
                }
            }
        }

        public override decimal Price
        {
            get
            {
                decimal sumAllParts = 0;

                foreach (var peripheral in this.Peripherals)
                {
                    sumAllParts += peripheral.Price;
                }

                foreach (var component in this.Components)
                {
                    sumAllParts += component.Price;
                }

                return base.Price + sumAllParts;
            }
        }

        public IReadOnlyCollection<IComponent> Components => this.components;

        public IReadOnlyCollection<IPeripheral> Peripherals => this.peripherals;

        public void AddComponent(IComponent component)
        {
            if (this.Components.Any(c => c.GetType().Name == component.GetType().Name))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingComponent, component.GetType().Name, this.GetType().Name, component.Id));
            }

            this.components.Add(component);
        }

        public void AddPeripheral(IPeripheral peripheral)
        {
            if (this.Peripherals.Any(c => c.GetType().Name == peripheral.GetType().Name))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.ExistingPeripheral, peripheral.GetType().Name, this.GetType().Name, peripheral.Id));
            }

            this.peripherals.Add(peripheral);
        }

        public IComponent RemoveComponent(string componentType)
        {
            IComponent component = this.Components.FirstOrDefault(c => c.GetType().Name == componentType);

            if (!this.Components.Contains(component))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingComponent, componentType, this.GetType().Name, this.Id));
            }

            this.components.Remove(component);

            return component;
        }

        public IPeripheral RemovePeripheral(string peripheralType)
        {
            IPeripheral peripheral = this.Peripherals.FirstOrDefault(p => p.GetType().Name == peripheralType);

            if (!this.Peripherals.Contains(peripheral))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.NotExistingPeripheral, peripheral, this.GetType().Name, this.Id));
            }

            this.peripherals.Remove(peripheral);

            return peripheral;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString())
                .AppendLine($" Components ({this.Components.Count}):");

            foreach (var component in this.Components)
            {
                sb.AppendLine($"  {component.ToString()}");
            }

            double peripheralAvgPerformance = 0;

            foreach (var peripheral in this.Peripherals)
            {
                peripheralAvgPerformance += peripheral.OverallPerformance;
            }

            peripheralAvgPerformance /= this.Peripherals.Count;

            sb.AppendLine($" Peripherals ({this.Peripherals.Count}); Average Overall Performance ({peripheralAvgPerformance:F2}):");

            foreach (var per in this.Peripherals)
            {
                sb.AppendLine($"  {per.ToString()}");
            }

            return sb.ToString().Trim();
        }
    }
}
