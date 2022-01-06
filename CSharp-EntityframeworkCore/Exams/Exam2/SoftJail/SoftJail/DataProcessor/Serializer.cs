namespace SoftJail.DataProcessor
{

    using Data;
    using Newtonsoft.Json;
    using SoftJail.DataProcessor.ExportDto;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            JsonPrisonerExportModel[] prisonerModels = context.Prisoners
                .Where(x => ids.Contains(x.Id))
                .Select(x => new JsonPrisonerExportModel
                {
                    Id = x.Id,
                    Name = x.FullName,
                    CellNumber = x.Cell.CellNumber,
                    Officers = x.PrisonerOfficers.Select(y => new JsonOfficerExportModel
                    {
                        Department = y.Officer.Department.Name,
                        OfficerName = y.Officer.FullName
                    })
                    .OrderBy(y => y.OfficerName)
                    .ToArray(),
                    TotalOfficerSalary = decimal.Parse(x.PrisonerOfficers.Sum(y => y.Officer.Salary).ToString("F2"))
                })
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Id)
                .ToArray();

            return JsonConvert.SerializeObject(prisonerModels, Formatting.Indented);
        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            string root = "Prisoners";
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlPrisonerExportModel[]), new XmlRootAttribute(root));
            string[] names = prisonersNames.Split(",", StringSplitOptions.RemoveEmptyEntries);

            XmlPrisonerExportModel[] prisonerModels = context.Prisoners
                .Where(x => names.Contains(x.FullName))
                .Select(x => new XmlPrisonerExportModel
                {
                    Id = x.Id,
                    Name = x.FullName,
                    IncarcerationDate = x.IncarcerationDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    EncryptedMessages = x.Mails.Select(y => new XmlMessageExportModel
                    {
                        Description = string.Join("", y.Description.Reverse())
                    })
                    .ToArray()
                })
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Id)
                .ToArray();

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            StringBuilder output = new StringBuilder();

            using (TextWriter writer = new StringWriter(output))
            {
                xmlSerializer.Serialize(writer, prisonerModels, namespaces);
            }

            return output.ToString().TrimEnd();
        }
    }
}