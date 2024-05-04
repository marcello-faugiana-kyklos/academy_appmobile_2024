using OOP.Common;

namespace OOPClassLibrary.Games.Models;

public class Store : EntityWithIdAndName<Store>, IEquatable<Store?>
{
    public Uri? ShopUrl { get; }

    public static Store Steam { get; }
    public static Store EpicGames { get; }


    static Store()
    {
        Steam = new Store("steam", "Steam Store", "https://store.steampowered.com/");
        EpicGames = new Store("epic-games", "Epic Games Store", "https://store.epicgames.com/");
        AddWellKnownValues();
    }

    public Store
    (
        string id,
        string name,
        string? shopUrl
    ) :
        this
        (
            id,
            name,
            shopUrl.IsNullOrEmpty() ? null : new Uri(shopUrl!)
        )
    {
    }

    public Store(string id, string name, Uri? shopUr = null) : base(id, name)
    {
        ShopUrl = shopUr;
    }

    private bool Equals(IEquatable<EntityWithIdAndName<Store>?> @this, Store? other) =>
        @this.Equals(other);

    public bool Equals(Store? other) =>
        Equals(this, other);
}
