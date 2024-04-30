using GamesDal;
using System.Data.SqlClient;

class SQLServerGamesDao : GamesDao
{
    public SQLServerGamesDao(string connectionString) :
        base
        (
            () => new SqlConnection(connectionString),
            "@",
            "+"
        )
    {
    }
}
