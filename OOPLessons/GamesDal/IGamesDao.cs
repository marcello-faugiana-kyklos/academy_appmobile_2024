using GamesDal.Criterias;
using GamesDal.DbModels;
using System.Data.Common;

namespace GamesDal;

public interface IGamesDao
{
    Task<GameDbItem[]> GetAllGameDbItemsAsync();

    Task<GameDbItem[]> GetGameDbItemsByPartialTitleAsync
    (
        string? id = null,
        string? title = null,
        string? json = null,
        string? mainId = null
    );

    Task<StoreDbItem[]> GetStoreDbItemsByCriteriaAsync
    (
        StoreCriteria? criteria = null
    );

    Task<GameTransactionDbItem[]> GetTransactionDbItemsByCriteriaAsync
    (
        GameTransactionCriteria? criteria = null
    );
}