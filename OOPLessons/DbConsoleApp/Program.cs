using DbConsoleApp;

string connStr = @"Data Source=./../../../../../databases/SQLite/academy_mobile_2024.db;Version=3;FailIfMissing=false;Foreign Keys=True";

GamesDao dao = new GamesDao(connStr);

var games = await dao.GetAllGamesAsync();

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


var games2 =  await dao.GetAllGamesAsync_TO_AVOID();

var games3 = await dao.GetAllGamesAsync_Better_but_TO_AVOID();



Console.ReadLine();