using System.Collections.Generic;

namespace FastFood.Core.ViewModels.Orders
{
    public class CreateOrderViewModel
    {
        public CreateOrderViewModel()
        {
            this.Items = new List<CreateOrderItemViewModel>();
            this.Employees = new List<CreateOrderEmployeeViewModel>();
        }

        public List<CreateOrderItemViewModel> Items { get; set; }

        public List<CreateOrderEmployeeViewModel> Employees { get; set; }
    }
}
