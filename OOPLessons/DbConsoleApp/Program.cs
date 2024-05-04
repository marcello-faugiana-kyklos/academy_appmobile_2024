using GamesDal;
using GamesDal.Criterias;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OOPClassLibrary.Games;
using System.Data.Common;
using System.Data.SQLite;


ConnectionInfo ReadConnectionData()
{
    IConfigurationRoot config =
        new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

    var connInfo =
        config
        .GetRequiredSection("Settings:ConnectionStrings")
        .Get<ConnectionInfo>()
        ?? throw new Exception("Please configure your connection info in appsettings.json");

    return connInfo;
}


IServiceProvider ComposeRoot()
{
    var connInfo = ReadConnectionData();

    var collection = 
        new ServiceCollection()
        .AddSingleton(connInfo)
        .AddSingleton
        (
            sp =>
            {
                var cnInfo = sp.GetRequiredService<ConnectionInfo>();
                IGamesDao dao = cnInfo.IsSQLite ?
                    new SQLiteGamesDao(cnInfo.ConnectionString)
                    : new SQLServerGamesDao(cnInfo.ConnectionString);
                return dao;
            }
        )
        .AddSingleton<IGamesService, GamesService>();

    return collection.BuildServiceProvider();
}

IServiceProvider serviceProvider = ComposeRoot();

IGamesDao dao = serviceProvider.GetRequiredService<IGamesDao>();

IGamesService gamesService = serviceProvider.GetRequiredService<IGamesService>();

IGamesDao dao2 = serviceProvider.GetRequiredService<IGamesDao>();

var areEquals = dao == dao2;

var tx = await
        dao
        .GetTransactionDbItemsByCriteriaAsync
        (
            new GameTransactionCriteria
            {
                AcquireDateFrom = new DateOnly(2024, 3, 1),
                PurchasePriceFrom = 1.1m,
                PurchasePriceTo = 10m,
                GameTitle = "mat"
            }
        );

var txDto = await
    gamesService
    .GetGameTransactionDtosAsync()
    .ConfigureAwait(false);

var games = await gamesService.GetAllGamesAsync();

Console.WriteLine($"gameId | gameTitle");
games
    .ToList()
    .ForEach
    (
        g =>
            {
                Console.WriteLine($"{g.GameId} | {g.Title}");
                foreach (var dlc in g.DlcGames)
                {
                    Console.WriteLine($"   {g.GameId} | {g.Title}");
                }
            }
    );


var zeldaGames = await dao.GetGameDbItemsByPartialTitleAsync(id: "fallout", mainId: "fallout");


var allStores = await dao.GetStoreDbItemsByCriteriaAsync();

Console.ReadLine();

//var games2 =  await dao.GetAllGamesAsync_TO_AVOID();

//var games3 = await dao.GetAllGamesAsync_Better_but_TO_AVOID();

class SqliteGameConnectionFctory : IGamesIDbConnectionFactory
{
    private readonly string _connectionString;

    public SqliteGameConnectionFctory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DbConnection CreateConnection() =>
        new SQLiteConnection(_connectionString);
}


class ConnectionInfo
{
    public string DbProvider { get; set; } = null!;
    public string ConnectionString { get; set; } = null!;
    public string Schema { get; set; } = null!;

    public bool IsSQLite =>
        string.Equals("SQLite", DbProvider, StringComparison.OrdinalIgnoreCase);

    public bool IsSqlServer =>
        string.Equals("SqlServer", DbProvider, StringComparison.OrdinalIgnoreCase);
}
