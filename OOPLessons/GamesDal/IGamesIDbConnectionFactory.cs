using System.Data.Common;

namespace GamesDal;

public interface IGamesIDbConnectionFactory
{
    DbConnection CreateConnection();
}
