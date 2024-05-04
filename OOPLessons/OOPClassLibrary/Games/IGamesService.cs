using OOPClassLibrary.Games.Dtos;
using OOPClassLibrary.Games.Models;

namespace OOPClassLibrary.Games;

public interface IGamesService
{
    Task<Game[]> GetAllGamesAsync();
    ValueTask<GameTransactionDto[]> GetGameTransactionDtosAsync(string? gameTitle = null);
}