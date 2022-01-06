using SantaWorkshop.Core.Contracts;
using SantaWorkshop.Models.Dwarfs;
using SantaWorkshop.Models.Dwarfs.Contracts;
using SantaWorkshop.Models.Instruments;
using SantaWorkshop.Models.Presents;
using SantaWorkshop.Models.Presents.Contracts;
using SantaWorkshop.Models.Workshops;
using SantaWorkshop.Repositories;
using SantaWorkshop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SantaWorkshop.Core
{
    public class Controller : IController
    {
        private readonly DwarfRepository dwarfs;
        private readonly PresentRepository presents;

        public Controller()
        {
            this.dwarfs = new DwarfRepository();
            this.presents = new PresentRepository();
        }
        public string AddDwarf(string dwarfType, string dwarfName)
        {
            var currentDwarf = dwarfType switch
            {
                "HappyDwarf" => (IDwarf)new HappyDwarf(dwarfName),
                "SleepyDwarf" => new SleepyDwarf(dwarfName),
                _ => throw new InvalidOperationException(ExceptionMessages.InvalidDwarfType)
            };

            this.dwarfs.Add(currentDwarf);
            var outputMsg = string.Format(OutputMessages.DwarfAdded, dwarfType, dwarfName);

            return outputMsg;
        }

        public string AddInstrumentToDwarf(string dwarfName, int power)
        {
            var dwarf = this.dwarfs.Models.FirstOrDefault(d => d.Name == dwarfName);

            if (dwarf == null)
            {
                var msg = ExceptionMessages.InexistentDwarf;
                throw new InvalidOperationException(msg);
            }

            var instrument = new Instrument(power);
            dwarf.AddInstrument(instrument);

            var outputMsg = string.Format(OutputMessages.InstrumentAdded, power, dwarfName);

            return outputMsg;
        }

        public string AddPresent(string presentName, int energyRequired)
        {
            var present = new Present(presentName, energyRequired);
            this.presents.Add(present);

            var outputMsg = string.Format(OutputMessages.PresentAdded, presentName);

            return outputMsg;
        }

        public string CraftPresent(string presentName)
        {
            var workshop = new Workshop();

            var present = this.presents.FindByName(presentName);

            var readyDwarfs = this.dwarfs.Models.
                Where(d => d.Energy >= 50)
                .OrderByDescending(d => d.Energy).ToList();

            if (!readyDwarfs.Any())
            {
                var msg = ExceptionMessages.DwarfsNotReady;
                throw new InvalidOperationException(msg);
            }

            while (readyDwarfs.Any())
            {
                var currentDwarf = readyDwarfs.First();

                workshop.Craft(present, currentDwarf);

                if (!currentDwarf.Instruments.Any())
                {
                    readyDwarfs.Remove(currentDwarf);
                }

                if (currentDwarf.Energy == 0)
                {
                    this.dwarfs.Remove(currentDwarf);
                    readyDwarfs.Remove(currentDwarf);
                }

                if (present.IsDone())
                {
                    break;
                }
            }


            var outputMsg =
                string.Format(present.IsDone() ?
                    OutputMessages.PresentIsDone :
                    OutputMessages.PresentIsNotDone, presentName);

            return outputMsg;
        }

        public string Report()
        {
            var craftedPresents = this.presents.Models.
                Count(p => p.IsDone());

            var sb = new StringBuilder();

            sb.AppendLine($"{craftedPresents} presents are done!")
                .AppendLine("Dwarfs info:");

            foreach (var dwarf in this.dwarfs.Models)
            {
                sb.AppendLine($"Name: {dwarf.Name}");
                sb.AppendLine($"Energy: {dwarf.Energy}");
                sb.AppendLine($"Instruments: {dwarf.Instruments.Count} not broken left");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
