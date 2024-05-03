using GamesDal.Criterias;
using GamesDal.DbModels;
using GamesDal.Support;
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

    public Task<GameDbItem[]> GetAllGameDbItemsAsync() =>
        GetGameDbItemsAsyncImpl
        (
            "select game_id, game_title, json_data, main_game_id from games",
            null
        );

    private string BuildParamName(string paramName) =>
        $" {_parameterPrefix}{paramName}";

    private string Likelize(string paramName) =>
        $" like '%' {_stringConcatOperator} {BuildParamName(paramName)} {_stringConcatOperator} '%'";


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

    private string AddConditionForLikeIfNecessary(object? value, string fieldName, string paramName) =>
        value is null ? string.Empty : $" and {fieldName} {Likelize(paramName)}";

    private string AddConditionForOperatorIfNecessary
    (
        object? value,
        string @operator, 
        string fieldName, 
        string paramName
    ) =>
        value is null ? string.Empty : $" and {fieldName} {@operator} {BuildParamName(paramName)}";

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

        sql += AddConditionForLikeIfNecessary(id, "game_id", "p1");
        sql += AddConditionForLikeIfNecessary(title, "game_title", "p2");
        sql += AddConditionForLikeIfNecessary(json, "json_data", "p3");
        sql += AddConditionForLikeIfNecessary(mainId, "main_game_id", "p4");

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

        sql += AddConditionForLikeIfNecessary(criteria?.StoreId, "store_id", "p1");
        sql += AddConditionForLikeIfNecessary(criteria?.StoreName, "store_name", "p2");
        sql += AddConditionForLikeIfNecessary(criteria?.StoreUrl, "store_url", "p3");

        Action<DbCommand>? parametersAction =
            command =>
            {
                AddParameterIfNecessary(command, "p1", criteria?.StoreId);
                AddParameterIfNecessary(command, "p2", criteria?.StoreName);
                AddParameterIfNecessary(command, "p3", criteria?.StoreUrl);
            };

        return await GetStoreDbItemsAsyncImpl(sql, parametersAction);
    }


    public async Task<GameTransactionDbItem[]> GetTransactionDbItemsByCriteriaAsync
    (
        GameTransactionCriteria? criteria = null
    )
    {
        string sql =
            @$"
SELECT
    gt.game_tx_id,
    g.game_id,
    g.game_title,
    g.main_game_id,
    s.store_id,
    s.store_name,
    p.platform_id,
    p.platform_name,
    l.launcher_id,
    l.launcher_name,
    m.media_format_id,
    m.media_format,
    gt.acquire_date,
    gt.purchase_price
FROM
    game_transactions gt
INNER JOIN games g ON
    gt.game_id = g.game_id
INNER JOIN stores s ON
    gt.store_id = s.store_id
INNER JOIN platforms p ON
    p.platform_id = gt.platform_id
INNER JOIN launchers l ON
    l.launcher_id = gt.launcher_id
INNER JOIN media_formats m ON
    m.media_format_id = gt.media_format_id
WHERE 1 = 1 ";

        sql += AddConditionForLikeIfNecessary(criteria?.GameTitle, "g.game_title", "p1");
        sql += AddConditionForLikeIfNecessary(criteria?.StoreName, "s.store_name", "p2");
        sql += AddConditionForLikeIfNecessary(criteria?.PlatformName, "p.platform_name", "p3");
        sql += AddConditionForLikeIfNecessary(criteria?.LauncherName, "l.launcher_name", "p4");
        sql += AddConditionForLikeIfNecessary(criteria?.MediaFormat, "m.media_format", "p5");
        sql += AddConditionForOperatorIfNecessary(criteria?.AcquireDateFrom, ">=", "gt.acquire_date", "p6");
        sql += AddConditionForOperatorIfNecessary(criteria?.AcquireDateTo, "<=", "gt.acquire_date", "p7");
        sql += AddConditionForOperatorIfNecessary(criteria?.PurchasePriceFrom, ">=", "gt.purchase_price", "p8");
        sql += AddConditionForOperatorIfNecessary(criteria?.PurchasePriceTo, "<=", "gt.purchase_price", "p9");

        Action<DbCommand>? parametersAction =
            command =>
            {
                AddParameterIfNecessary(command, "p1", criteria?.GameTitle);
                AddParameterIfNecessary(command, "p2", criteria?.StoreName);
                AddParameterIfNecessary(command, "p3", criteria?.PlatformName);
                AddParameterIfNecessary(command, "p4", criteria?.LauncherName);
                AddParameterIfNecessary(command, "p5", criteria?.MediaFormat);
                AddParameterIfNecessary(command, "p6", criteria?.AcquireDateFrom.ToDateTime(), DbType.DateTime);
                AddParameterIfNecessary(command, "p7", criteria?.AcquireDateTo.ToDateTime(), DbType.DateTime);
                AddParameterIfNecessary(command, "p8", criteria?.PurchasePriceFrom, DbType.Decimal);
                AddParameterIfNecessary(command, "p9", criteria?.PurchasePriceTo, DbType.Decimal);
            };

        Func<DbDataReader, GameTransactionDbItem> mapping =
            dataReader =>
                new GameTransactionDbItem
                {
                    TransactionId = dataReader.GetString(0),
                    GameId = dataReader.GetString(1),
                    GameTitle = dataReader.GetString(2),
                    MainGameId = dataReader[3] as string,
                    StoreId = dataReader.GetString(4),
                    StoreName = dataReader.GetString(5),
                    PlatformId = dataReader.GetString(6),
                    PlatformName = dataReader.GetString(7),
                    LauncherId = dataReader.GetString(8),
                    LauncherName = dataReader.GetString(9),
                    MediaFormatId = dataReader.GetString(10),
                    MediaFormat = dataReader.GetString(11),
                    AcquireDate = dataReader.GetDateTime(12).ToDateOnly(),
                    PurchasePrice = dataReader.GetDecimal(13)
                };

        return await GetGenericDbItemsAsyncImpl(sql, parametersAction, mapping);
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
        Func<DbDataReader, GameDbItem> mapping =
            dataReader =>
            {
                string gameId = dataReader.GetString(0);
                var gameTitle = (dataReader[dataReader.GetOrdinal("game_title")] as string)!;
                var jsonData = dataReader[2] as string;
                var mainGameId = dataReader[3] as string;
                
                return
                    new GameDbItem
                    {
                        GameId = gameId,
                        Title = gameTitle,
                        JsonData = jsonData,
                        MainGameId = mainGameId
                    };
            };

        return await GetGenericDbItemsAsyncImpl(sql, parametersAction, mapping);
    }

    private async Task<StoreDbItem[]> GetStoreDbItemsAsyncImpl(string sql, Action<DbCommand>? parametersAction)
    {
        Func<DbDataReader, StoreDbItem> mapping =
            dataReader =>
            {
                string storeId = dataReader.GetString(0);
                var storeName = (dataReader[dataReader.GetOrdinal("store_name")] as string)!;
                var storeUrl = dataReader[2] as string;

                return
                    new StoreDbItem
                    {
                        StoreId = storeId,
                        StoreName = storeName,
                        StoreUrl = storeUrl
                    };
            };

        return await GetGenericDbItemsAsyncImpl(sql, parametersAction, mapping);
    }


    private async Task<T[]> GetGenericDbItemsAsyncImpl<T>
    (
        string sql, 
        Action<DbCommand>? parametersAction,
        Func<DbDataReader, T> mapping
    )
    {
        using DbConnection connection = _connectionFactory();
        await connection.OpenAsync();

        using DbCommand command = connection.CreateCommand();
        command.CommandText = sql;
        command.CommandType = CommandType.Text;

        parametersAction?.Invoke(command);

        using DbDataReader dataReader = await command.ExecuteReaderAsync();

        List<T> dbItems = new List<T>();

        while (dataReader.Read())
        {
            T dbItem = mapping(dataReader);
            dbItems.Add(dbItem);
        }

        return dbItems.ToArray();
    }
}
