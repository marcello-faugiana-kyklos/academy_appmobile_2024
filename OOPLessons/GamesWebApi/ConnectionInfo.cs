
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle





// Configure the HTTP request pipeline.




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