using OOPClassLibrary.Support;

namespace OOPClassLibrary.Games.Models;

public class Store : EntityWithIdAndName, IEquatable<Store?>
{
    public Uri? ShopUrl { get; }

    public static Store Steam { get; } = new Store("steam", "Steam Store", "https://store.steampowered.com/");
    public static Store EpicGames { get; } = new Store("epic-games", "Epic Games Store", "https://store.epicgames.com/");

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

    private bool Equals(IEquatable<EntityWithIdAndName?> @this, Store? other) =>
        @this.Equals(other);

    public bool Equals(Store? other) =>
        Equals(this, other);
}
