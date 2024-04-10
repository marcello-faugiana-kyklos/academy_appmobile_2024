namespace OOPClassLibrary.Geometry;

public class Point2DImmutable : IPoint2D
{
    public static IPoint2D Origin { get; } =
        new Point2DImmutable(0, 0);

    public double X { get; }
    public double Y { get; }

    public Point2DImmutable(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double DistanceFromOrigin() =>
        Point2DHelpers.ComputedDistance(this, Origin);

    public double DistanceFromOtherPoint(IPoint2D other) =>
        Point2DHelpers.ComputedDistance(this, other);

    public override bool Equals(object? obj) =>
        Equals(obj as IPoint2D);

    public bool Equals(IPoint2D? other) =>
        Point2DHelpers.PointsAreEqual(this, other);

    public override int GetHashCode() =>
        HashCode.Combine(X, Y);

    public override string ToString() =>
        $"I({X}, {Y})";
}