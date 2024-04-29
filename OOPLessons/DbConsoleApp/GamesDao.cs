using DbConsoleApp.DbModels;
using OOPClassLibrary.Games.Models;
using System.Data.SQLite;

namespace DbConsoleApp;

internal class GamesDao
{
    private readonly string _connectionString;

    public GamesDao(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<Game>> GetAllGamesAsync()
    {
        using SQLiteConnection connection = new SQLiteConnection(_connectionString);
        await connection.OpenAsync();
        using SQLiteCommand command = new SQLiteCommand(connection);

        command.CommandText = "select game_id, game_title, json_data, main_game_id from games";
        command.CommandType = System.Data.CommandType.Text;

        using var dataReader = await command.ExecuteReaderAsync();

        List<GameDbItem> games = new List<GameDbItem>();

        while (dataReader.Read())
        {
            string gameId = dataReader.GetString(0);
            var gameTitle = (dataReader[dataReader.GetOrdinal("game_title")] as string)!;
            var jsonData = dataReader[2] as string;
            var mainGameId = dataReader[3] as string;
            GameDbItem game =
                new GameDbItem
                {
                    GameId = gameId,
                    Title = gameTitle,
                    JsonData = jsonData,
                    MainGameId = mainGameId
                };

            games.Add(game);
        }

        var mainGames = games.Where(g => g.MainGameId is null);
        var dlcGames = games.Where(g => g.MainGameId is not null);


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


        //List<Game> resultGames = new List<Game>();
        //foreach (var mainGame in mainGames)
        //{
        //    Game game = new Game(mainGame.GameId, mainGame.Title, mainGame.JsonData);

        //    var dlcForGame = dlcGames.Where(g => g.MainGameId == mainGame.GameId);

        //    foreach (var dlc in dlcForGame)
        //    {
        //        game.AddNewDlc(dlc.GameId, dlc.Title, dlc.JsonData);
        //    }

        //    resultGames.Add(game);
        //}

        //return resultGames;
    }


    // PROBLEMA: n + 1 select
    public async Task<List<Game>> GetAllGamesAsync_TO_AVOID()
    {
        using SQLiteConnection connection = new SQLiteConnection(_connectionString);
        await connection.OpenAsync();
        using SQLiteCommand mainCommand = new SQLiteCommand(connection);

        mainCommand.CommandText = "select game_id, game_title, json_data from games where main_game_id is null";
        mainCommand.CommandType = System.Data.CommandType.Text;

        using var mainDataReader = await mainCommand.ExecuteReaderAsync();

        List<Game> mainGames = new List<Game>();

        while (mainDataReader.Read())
        {
            string gameId = mainDataReader.GetString(0);
            var gameTitle = (mainDataReader[mainDataReader.GetOrdinal("game_title")] as string)!;
            var jsonData = mainDataReader[2] as string;
            Game game = new Game(gameId, gameTitle, jsonData);
            mainGames.Add(game);
        }


        foreach (var mainGame in mainGames)
        {
            using SQLiteCommand command = new SQLiteCommand(connection);

            command.CommandText =
                @$"select game_id, game_title, json_data from games 
                   where main_game_id = '{mainGame.GameId}'";

            command.CommandType = System.Data.CommandType.Text;

            using var dataReader = await command.ExecuteReaderAsync();

            while (dataReader.Read())
            {
                string gameId = dataReader.GetString(0);
                var gameTitle = (dataReader[dataReader.GetOrdinal("game_title")] as string)!;
                var jsonData = dataReader[2] as string;
                mainGame.AddNewDlc(gameId, gameTitle, jsonData);
            }
        }

        return mainGames;
    }

    public async Task<List<Game>> GetAllGamesAsync_Better_but_TO_AVOID()
    {
        using SQLiteConnection connection = new SQLiteConnection(_connectionString);
        await connection.OpenAsync();

        using SQLiteCommand mainCommand = new SQLiteCommand(connection);

        mainCommand.CommandText = "select game_id, game_title, json_data from games where main_game_id is null";
        mainCommand.CommandType = System.Data.CommandType.Text;

        using var mainDataReader = await mainCommand.ExecuteReaderAsync();

        List<Game> mainGames = new List<Game>();

        while (mainDataReader.Read())
        {
            string gameId = mainDataReader.GetString(0);
            var gameTitle = (mainDataReader[mainDataReader.GetOrdinal("game_title")] as string)!;
            var jsonData = mainDataReader[2] as string;
            Game game = new Game(gameId, gameTitle, jsonData);
            mainGames.Add(game);
        }




        using SQLiteCommand dlcCmd = new SQLiteCommand(connection);

        dlcCmd.CommandText = "select game_id, game_title, json_data, main_game_id from games where main_game_id is not null";
        dlcCmd.CommandType = System.Data.CommandType.Text;

        using var dlcDataReader = await dlcCmd.ExecuteReaderAsync();

        Dictionary<string, List<Game>> dlcGames = new Dictionary<string, List<Game>>();

        while (dlcDataReader.Read())
        {
            string gameId = dlcDataReader.GetString(0);
            var gameTitle = (dlcDataReader[dlcDataReader.GetOrdinal("game_title")] as string)!;
            var jsonData = dlcDataReader[2] as string;
            var mainGameId = dlcDataReader.GetString(3);
            Game game = new Game(gameId, gameTitle, jsonData);
            if (dlcGames.TryGetValue(mainGameId, out var list))
            {
                list.Add(game);
            }
            else
            {
                list = new List<Game>() { game };
                dlcGames.Add(mainGameId, list);
            }
        }


        foreach (var mainGame in mainGames)
        {
            if (dlcGames.TryGetValue(mainGame.GameId, out var dlcList))
            {
                foreach (var dlcGame in dlcList)
                    mainGame.AddNewDlc(dlcGame.GameId, dlcGame.Title, dlcGame.JsonData);
            }
        }

        return mainGames;
    }
}
