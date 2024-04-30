using GamesDal.Criterias;
using GamesDal.DbModels;
using System.Data;
using System.Data.Common;
using System.Runtime.Intrinsics;

namespace GamesDal;

public class GamesDao : IGamesDao
{
    private readonly Func<DbConnection> _connectionFactory;
    private readonly string _parameterPrefix;
    private readonly string _stringConcatOperator;

    //private readonly IGamesIDbConnectionFactory _gamesIDbConnectionFactory;


    public GamesDao
    (
        Func<DbConnection> connectionFactory,
        string parameterPrefix,
        string stringConcatOperator
    )
    {
        _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        _parameterPrefix = parameterPrefix;
        _stringConcatOperator = stringConcatOperator;
    }

    //public GamesDao(IGamesIDbConnectionFactory gamesIDbConnectionFactory)
    //{
    //    _gamesIDbConnectionFactory = gamesIDbConnectionFactory;
    //}

    public Task<GameDbItem[]> GetAllGameDbItemsAsync() =>
        GetGameDbItemsAsyncImpl
        (
            "select game_id, game_title, json_data, main_game_id from games",
            null
        );

    private string Likelize(string paramName) =>
        $" like '%' {_stringConcatOperator} {_parameterPrefix}{paramName} {_stringConcatOperator} '%'";


