using OOP.Common;

namespace OOPClassLibrary.Games.Models;

public class GameTransaction
{
    public string TransactionId { get; }
    public Game Game { get; }
    public Platform Platform { get; }
    public Store Store { get; }
    public Launcher Launcher { get; }

    public MediaFormat MediaFormat { get; }

    public DateOnly AcquireDate { get; }

    public PurchasePrice PurchasePrice { get; }

    public GameTransaction
    (
        string transactionId,
        Game game,
        Store store,
        Platform platform,
        Launcher launcher,
        MediaFormat mediaFormat,
        DateOnly acquireDate,
        decimal purchasePrice
    )
    {
        TransactionId = transactionId.GetWithTextOrThrow(nameof(transactionId));
        Game = game.GetNonNullOrThrow(nameof(game));
        Platform = platform.GetNonNullOrThrow(nameof(game));
        Store = store.GetNonNullOrThrow(nameof(store));
        Launcher = launcher.GetNonNullOrThrow(nameof(launcher));
        MediaFormat = mediaFormat;
        AcquireDate = acquireDate;

        PurchasePrice =
            purchasePrice >= 0m ?
            purchasePrice :
            throw new ArgumentException($"Price must be >= 0");
    }
}


