namespace GamesDal.DbModels;

public class GameTransactionDbItem
{
    public string TransactionId { get; set; } = null!;
    public string GameId { get; set; } = null!;
    public string GameTitle { get; set; } = null!;
    public string? MainGameId { get; set; } = null!;
    public string PlatformId { get; set; } = null!;
    public string PlatformName { get; set; } = null!;
    public string StoreId { get; set; } = null!;
    public string StoreName { get; set; } = null!;
    public string LauncherId { get; set; } = null!;
    public string LauncherName { get; set; } = null!;

    public string MediaFormatId { get; set; } = null!;
    public string MediaFormat { get; set; } = null!;

    public DateOnly AcquireDate { get; set; }

    public decimal PurchasePrice { get; set; }
}
