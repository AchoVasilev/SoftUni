using System;
using System.Text;
using OnlineShop.Models.Products.Components;

namespace OnlineShop.Models.Products
{
    public abstract class Component : Product, IComponent
    {
        protected Component(int id, string manufacturer, string model, decimal price, double overallPerformance, int generation) 
            : base(id, manufacturer, model, price, overallPerformance)
        {
            this.Generation = generation;
        }

        public int Generation { get; private set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(base.ToString())
                .AppendLine($" Generation: {this.Generation}");

            return sb.ToString().Trim();
        }
    }
}
