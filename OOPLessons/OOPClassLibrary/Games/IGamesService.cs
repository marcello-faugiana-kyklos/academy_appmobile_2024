using OOPClassLibrary.Games.Models;

namespace OOPClassLibrary.Games
{
    public interface IGamesService
    {
        Task<List<Game>> GetAllGamesAsync();
    }
}