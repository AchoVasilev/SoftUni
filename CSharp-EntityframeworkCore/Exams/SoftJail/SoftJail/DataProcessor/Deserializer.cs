namespace SoftJail.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Newtonsoft.Json;

    using Data;
    using SoftJail.DataProcessor.ImportDto;
    using System.Linq;
    using System.Text;
    using SoftJail.Data.Models;
    using System.Globalization;
    using System.Xml.Serialization;
    using System.IO;
    using SoftJail.Data.Models.Enums;

    public class Deserializer
    {
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            List<Department> departments = new List<Department>();

            DepartmentCellInputModel[] departmentCells = JsonConvert.DeserializeObject<DepartmentCellInputModel[]>(jsonString);

            foreach (var departmentCell in departmentCells)
            {
                if (!IsValid(departmentCell) || !departmentCell.Cells.All(IsValid) || !departmentCell.Cells.Any())
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                Department department = new Department
                {
                    Name = departmentCell.Name,
                    Cells = departmentCell.Cells
                                           .Select(x => new Cell
                                           {
                                               CellNumber = x.CellNumber,
                                               HasWindow = x.HasWindow
                                           })
                                           .ToList()
                };

                departments.Add(department);

                sb.AppendLine($"Imported {department.Name} with {department.Cells.Count} cells");
            }

            context.AddRange(departments);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            List<Prisoner> prisoners = new List<Prisoner>();

            PrisonerMailInputModel[] prisonerMails = JsonConvert.DeserializeObject<PrisonerMailInputModel[]>(jsonString);

            foreach (var prisonerMail in prisonerMails)
            {
                if (!IsValid(prisonerMail) || !prisonerMail.Mails.All(IsValid))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                bool isValidReleaseDate = DateTime.TryParseExact(prisonerMail.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out DateTime releaseDate);

                DateTime incarcerationDate = DateTime.ParseExact(prisonerMail.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                Prisoner prisoner = new Prisoner
                {
                    FullName = prisonerMail.FullName,
                    Nickname = prisonerMail.Nickname,
                    Age = prisonerMail.Age,
                    Bail = prisonerMail.Bail,
                    CellId = prisonerMail.CellId,
                    IncarcerationDate = incarcerationDate,
                    ReleaseDate = isValidReleaseDate ? (DateTime?)releaseDate : null,
                    Mails = prisonerMail.Mails
                                        .Select(x => new Mail
                                        {
                                            Description = x.Description,
                                            Address = x.Address,
                                            Sender = x.Sender
                                        })
                                        .ToList()
                };

                prisoners.Add(prisoner);
                sb.AppendLine($"Imported {prisoner.FullName} {prisoner.Age} years old");
            }

            context.Prisoners.AddRange(prisoners);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            List<Officer> officers = new List<Officer>();

            string root = "Officers";
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(OfficerPrisonerInputModel[]), new XmlRootAttribute(root));
            TextReader reader = new StringReader(xmlString);

            OfficerPrisonerInputModel[] officerPrisoners = xmlSerializer.Deserialize(reader) as OfficerPrisonerInputModel[];

            foreach (var officerPrisoner in officerPrisoners)
            {
                if (!IsValid(officerPrisoner))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                Officer officer = new Officer
                {
                    FullName = officerPrisoner.Name,
                    Salary = officerPrisoner.Money,
                    DepartmentId = officerPrisoner.DepartmentId,
                    Position = Enum.Parse<Position>(officerPrisoner.Position),
                    Weapon = Enum.Parse<Weapon>(officerPrisoner.Weapon),
                    OfficerPrisoners = officerPrisoner.Prisoners.Select(x => new OfficerPrisoner
                    {
                        PrisonerId = x.Id
                    })
                    .ToList()
                };

                officers.Add(officer);
                sb.AppendLine($"Imported {officer.FullName} ({officer.OfficerPrisoners.Count} prisoners)");
            }

            context.Officers.AddRange(officers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
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