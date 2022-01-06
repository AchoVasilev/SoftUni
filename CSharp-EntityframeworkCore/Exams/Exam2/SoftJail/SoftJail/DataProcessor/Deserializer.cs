namespace SoftJail.DataProcessor
{

    using Data;
    using Newtonsoft.Json;
    using SoftJail.Data.Models;
    using SoftJail.Data.Models.Enums;
    using SoftJail.DataProcessor.ImportDto;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            StringBuilder output = new StringBuilder();
            JsonDepartmentImportModel[] departmentModels = JsonConvert.DeserializeObject<JsonDepartmentImportModel[]>(jsonString);

            foreach (var departmentModel in departmentModels)
            {
                if (!IsValid(departmentModel) || !departmentModel.Cells.All(IsValid) || departmentModel.Cells.Length == 0)
                {
                    output.AppendLine("Invalid Data");
                    continue;
                }

                Department department = new Department
                {
                    Name = departmentModel.Name,
                    Cells = departmentModel.Cells.Select(x => new Cell
                    {
                        CellNumber = x.CellNumber,
                        HasWindow = x.HasWindow
                    })
                    .ToArray()
                };

                output.AppendLine($"Imported {department.Name} with {department.Cells.Count} cells");
                context.Departments.Add(department);
            }
            context.SaveChanges();

            return output.ToString().TrimEnd();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            PrisonerJsonImportModel[] prisonerModels = JsonConvert.DeserializeObject<PrisonerJsonImportModel[]>(jsonString);
            StringBuilder output = new StringBuilder();

            foreach (var prisonerModel in prisonerModels)
            {
                if (!IsValid(prisonerModel) || !prisonerModel.Mails.All(IsValid))
                {
                    output.AppendLine("Invalid Data");
                    continue;
                }

                bool isValidReleaseDate = DateTime.TryParseExact(prisonerModel.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime releaseDate);
                Prisoner prisoner = new Prisoner
                {
                    FullName = prisonerModel.FullName,
                    Nickname = prisonerModel.Nickname,
                    Age = prisonerModel.Age,
                    IncarcerationDate = DateTime.ParseExact(prisonerModel.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    ReleaseDate = isValidReleaseDate ? (DateTime?)releaseDate : null,
                    CellId = prisonerModel.CellId,
                    Mails = prisonerModel.Mails.Select(x => new Mail
                    {
                        Description = x.Description,
                        Sender = x.Sender,
                        Address = x.Address
                    })
                    .ToArray()
                };

                output.AppendLine($"Imported {prisoner.FullName} {prisoner.Age} years old");
                context.Prisoners.Add(prisoner);
            }
            context.SaveChanges();

            return output.ToString().TrimEnd();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            string root = "Officers";
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlOfficerImportModel[]), new XmlRootAttribute(root));
            StringBuilder output = new StringBuilder();

            using (TextReader reader = new StringReader(xmlString))
            {
                XmlOfficerImportModel[] officerModels = xmlSerializer.Deserialize(reader) as XmlOfficerImportModel[];
                foreach (var officerModel in officerModels)
                {
                    if (!IsValid(officerModel))
                    {
                        output.AppendLine("Invalid Data");
                        continue;
                    }

                    Officer officer = new Officer
                    {
                        FullName = officerModel.FullName,
                        Salary = officerModel.Salary,
                        Position = Enum.Parse<Position>(officerModel.Position),
                        Weapon = Enum.Parse<Weapon>(officerModel.Weapon),
                        DepartmentId = officerModel.DepartmentId,
                        OfficerPrisoners = officerModel.Prisoners.Select(x => new OfficerPrisoner
                        {
                            PrisonerId = x.Id
                        })
                        .ToArray()
                    };

                    output.AppendLine($"Imported {officer.FullName} ({officer.OfficerPrisoners.Count} prisoners)");
                    context.Officers.Add(officer);
                }
            }

            context.SaveChanges();
            return output.ToString().TrimEnd();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }
    }
}