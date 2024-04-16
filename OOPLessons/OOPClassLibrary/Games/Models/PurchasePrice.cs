namespace OOPClassLibrary.Games.Models;

public readonly struct PurchasePrice : IEquatable<PurchasePrice>
{
    public decimal Value { get; }

    public PurchasePrice(decimal value) =>
        Value = FixValue(value);

    public static implicit operator decimal(PurchasePrice price) =>
        price.Value;

    public static implicit operator PurchasePrice(decimal value) =>
        new PurchasePrice(value);

    public static implicit operator PurchasePrice(double value) =>
        new PurchasePrice((decimal)value);

    public static implicit operator PurchasePrice(long value) =>
        new PurchasePrice(value);

    public static bool operator ==(PurchasePrice left, PurchasePrice right) =>
        left.Equals(right);

    public static bool operator !=(PurchasePrice left, PurchasePrice right) =>
        !(left == right);

    public static bool operator >(PurchasePrice left, PurchasePrice right) =>
        left.Value > right.Value;

    public static bool operator <(PurchasePrice left, PurchasePrice right) =>
        left.Value < right.Value;

    public static bool operator >=(PurchasePrice left, PurchasePrice right) =>
        left.Value >= right.Value;

    public static bool operator <=(PurchasePrice left, PurchasePrice right) =>
        left.Value <= right.Value;

    private static decimal FixValue(decimal value) =>
     value switch
     {
         < 0m => throw new ArgumentException($"Price must be >= 0"),
         _ => Math.Round(value, 2)
     };

    public override bool Equals(object? obj) =>
        obj is PurchasePrice price && Equals(price);

    public bool Equals(PurchasePrice other) =>
        Value == other.Value;

    public override int GetHashCode() =>
        HashCode.Combine(Value);
}
