namespace GamesDal.DbModels;

public class GameDbItem
{
    public string GameId { get; set; } = null!; 
    public string Title { get; set; } = null!;
    public string? JsonData { get; set; }
    public string? MainGameId { get; set; }
}
