namespace VaporStore.DataProcessor.Dto.Export
{
    public class GameByGenreJsonExportModel
    {
        public int Id { get; set; }

        public string Genre { get; set; }

        public GamesJsonExportModel[] Games { get; set; }

        public int TotalPlayers { get; set; }
    }
}
