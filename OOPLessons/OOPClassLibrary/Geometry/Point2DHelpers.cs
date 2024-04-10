namespace OOPClassLibrary.Geometry;

public static class Point2DHelpers
{
    public static double ComputedDistance(IPoint2D p1, IPoint2D p2)
    {
        double dX = Math.Pow(p1.X - p2.X, 2);
        double dY = Math.Pow(p1.Y - p2.Y, 2);

        return Math.Sqrt(dX + dY);
    }

    public static bool PointsAreEqual(IPoint2D? p1, IPoint2D? p2)
    {
        if (p1 is null || p2 is null)
        {
            return false;
        }

        if (ReferenceEquals(p1, p2))
        {
            return true;
        }

        return
            p1.X == p2.X
            && p1.Y == p2.Y;
    }
}
