using GamesDal;
using System.Data.SQLite;

class SQLiteGamesDao : GamesDao
{
    public SQLiteGamesDao(string connectionString) : 
        base
        (
            () => new SQLiteConnection(connectionString), 
            ":", 
            "||"
        )
    {
    }
}
