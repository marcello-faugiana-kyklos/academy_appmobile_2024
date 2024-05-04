using OOP.Common;
using System.Collections.Concurrent;
using System.Reflection;

namespace OOPClassLibrary.Games.Models;

public record Store2
(
    string StoreId,
    string StoreName,
    Uri? ShopUrl
)
{
    public static Store2 Steam { get; }
    public static Store2 EpicGames { get; }

    static Store2()
    {
        Steam = new Store2("steam", "Steam Store", new Uri("https://store.steampowered.com/"));
        EpicGames = new Store2("epic-games", "Epic Games Store", new Uri("https://store.epicgames.com/"));
        Steam.AddKnownValuesForType(x =>  x.StoreId);
    }

    public static Store2 GetOrAddStore
    (
        string storeId,
        string? storeName = null,
        Uri? url = null
    ) =>
        storeId
        .GetOrAddItem
        (
            () => new Store2(storeId, storeName!, url)
        );
}

public record Launcher2
(
    string LauncherId,
    string LauncherName
)
{
    public static Launcher2 Steam { get; }
    public static Launcher2 EpicGames { get; }

    static Launcher2()
    {
        Steam = new Launcher2("steam", "Steam Launcher");
        EpicGames = new Launcher2("epic-games", "Epic Games Launcher");
        Steam.AddKnownValuesForType(x => x.LauncherId);
        // AddWellKnownValues();
    }
}

public record Plaftorm2
(
    string PlaftormId,
    string PlaftormName
) : IEquatable<Plaftorm2>
{
    public static Plaftorm2 PC { get; }
    public static Plaftorm2 XboxSeriesX_S { get; }

    static Plaftorm2()
    {
        PC = new Plaftorm2("pc", "Personal Computer");
        XboxSeriesX_S = new Plaftorm2("xbox-x-s", "Xbox Series X/S");
        PC.AddKnownValuesForType(x => x.PlaftormId);
    }

    public virtual bool Equals(Plaftorm2? other) =>
        this
        .AreEquals
        (
            other, 
            (x, y) => x.PlaftormId.Equals(y.PlaftormId, StringComparison.InvariantCultureIgnoreCase)
        );

    public override int GetHashCode() =>
        PlaftormId?.GetHashCode() ?? 0;
}


internal static class ItemsExtensions
{
    internal static readonly ConcurrentDictionary<Type, ConcurrentDictionary<string, object>> 
        _globalCache;

    static ItemsExtensions()
    {
        _globalCache = new ConcurrentDictionary<Type, ConcurrentDictionary<string, object>>();
    }

    internal static void AddKnownValuesForType<T>
    (
        this T _, 
        Func<T, string> keyExtractor
    ) 
        where T : class
    {
        Type type = typeof(T);

        PropertyInfo[] staticProps =
            type
            .GetProperties(BindingFlags.Public | BindingFlags.Static)
            .Where(p => p.PropertyType == type)
            .ToArray();

        ConcurrentDictionary<string, object> wellKnownTypeForT =
            _globalCache
            .GetOrAdd
            (
                type,
                _ => new ConcurrentDictionary<string, object>()
            );

        foreach (PropertyInfo prop in staticProps)
        {
            T propValue = (prop.GetValue(null) as T)!;
            wellKnownTypeForT.TryAdd(keyExtractor(propValue), propValue);
        }
    }

    internal static T GetOrAddItem<T>(this string itemId, Func<T> factory) where T : class
    {
        itemId.GetWithTextOrThrow(nameof(itemId));

        ConcurrentDictionary<string, object> wellKnownTypeForT =
            _globalCache
            .GetOrAdd
            (
                typeof(T),
                _ => new ConcurrentDictionary<string, object>()
            );

        return (wellKnownTypeForT.GetOrAdd(itemId, _ => factory()!) as T)!;
    }

    public static bool AreEquals<T>(this T? item1, T? item2, Func<T, T, bool> comparison) 
        where T : class
    {
        if (item1 is null || item2 is null)
        {
            return false;
        }

        if (ReferenceEquals(item1, item2))
        {
            return true;
        }

        return comparison(item1, item2);
    }
}


