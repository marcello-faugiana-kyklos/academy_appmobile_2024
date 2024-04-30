using GamesDal;
using GamesDal.DbModels;
using OOPClassLibrary.Games.Models;

namespace OOPClassLibrary.Games;

public class GamesService : IGamesService
{
    private readonly IGamesDao _gamesDao;

    public GamesService(IGamesDao gamesDao)
    {
        _gamesDao = gamesDao;
    }

    public async Task<List<Game>> GetAllGamesAsync()
    {
        var dbItems = await _gamesDao.GetAllGameDbItemsAsync();
        return BuildGamesFromDbItems(dbItems);
    }

    private List<Game> BuildGamesFromDbItems(IEnumerable<GameDbItem> gamesDbItems)
    {
        var mainGames = gamesDbItems.Where(g => g.MainGameId is null);
        var dlcGames = gamesDbItems.Where(g => g.MainGameId is not null);

        return
            mainGames
            .Join
            (
                dlcGames,
                g => g.GameId,
                g => g.MainGameId,
                (g, dlc) => (MainGame: g, Dlc: dlc)
            )
            .GroupBy
            (
                x => x.MainGame.GameId
            )
            .Select
            (
                group =>
                {
                    var mainGame = group.First().MainGame;
                    Game g = new Game(mainGame.GameId, mainGame.Title, mainGame.JsonData);

                    foreach (var item in group)
                    {
                        g.AddNewDlc(item.Dlc.GameId, item.Dlc.Title, item.Dlc.JsonData);
                    }
                    return g;
                }
            )
            .Concat
            (
                mainGames
                .Where(g => !dlcGames.Any(x => x.MainGameId == g.GameId))
                .Select(g => new Game(g.GameId, g.Title, g.JsonData))
            )
            .ToList();
    }
}
