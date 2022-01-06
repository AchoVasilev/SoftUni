namespace VaporStore.DataProcessor
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Data;
    using VaporStore.DataProcessor.Dto;
    using Newtonsoft.Json;
    using System.Linq;
    using VaporStore.Data.Models;
    using VaporStore.DataProcessor.Dto.Import;
    using System.Xml.Serialization;
    using System.IO;
    using System.Globalization;

    public static class Deserializer
    {
        public static string ImportGames(VaporStoreDbContext context, string jsonString)
        {
            StringBuilder output = new StringBuilder();
            GameJsonImportModel[] gameModels = JsonConvert.DeserializeObject<GameJsonImportModel[]>(jsonString);

            foreach (GameJsonImportModel gameModel in gameModels)
            {
                if (!IsValid(gameModel) || !gameModel.Tags.Any())
                {
                    output.AppendLine("Invalid Data");
                    continue;
                }

                Developer developer = context.Developers.FirstOrDefault(x => x.Name == gameModel.Developer)
                    ?? new Developer { Name = gameModel.Developer };

                Genre genre = context.Genres.FirstOrDefault(x => x.Name == gameModel.Genre)
                    ?? new Genre { Name = gameModel.Genre };

                Game game = new Game
                {
                    Name = gameModel.Name,
                    Price = gameModel.Price,
                    ReleaseDate = gameModel.ReleaseDate.Value,
                    Developer = developer,
                    Genre = genre
                };

                foreach (var tagModel in gameModel.Tags)
                {
                    Tag tag = context.Tags.FirstOrDefault(x => x.Name == tagModel)
                        ?? new Tag { Name = tagModel };

                    game.GameTags.Add(new GameTag { Tag = tag });
                }

                context.Games.Add(game);
                context.SaveChanges();

                output.AppendLine($"Added {game.Name} ({game.Genre.Name}) with {game.GameTags.Count} tags");
            }

            return output.ToString().TrimEnd();
        }

        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
        {
            UserJsonImportModel[] userModels = JsonConvert.DeserializeObject<UserJsonImportModel[]>(jsonString);
            StringBuilder output = new StringBuilder();

            foreach (var userModel in userModels)
            {
                if (!IsValid(userModel) || !userModel.Cards.All(IsValid))
                {
                    output.AppendLine("Invalid Data");
                    continue;
                }

                User user = new User
                {
                    FullName = userModel.FullName,
                    Username = userModel.Username,
                    Email = userModel.Email,
                    Age = userModel.Age,
                    Cards = userModel.Cards.Select(x => new Card
                    {
                        Number = x.Number,
                        Cvc = x.Cvc,
                        Type = x.Type.Value
                    })
                    .ToArray()
                };

                context.Users.Add(user);
                output.AppendLine($"Imported {user.Username} with {user.Cards.Count} cards");
            }

            context.SaveChanges();
            return output.ToString().TrimEnd();
        }

        public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
        {
            string root = "Purchases";
            StringBuilder output = new StringBuilder();
            XmlSerializer serializer = new XmlSerializer(typeof(PurchaseXmlImportModel[]), new XmlRootAttribute(root));
            using TextReader reader = new StringReader(xmlString);
            {
                PurchaseXmlImportModel[] purchaseModels = serializer.Deserialize(reader) as PurchaseXmlImportModel[];

                foreach (var purchaseModel in purchaseModels)
                {
                    if (!IsValid(purchaseModel))
                    {
                        output.AppendLine("Invalid Data");
                        continue;
                    }

                    bool isValidDate = DateTime.TryParseExact(purchaseModel.Date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date);
                    if (!isValidDate)
                    {
                        output.AppendLine("Invalid Data");
                        continue;
                    }

                    Purchase purchase = new Purchase
                    {
                        Game = context.Games.FirstOrDefault(x => x.Name == purchaseModel.GameName),
                        Type = purchaseModel.Type.Value,
                        ProductKey = purchaseModel.ProductKey,
                        Card = context.Cards.FirstOrDefault(x => x.Number == purchaseModel.Card),
                        Date = date
                    };

                    context.Purchases.Add(purchase);
                    string username = context.Users
                        .Where(x => x.Id == purchase.Card.UserId)
                        .Select(x => x.Username)
                        .FirstOrDefault();

                    output.AppendLine($"Imported {purchase.Game.Name} for {username}");
                }
            }
            context.SaveChanges();
            return output.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}