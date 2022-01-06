namespace VaporStore.DataProcessor
{
	using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.DataProcessor.Dto.Export;

    public static class Serializer
	{
		public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
		{
			GameByGenreJsonExportModel[] gamesModel = context.Genres
				.ToArray()
				.Where(x => genreNames.Contains(x.Name))
				.Select(x => new GameByGenreJsonExportModel
				{
					Id = x.Id,
					Genre = x.Name,
					Games = x.Games.Select(y => new GamesJsonExportModel
					{
						Id = y.Id,
						Title = y.Name,
						Developer = y.Developer.Name,
						Tags = string.Join(", ", y.GameTags.Select(x => x.Tag.Name)),
						Players = y.Purchases.Count
					})
					.Where(z => z.Players > 0)
					.OrderByDescending(z => z.Players)
					.ThenBy(z => z.Id)
					.ToArray(),
					TotalPlayers = x.Games.Sum(y => y.Purchases.Count)
				})
				.OrderByDescending(x => x.TotalPlayers)
				.ThenBy(x => x.Id)
				.ToArray();

			return JsonConvert.SerializeObject(gamesModel, Formatting.Indented);
		}

		public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
		{
			StringBuilder output = new StringBuilder();
			string root = "Users";
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserXmlExportModel[]), new XmlRootAttribute(root));

			UserXmlExportModel[] usersXml = context.Users
				.ToArray()
				.Where(x => x.Cards.Any(y => y.Purchases.Any(z => z.Type.ToString() == storeType)))
				.Select(x => new UserXmlExportModel
				{
					Username = x.Username,
					Purchases = x.Cards.SelectMany(c => c.Purchases)
					.Where(y => y.Type.ToString() == storeType)
					.Select(y => new PurchaseXmlExportModel
					{
						Card = y.Card.Number,
						Cvc = y.Card.Cvc,
						Date = y.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
						Game = new GameXmlExportModel
						{
							Name = y.Game.Name,
							Genre = y.Game.Genre.Name,
							Price = y.Game.Price
						}
					})
					.OrderBy(x => x.Date)
					.ToArray(),
					TotalSpent = x.Cards.Sum(y => y.Purchases.Where(p => p.Type.ToString() == storeType).Sum(z => z.Game.Price))
				})
				.OrderByDescending(x => x.TotalSpent)
				.ThenBy(x => x.Username)
				.ToArray();

			XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
			ns.Add(string.Empty, string.Empty);
			using (TextWriter writer = new StringWriter(output))
			{
				xmlSerializer.Serialize(writer, usersXml, ns);
			}

			return output.ToString().TrimEnd();
		}
	}
}