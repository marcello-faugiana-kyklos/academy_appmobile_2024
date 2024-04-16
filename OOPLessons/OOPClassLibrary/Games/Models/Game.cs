using OOPClassLibrary.Games.Exceptions;
using OOPClassLibrary.Support;
using System.Diagnostics.CodeAnalysis;

namespace OOPClassLibrary.Games.Models;

public class Game
{
    private readonly ISet<DlcGame> _dlcGames;


    public IEnumerable<Game> DlcGames =>
        _dlcGames;

    public string GameId { get; }   // zelda-botw
    public string Title { get; }  // The Legend of Zelda: Breath of the Wild
    public string? JsonData { get; }

    public Game(string id, string title, string? jsonData = null)
    {
        GameId = id.GetNonNullOrThrow(nameof(id));
        Title = title.GetNonNullOrThrow(nameof(title));
        JsonData = jsonData;

        _dlcGames = new HashSet<DlcGame>(DlcGame.EqualityComparer);
    }

    public Game AddNewDlc
    (
        string dlcId, 
        string dlcTitle, 
        string? dlcJsonData = null
    )
    {
        if (IsDlc)
        {
            throw new DlcCannotHaveOtherDlcException($"Game '{Title}' is already a dlc");
        }

        DlcGame dlcGame = new DlcGame(this, dlcId, dlcTitle, dlcJsonData);
        _dlcGames.Add(dlcGame);
        return dlcGame;
    }

    public virtual bool IsDlc =>
        false;


    public Game? MainGame =>
        (this as DlcGame)?._mainGame;


    private class DlcGame : Game
    {
        internal readonly Game _mainGame;

        public DlcGame(Game mainGame, string id, string title, string? jsonData) : 
            base(id, title, jsonData)
        {
            _mainGame = mainGame.GetNonNullOrThrow(nameof(mainGame));
        }

        public override bool IsDlc =>
            true;

        private class DlcGameEqualityComparer : IEqualityComparer<DlcGame>
        {
            public bool Equals(DlcGame? x, DlcGame? y) =>
                x?.GameId.Equals(y?.GameId) ?? false;

            public int GetHashCode([DisallowNull] DlcGame dlc) =>
                dlc.GameId.GetHashCode();
        }

        public static IEqualityComparer<DlcGame> EqualityComparer { get; } =
            new DlcGameEqualityComparer();

    }
}


