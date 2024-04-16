using OOPClassLibrary.Support;

namespace OOPClassLibrary.Games.Models;

public class Platform : EntityWithIdAndName, IEquatable<Platform?>
{
    public static Platform NintendoSwitch { get; } = new Platform("nsw", "Nintendo Switch");
    public static Platform PlayStation { get; } = new Platform("ps", "Sony Playstation");
    public static Platform PC { get; } = new Platform("PC", "Personal Computer");

    public Platform(string id, string name) : base(id, name)
    {
    }

    private bool Equals(IEquatable<EntityWithIdAndName?> @this, Platform? other) =>
        @this.Equals(other);

    public bool Equals(Platform? other) =>
        Equals(this, other);
}
