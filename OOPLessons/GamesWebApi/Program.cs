using GamesDal;
using GamesWebApi.Authentication;
using OOPClassLibrary.Games;

ConnectionInfo GetConnectionInfo(IConfiguration configuration)
{
    var connInfo =
         configuration
         .GetRequiredSection("Settings:ConnectionStrings")
         .Get<ConnectionInfo>()
         ?? throw new Exception("Please configure your connection info in appsettings.json");

    return connInfo;
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder
    .Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen
    (
        options =>
        {
            options
            .SwaggerDoc
            (
                "v1", 
                new() 
                { 
                    Title = "Games API", 
                    Description = "Owned games API", 
                    Version = "v1" 
                }
            );
        }
    )
    .AddSingleton(GetConnectionInfo(builder.Configuration))
    .AddSingleton
    (
        sp =>
        {
            var cnInfo = sp.GetRequiredService<ConnectionInfo>();
            IGamesDao dao = new SQLiteGamesDao(cnInfo.ConnectionString);
            return dao;
        }
    )
    .AddSingleton<IGamesService, GamesService>()
    .AddAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.Environment.ContentRootPath = Path.Combine(app.Environment.ContentRootPath, "bin", "Debug", "net8.0");
}

app.MapGet
(
    "/games",
    async (string? title, IGamesService service) =>
    {
        var txData = await service.GetGameTransactionDtosAsync(title);
        return txData;
    }
)
.AddEndpointFilter<ApiKeyAuthenticationEndpointFilter>()
.WithOpenApi();

app.Run();