    private static void AddParameterIfNecessary
    (
        DbCommand command, 
        string paramName, 
        object? value,
        DbType dbType = DbType.String
    )
    {
        if (value is not null)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = paramName;
            parameter.Value = value;
            parameter.DbType = dbType;
            parameter.Direction = ParameterDirection.Input;
            command.Parameters.Add(parameter);
        }
    }

    private string AddConditionIfNecessary(object? value, string fieldName, string paramName) =>
        value is null ? string.Empty : $" and {fieldName} {Likelize(paramName)}";

    public async Task<GameDbItem[]> GetGameDbItemsByPartialTitleAsync
    (
        string? id,
        string? title,
        string? json,
        string? mainId
    )
    {
        string sql =
            @$"select game_id, game_title, json_data, main_game_id 
               from games where 1 = 1 ";

        sql += AddConditionIfNecessary(id, "game_id", "p1");
        sql += AddConditionIfNecessary(title, "game_title", "p2");
        sql += AddConditionIfNecessary(json, "json_data", "p3");
        sql += AddConditionIfNecessary(mainId, "main_game_id", "p4");

        Action<DbCommand>? parametersAction =
            command =>
            {
                AddParameterIfNecessary(command, "p1", id);
                AddParameterIfNecessary(command, "p2", title);
                AddParameterIfNecessary(command, "p3", json);
                AddParameterIfNecessary(command, "p4", mainId);
            };

        return await GetGameDbItemsAsyncImpl(sql, parametersAction);
    }

    public async Task<StoreDbItem[]> GetStoreDbItemsByCriteriaAsync
    (
        StoreCriteria? criteria = null
    )
    {
        string sql =
            @$"select store_id, store_name, store_url    
               from stores where 1 = 1 ";

        sql += AddConditionIfNecessary(criteria?.StoreId, "store_id", "p1");
        sql += AddConditionIfNecessary(criteria?.StoreName, "store_name", "p2");
        sql += AddConditionIfNecessary(criteria?.StoreUrl, "store_url", "p3");

        Action<DbCommand>? parametersAction =
            command =>
            {
                AddParameterIfNecessary(command, "p1", criteria?.StoreId);
                AddParameterIfNecessary(command, "p2", criteria?.StoreName);
                AddParameterIfNecessary(command, "p3", criteria?.StoreUrl);
            };

        return await GetStoreDbItemsAsyncImpl(sql, parametersAction);
    }

    #region roba da evitare

    //// PROBLEMA: n + 1 select
    //public async Task<List<Game>> GetAllGamesAsync_TO_AVOID()
    //{
    //    using SQLiteConnection connection = new SQLiteConnection(_connectionString);
    //    await connection.OpenAsync();
    //    using SQLiteCommand mainCommand = new SQLiteCommand(connection);

    //    mainCommand.CommandText = "select game_id, game_title, json_data from games where main_game_id is null";
    //    mainCommand.CommandType = System.Data.CommandType.Text;

    //    using var mainDataReader = await mainCommand.ExecuteReaderAsync();

    //    List<Game> mainGames = new List<Game>();

    //    while (mainDataReader.Read())
    //    {
    //        string gameId = mainDataReader.GetString(0);
    //        var gameTitle = (mainDataReader[mainDataReader.GetOrdinal("game_title")] as string)!;
    //        var jsonData = mainDataReader[2] as string;
    //        Game game = new Game(gameId, gameTitle, jsonData);
    //        mainGames.Add(game);
    //    }


    //    foreach (var mainGame in mainGames)
    //    {
    //        using SQLiteCommand command = new SQLiteCommand(connection);

    //        command.CommandText =
    //            @$"select game_id, game_title, json_data from games 
    //               where main_game_id = '{mainGame.GameId}'";

    //        command.CommandType = System.Data.CommandType.Text;

    //        using var dataReader = await command.ExecuteReaderAsync();

    //        while (dataReader.Read())
    //        {
    //            string gameId = dataReader.GetString(0);
    //            var gameTitle = (dataReader[dataReader.GetOrdinal("game_title")] as string)!;
    //            var jsonData = dataReader[2] as string;
    //            mainGame.AddNewDlc(gameId, gameTitle, jsonData);
    //        }
    //    }

    //    return mainGames;
    //}

    //public async Task<List<Game>> GetAllGamesAsync_Better_but_TO_AVOID()
    //{
    //    using SQLiteConnection connection = new SQLiteConnection(_connectionString);
    //    await connection.OpenAsync();

    //    using SQLiteCommand mainCommand = new SQLiteCommand(connection);

    //    mainCommand.CommandText = "select game_id, game_title, json_data from games where main_game_id is null";
    //    mainCommand.CommandType = System.Data.CommandType.Text;

    //    using var mainDataReader = await mainCommand.ExecuteReaderAsync();

    //    List<Game> mainGames = new List<Game>();

    //    while (mainDataReader.Read())
    //    {
    //        string gameId = mainDataReader.GetString(0);
    //        var gameTitle = (mainDataReader[mainDataReader.GetOrdinal("game_title")] as string)!;
    //        var jsonData = mainDataReader[2] as string;
    //        Game game = new Game(gameId, gameTitle, jsonData);
    //        mainGames.Add(game);
    //    }




    //    using SQLiteCommand dlcCmd = new SQLiteCommand(connection);

    //    dlcCmd.CommandText = "select game_id, game_title, json_data, main_game_id from games where main_game_id is not null";
    //    dlcCmd.CommandType = System.Data.CommandType.Text;

    //    using var dlcDataReader = await dlcCmd.ExecuteReaderAsync();

    //    Dictionary<string, List<Game>> dlcGames = new Dictionary<string, List<Game>>();

    //    while (dlcDataReader.Read())
    //    {
    //        string gameId = dlcDataReader.GetString(0);
    //        var gameTitle = (dlcDataReader[dlcDataReader.GetOrdinal("game_title")] as string)!;
    //        var jsonData = dlcDataReader[2] as string;
    //        var mainGameId = dlcDataReader.GetString(3);
    //        Game game = new Game(gameId, gameTitle, jsonData);
    //        if (dlcGames.TryGetValue(mainGameId, out var list))
    //        {
    //            list.Add(game);
    //        }
    //        else
    //        {
    //            list = new List<Game>() { game };
    //            dlcGames.Add(mainGameId, list);
    //        }
    //    }


    //    foreach (var mainGame in mainGames)
    //    {
    //        if (dlcGames.TryGetValue(mainGame.GameId, out var dlcList))
    //        {
    //            foreach (var dlcGame in dlcList)
    //                mainGame.AddNewDlc(dlcGame.GameId, dlcGame.Title, dlcGame.JsonData);
    //        }
    //    }

    //    return mainGames;
    //}

    #endregion

    private async Task<GameDbItem[]> GetGameDbItemsAsyncImpl(string sql, Action<DbCommand>? parametersAction)
    {
        using DbConnection connection = _connectionFactory();
        await connection.OpenAsync();

        using DbCommand command = connection.CreateCommand();
        command.CommandText = sql;
        command.CommandType = CommandType.Text;

        parametersAction?.Invoke(command);

        using DbDataReader dataReader = await command.ExecuteReaderAsync();

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

        return games.ToArray();
    }

    private async Task<StoreDbItem[]> GetStoreDbItemsAsyncImpl(string sql, Action<DbCommand>? parametersAction)
    {
        using DbConnection connection = _connectionFactory();
        await connection.OpenAsync();

        using DbCommand command = connection.CreateCommand();
        command.CommandText = sql;
        command.CommandType = CommandType.Text;

        parametersAction?.Invoke(command);

        using DbDataReader dataReader = await command.ExecuteReaderAsync();

        List<StoreDbItem> games = new List<StoreDbItem>();

        while (dataReader.Read())
        {
            string storeId = dataReader.GetString(0);
            var storeName = (dataReader[dataReader.GetOrdinal("store_name")] as string)!;
            var storeUrl = dataReader[2] as string;

            StoreDbItem game =
                new StoreDbItem
                {
                    StoreId = storeId,
                    StoreName = storeName,
                    StoreUrl = storeUrl
                };

            games.Add(game);
        }

        return games.ToArray();
    }

}
