using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

using OnlineShop.Common.Constants;
using OnlineShop.Models.Products.Computers;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Peripherals;

namespace OnlineShop.Core
{
    public class Controller : IController
    {
        private readonly List<IComputer> computers;

        public Controller()
        {
            computers = new List<IComputer>();
        }

        public string AddComputer(string computerType, int id, string manufacturer, string model, decimal price)
        {
            switch (computerType)
            {
                case "DesktopComputer":
                    var desktop = new DesktopComputer(id, manufacturer, model, price);

                    if (computers.Any(c => c.Id == id))
                    {
                        throw new ArgumentException(ExceptionMessages.ExistingComputerId);
                    }

                    computers.Add(desktop);
                    break;
                case "Laptop":
                    var laptop = new Laptop(id, manufacturer, model, price);

                    if (computers.Any(c => c.Id == id))
                    {
                        throw new ArgumentException(ExceptionMessages.ExistingComputerId);
                    }

                    computers.Add(laptop);
                    break;
                default:
                    throw new ArgumentException(ExceptionMessages.InvalidComputerType);
            }

            return string.Format(SuccessMessages.AddedComputer, id);
        }

        public string AddComponent(int computerId, int id, string componentType, string manufacturer, string model, decimal price, double overallPerformance, int generation)
        {
            IComputer computer = this.CheckIfComputerExist(id);

            if (computer.Components.Any(c => c.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingComponentId);
            }

            IComponent newComponent = componentType switch
            {
                "CentralProcessingUnit" => new CentralProcessingUnit(id, manufacturer, model, price, overallPerformance, generation),
                "Motherboard" => new Motherboard(id, manufacturer, model, price, overallPerformance, generation),
                "PowerSupply" => new PowerSupply(id, manufacturer, model, price, overallPerformance, generation),
                "RandomAccessMemory" => new RandomAccessMemory(id, manufacturer, model, price, overallPerformance, generation),
                "SolidStateDrive" => new SolidStateDrive(id, manufacturer, model, price, overallPerformance, generation),
                "VideoCard" => new VideoCard(id, manufacturer, model, price, overallPerformance, generation),
                _ => throw new ArgumentException(ExceptionMessages.InvalidComponentType)
            };

            computer.AddComponent(newComponent);

            return string.Format(SuccessMessages.AddedComponent, componentType, id, computerId);

        }

        public string AddPeripheral(int computerId, int id, string peripheralType, string manufacturer, string model, decimal price, double overallPerformance, string connectionType)
        {
            IComputer computer = this.CheckIfComputerExist(id);

            if (computer.Peripherals.Any(p => p.Id == id))
            {
                throw new ArgumentException(ExceptionMessages.ExistingPeripheralId);
            }

            foreach (var currentComputer in this.computers)
            {
                if (currentComputer.Peripherals.Any(i => i.Id == id))
                {
                    throw new ArgumentException(ExceptionMessages.ExistingPeripheralId);
                }
            }

            IPeripheral peripheral = peripheralType switch
            {
                "Headset" => new Headset(id, manufacturer, model, price, overallPerformance, connectionType),
                "Keyboard" => new Keyboard(id, manufacturer, model, price, overallPerformance, connectionType),
                "Monitor" => new Monitor(id, manufacturer, model, price, overallPerformance, connectionType),
                "Mouse" => new Mouse(id, manufacturer, model, price, overallPerformance, connectionType),
                _ => throw new ArgumentException(ExceptionMessages.InvalidPeripheralType)
            };

            computer.AddPeripheral(peripheral);

            return string.Format(SuccessMessages.AddedPeripheral, peripheralType, id, computerId);
        }

        public string RemoveComponent(string componentType, int computerId)
        {
            IComputer computer = this.CheckIfComputerExist(computerId);

            IComponent component = computer.RemoveComponent(componentType);

            return string.Format(SuccessMessages.RemovedComponent, componentType, component.Id);
        }

        public string RemovePeripheral(string peripheralType, int computerId)
        {
            IComputer computer = this.computers.FirstOrDefault(i => i.Id == computerId);

            if (computer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            IPeripheral peripheral = computer.RemovePeripheral(peripheralType);

            return string.Format(SuccessMessages.RemovedPeripheral, peripheralType, peripheral.Id);
        }

        public string BuyBest(decimal budget)
        {
            IComputer computer;
            var computerCollection = this.computers.OrderByDescending(i => i.OverallPerformance);

            foreach (var comp in computerCollection)
            {
                if (comp.Price <= budget)
                {
                    computer = comp;
                    this.computers.Remove(computer);

                    return computer.ToString();
                }
            }

            throw new ArgumentException(string.Format(ExceptionMessages.CanNotBuyComputer, budget));
        }

        public string BuyComputer(int id)
        {
            IComputer computer = this.CheckIfComputerExist(id);
            computers.Remove(computer);

            return computer.ToString();
        }

        public string GetComputerData(int id)
        {
            var computer = CheckIfComputerExist(id);
            return computer.ToString();
        }

        private IComputer CheckIfComputerExist(int id)
        {
            var computer = computers.FirstOrDefault(i => i.Id == id);
            if (computer == null)
            {
                throw new ArgumentException("Computer with this id does not exist.");
            }

            return computer;
        }
    }
}
