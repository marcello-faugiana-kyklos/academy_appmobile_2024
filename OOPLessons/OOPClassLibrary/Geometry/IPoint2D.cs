namespace OOPClassLibrary.Geometry;

public interface IPoint2D
{
    double X { get; }
    double Y { get; }

    double DistanceFromOrigin();
    double DistanceFromOtherPoint(IPoint2D other);
    bool Equals(IPoint2D? other);
}
