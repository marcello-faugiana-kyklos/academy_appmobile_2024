#nullable disable

namespace OOPClassLibrary.Games.Dtos;

public class GameTransactionDto
{
    public string TransactionId { get; set; }

    public GameDto Game { get; set; }

    public PlatformDto Platform { get; set; }

    public StoreDto Store { get; set; }

    public LauncherDto Launcher { get; set; }

    public string MediaFormat { get; set; }

    public DateOnly AcquireDate { get; set; }

    public decimal PurchasePrice { get; set; }
}
