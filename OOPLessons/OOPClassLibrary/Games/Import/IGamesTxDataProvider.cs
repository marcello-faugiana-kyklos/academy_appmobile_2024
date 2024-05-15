using OOPClassLibrary.Games.Dtos;

namespace OOPClassLibrary.Games.Import;

public interface IGamesTxDataProvider
{
    ValueTask<GameTxImportData[]> GetGamesTxDataAsync();
}
