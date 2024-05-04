using OOP.Common;

namespace OOPClassLibrary.Games.Models;

public class GamesCollection
{
    private readonly List<GameTransaction> _gamesTransactions;

    public string Name { get; }
    public string Owner { get; }

    public GamesCollection(string name, string owner)
    {
        Name = name;
        Owner = owner;
        _gamesTransactions = new List<GameTransaction>();
    }

    public GameTransaction AddNewGameTransaction
    (
        Game game,
        Store store,
        Platform platform,
        Launcher launcher,
        MediaFormat mediaFormat = MediaFormat.Digital,
        DateOnly? acquireDate = null,
        decimal purchasePrice = 0m
    )
    {
        GameTransaction gameTransaction =
            new
            (
                Guid.NewGuid().ToString(),
                game,
                store,
                platform,
                launcher,
                mediaFormat,
                acquireDate ?? DateTime.Today.ToDateOnly(),
                purchasePrice
            );

        _gamesTransactions.Add(gameTransaction);
        return gameTransaction;
    }
}


