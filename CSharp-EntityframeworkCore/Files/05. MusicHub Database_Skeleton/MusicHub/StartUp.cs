namespace MusicHub
{
    using System;
    using System.Linq;
    using System.Text;

    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            MusicHubDbContext context =
                new MusicHubDbContext();

            DbInitializer.ResetDatabase(context);

            string result = ExportSongsAboveDuration(context, 4);

            Console.WriteLine(result);
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            StringBuilder sb = new StringBuilder();

            var albums = context.Producers
                .FirstOrDefault(x => x.Id == producerId)
                .Albums
                .Select(x => new
                {
                    AlbumName = x.Name,
                    ReleaseDate = x.ReleaseDate,
                    ProducerName = x.Producer.Name,
                    Songs = x.Songs
                              .Select(y => new
                              {
                                  SongName = y.Name,
                                  Price = y.Price,
                                  Writer = y.Writer.Name
                              })
                              .OrderByDescending(y => y.SongName)
                              .ThenBy(y => y.Writer)
                              .ToList(),
                    AlbumPrice = x.Price
                })
                .OrderByDescending(x => x.AlbumPrice)
                .ToList();

            foreach (var album in albums)
            {
                sb.AppendLine($"-AlbumName: {album.AlbumName}")
                    .AppendLine($"-ReleaseDate: {album.ReleaseDate:MM/dd/yyyy}")
                    .AppendLine($"-ProducerName: {album.ProducerName}")
                    .AppendLine($"-Songs:");

                int counter = 1;

                foreach (var song in album.Songs)
                {
                    sb.AppendLine($"---#{counter++}")
                        .AppendLine($"---SongName: {song.SongName}")
                        .AppendLine($"---Price: {song.Price:F2}")
                        .AppendLine($"---Writer: {song.Writer}");
                }

                sb.AppendLine($"-AlbumPrice: {album.AlbumPrice:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            StringBuilder sb = new StringBuilder();

            var songs = context.Songs
                .ToList()
                .Where(s => s.Duration.TotalSeconds > duration)
                .Select(x => new
                {
                    SongName = x.Name,
                    PerformerName = x.SongPerformers
                                     .Select(x => x.Performer.FirstName + " " + x.Performer.LastName)
                                     .FirstOrDefault(),
                    WriterName = x.Writer.Name,
                    AlbumProducer = x.Album.Producer.Name,
                    SongDuration = x.Duration
                })
                .OrderBy(x => x.SongName)
                .ThenBy(x => x.WriterName)
                .ThenBy(x => x.PerformerName)
                .ToList();

            int counter = 1;

            foreach (var song in songs)
            {
                sb.AppendLine($"-Song #{counter++}")
                    .AppendLine($"---SongName: {song.SongName}")
                    .AppendLine($"---Writer: {song.WriterName}")
                    .AppendLine($"---Performer: {song.PerformerName}")
                    .AppendLine($"---AlbumProducer: {song.AlbumProducer}")
                    .AppendLine($"---Duration: {song.SongDuration:c}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
