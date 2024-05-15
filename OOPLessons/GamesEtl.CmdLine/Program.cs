// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using OOPClassLibrary.Games.Import;

var logger = 
    new NLogLoggerFactory()
    .CreateLogger<GenericGamesTxDataProvider>();

IGamesTxDataProvider gamesTxDataProvider =
    new GenericGamesTxDataProvider(@"./gamestx.txt", logger);

var gamesTxDataCollection = await 
    gamesTxDataProvider
    .GetGamesTxDataAsync()
    .ConfigureAwait(false);

foreach (var gameTx in gamesTxDataCollection)
{
    logger.LogDebug(gameTx.ToString());
}
