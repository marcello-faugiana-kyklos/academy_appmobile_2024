namespace OOPClassLibrary.Games.Models;

public class Launcher : EntityWithIdAndName<Launcher>, IEquatable<Launcher?>
{
    public static Launcher NintendoSwitch { get; } = new Launcher("nsw", "Nintendo Switch");
    public static Launcher PlayStation { get; } = new Launcher("ps", "Sony Playstation");
    public static Launcher Steam { get; } = new Launcher("steam", "Steam Launcher");
    public static Launcher EpicGames { get; } = new Launcher("epic-games", "Epic Games Launcher");

    public Launcher(string id, string name) : base(id, name)
    {
    }

    static Launcher()
    {
        AddWellKnownValues();
    }

    private bool Equals(IEquatable<EntityWithIdAndName<Launcher>?> @this, Launcher? other) =>
        @this.Equals(other);

    public bool Equals(Launcher? other) =>
        Equals(this, other);
}
