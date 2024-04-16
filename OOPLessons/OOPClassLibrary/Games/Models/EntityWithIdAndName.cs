using OOPClassLibrary.Support;

namespace OOPClassLibrary.Games.Models;

public abstract class EntityWithIdAndName : IEquatable<EntityWithIdAndName?>
{
    public string Id { get; }
    public string Name { get; }

    protected EntityWithIdAndName(string id, string name)
    {
        Id = id.GetWithTextOrThrow(nameof(id));
        Name = name.GetWithTextOrThrow(nameof(name));
    }

    public override bool Equals(object? obj) =>
        (this as IEquatable<EntityWithIdAndName?>).Equals(obj as EntityWithIdAndName);

    bool IEquatable<EntityWithIdAndName?>.Equals(EntityWithIdAndName? other) =>
        other is not null
        && GetType() == other.GetType()
        && Id == other.Id;

    public override int GetHashCode() =>
        Id.GetHashCode();
}