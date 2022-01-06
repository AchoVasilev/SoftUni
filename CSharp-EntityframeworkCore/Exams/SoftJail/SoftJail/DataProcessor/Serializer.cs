namespace SoftJail.DataProcessor
{

    using System;
    using System.Linq;
    using System.Xml.Serialization;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    using Data;
    using SoftJail.DataProcessor.ExportDto;
    using System.IO;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            List<PrisonersByCellsExportModel> prisoners = context.Prisoners
                .Where(x => ids.Contains(x.Id))
                .Select(x => new PrisonersByCellsExportModel
                {
                    Id = x.Id,
                    Name = x.FullName,
                    CellNumber = x.Cell.CellNumber,
                    Officers = x.PrisonerOfficers.Select(y => new OfficerExportModel
                    {
                        OfficerName = y.Officer.FullName,
                        Department = y.Officer.Department.Name
                    })
                    .OrderBy(y => y.OfficerName)
                    .ToArray(),
                    TotalOfficerSalary = decimal.Parse(x.PrisonerOfficers.Sum(x => x.Officer.Salary).ToString("F2"))
                })
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Id)
                .ToList();

            string result = JsonConvert.SerializeObject(prisoners, Formatting.Indented);

            return result;
        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            string[] names = prisonersNames.Split(",", StringSplitOptions.RemoveEmptyEntries);

            string root = "Prisoners";
            XmlSerializer serializer = new XmlSerializer(typeof(PrisonerInboxExportModel[]), new XmlRootAttribute(root));

            PrisonerInboxExportModel[] prisoners = context.Prisoners
                .Where(x => names.Contains(x.FullName))
                .Select(x => new PrisonerInboxExportModel
                {
                    Id = x.Id,
                    Name = x.FullName,
                    IncarcerationDate = x.IncarcerationDate.ToString("yyyy-MM-dd"),
                    EncryptedMessages = x.Mails.Select(y => new MessagesExportModel
                    {
                        Description = string.Join("", y.Description.Reverse())
                    })
                    .ToArray()
                })
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Id)
                .ToArray();

            TextWriter writer = new StringWriter();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            serializer.Serialize(writer, prisoners, ns);

            return writer.ToString();
        }
    }
}