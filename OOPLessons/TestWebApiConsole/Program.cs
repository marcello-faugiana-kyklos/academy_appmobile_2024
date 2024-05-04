
using Flurl;
using Flurl.Http;
using OOPClassLibrary.Games.Dtos;

string title = "elden";
string urlWebApi = $"http://localhost:5000/games";

try
{
    await
        urlWebApi
        .SetQueryParam("title", title)
        .GetJsonAsync<GameTransactionDto[]>();
}
catch (FlurlHttpException ex)
{
    Console.WriteLine(ex.GetBaseException().Message);
}

try
{
    await
        urlWebApi
        .SetQueryParam("title", title)
        .WithHeader("X-Api-Key", "WrongApiKey")
        .GetJsonAsync<GameTransactionDto[]>();
}
catch (FlurlHttpException ex)
{
    Console.WriteLine(ex.GetBaseException().Message);
}

Console.WriteLine();

// this should work!!
var txData = await 
    urlWebApi
    .SetQueryParam("title", title)
    .WithHeader("X-Api-Key", "MySuperSecretApiKey")
    .GetJsonAsync<GameTransactionDto[]>();

foreach (var tx in txData)
{
    Console.WriteLine(tx.Game.GameTitle);
}
