namespace OOPClassLibrary.Geometry;


// public:    visibilità dappertutto
// internal:  visibilità limitata all'interno dello stesso assembly
// private:   visibilità limitata al tipo nel quale è definito l'elemento
// protected: visibilità limitata alle sottoclassi, anche in assembly diversi


/*
             A
            ^ ^  ^
           B   D  E
          ^   
          C
         ^
         F

                 A
               /   \
              B     C
               \   /  \
                 D     F
                  \   /
                    E


    C <: A

    F <: C

    F <: A

    X <: X

*/

public class Point2D    //: IEquatable<Point2D>
{
    private class Point2DZero : Point2D
    {
        public override double X
        {
            get => _x;
            set
            {
                throw new InvalidOperationException("X cannot be set for Origin");
            }
        }

        public override double Y
        {
            get => base.Y;
            set
            {
                throw new InvalidOperationException("Y cannot be set for Origin");
            }
        }

        public override double DistanceFromOrigin() =>
            0d;

    }

    /// <summary>
    /// This point represents the origin 0, 0
    /// You cannot alter X or Y. You'll get an InvalidOperationException if you try.
    /// </summary>
    public static readonly Point2D Origin = new Point2DZero();

    #region X and Y

    protected double _x;
    private double _y;

    public virtual double X
    {
        get
        {
            return _x;
        }
        set
        {
            _x = value;
        }
    }

    public virtual double Y
    {
        get => _y;
        set => _y = value;
    }

    #endregion

    #region Constructors

    public Point2D(double x, double y)
    {
        _x = x;
        _y = y;
    }

    public Point2D(double xy) : this(xy, xy)
    {
    }

    public Point2D() : this(0d, 0d)
    {
    }

    #endregion

    public override string ToString() =>
        $"({X}, {Y})";
    //"(" + X.ToString() + ", " + Y.ToString() + ")";

    public virtual double DistanceFromOrigin() =>
        DistanceFromOtherPoint(Origin);

    public double DistanceFromOtherPoint(Point2D other)
    {
        double dX = Math.Pow(X - other.X, 2);
        double dY = Math.Pow(other.Y - Y, 2);

        return Math.Sqrt(dX + dY);
    }

    //public override int GetHashCode()
    //{
    //    /*
    //        a   b |  a & b  | a | b | a ^ x
    //        0   0 |    0    |   0   |   0
    //        0   1 |    0    |   1   |   1
    //        1   0 |    0    |   1   |   1
    //        1   1 |    1    |   1   |   0


    //    */
    //    return (17 * _x.GetHashCode()) ^ (17 * _y.GetHashCode());
    //}



    public override bool Equals(object? obj) =>
        Equals(obj as Point2D);

    public bool Equals(Point2D? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return
            X == other.X
            && Y == other.Y;
    }

    public override int GetHashCode() =>
        HashCode.Combine(X, Y);
}