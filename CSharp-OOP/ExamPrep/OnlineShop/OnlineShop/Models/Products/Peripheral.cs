using OnlineShop.Models.Products.Peripherals;
using System.Text;

namespace OnlineShop.Models.Products
{
    public abstract class Peripheral : Product, IPeripheral
    {
        protected Peripheral(int id, string manufacturer, string model, decimal price, double overallPerformance, string connectionType)
            : base(id, manufacturer, model, price, overallPerformance)
        {
        }

        public string ConnectionType { get; private set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString())
                .AppendLine($" Connection Type: {this.ConnectionType}");

            return sb.ToString().Trim();
        }
    }
}
