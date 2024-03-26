namespace OOPClassLibrary;


// public:    visibilità dappertutto
// internal:  visibilità limitata all'interno dello stesso assembly
// private:   visibilità limitata al tipo nel quale è definito l'elemento

public class Point2D : object
{
    public static readonly Point2D Origin = new Point2D();


    private double _x;
    private double _y;

    public double X
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

    public double Y
    {
        get => _y;
        set => _y = value;
    }
    

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

    public override string ToString() =>
        $"({X}, {Y})";
    //"(" + X.ToString() + ", " + Y.ToString() + ")";
    //base.GetType().ToString();

    public double DistanceFromOrigin() =>
        DistanceFromToOtherPoint(Origin);

    public double DistanceFromToOtherPoint(Point2D other)
    { 
        double dX = Math.Pow(X - other.X, 2);
        double dY = Math.Pow(other.Y - Y, 2);

        return Math.Sqrt(dX + dY);
    }
}


class Figure
{
    public Figure()
    {
        Point2D point2D = new Point2D();
    }
}