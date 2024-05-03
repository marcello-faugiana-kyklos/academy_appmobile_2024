using GamesDal;
using GamesDal.Criterias;
using OOPClassLibrary.Games;
using System.Data.Common;
using System.Data.SQLite;

string connStr = @"Data Source=./../../../../../databases/SQLite/academy_mobile_2024.db;Version=3;FailIfMissing=false;Foreign Keys=True";

//IGamesDao dao = new GamesDao(new SqliteGameConnectionFctory(connStr));
IGamesDao dao = new SQLiteGamesDao(connStr);

IGamesService gamesService = new GamesService(dao);

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



var games = await gamesService.GetAllGamesAsync();

Console.WriteLine($"gameId | gameTitle");
games
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
        new SQLiteConnection( _connectionString );
}

