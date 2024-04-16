using FluentAssertions;
using OOPClassLibrary.Geometry;
using OOPClassLibrary.Patterns;
using Xunit.Abstractions;

namespace OOPClassLibraryTests;

public class Point2DTests
{
    private readonly ITestOutputHelper _output;

    public Point2DTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Test1()
    {
        Point2D point0_0 = new Point2D();
        Point2D point1_1 = new Point2D(1);
        Point2D point1_2 = new Point2D(1, 2);

        point0_0.X.Should().Be(0);

        Point2D p1 = new Point2D(3, 4);

        p1.DistanceFromOrigin().Should().Be(5d);


        Point2D.Origin.X = 10;
        Point2D.Origin.Y = 100;

        p1.DistanceFromOrigin().Should().Be(5d);



        Point2D p2 = new Point2D(1, 1);
        Point2D p3 = new Point2D(4, 5);

        p2.DistanceFromOtherPoint(p3).Should().Be(5d);


        //point0_0.Y.Should().Be(0);

        //point1_1.X.Should().Be(1);
        //point1_1.Y.Should().Be(1);

        //point1_2.X.Should().Be(1);
        //point1_2.Y.Should().Be(2);
    }

    [Fact]
    public void Test_problem_memory_allocation_with_DistanceFromOrigin()
    {
        Point2D p1 = new Point2D(3, 4);

        for (int i = 0; i < 100_000_000; i++)
        {
            p1.DistanceFromOrigin();
        }
    }

    [Fact]
    public void TestInnerClass()
    {
        Point2D p1 = new Point2D(3, 3);

        object p2 = new Point2D(3);

        Point2D p3 = new Point2D(3, 3);

        p2.Equals(p1).Should().BeTrue();
        p1.Equals(p2).Should().BeTrue();

        p1.Equals("Sorpresa").Should().BeFalse();

        p1.Equals(p3);
    }

    [Fact]
    public void TestDateTime()
    {
        DateTime dt1 = new DateTime(2025, 1, 1);
        DateTime dt2 = new DateTime(2026, 1, 1);

        double days = (dt2 - dt1).TotalDays;

        //var nextYear = dt.AddYears(1);

        //nextYear.Year.Should().Be(2025);
        //nextYear.Month.Should().Be(3);
        //nextYear.Day.Should().Be(1);

    }

    [Fact]
    public void TestIPoint2D_1()
    {
        object p1 = new Point2D(4, 6);
        IPoint2D p2 = new Point2DImmutable(4, 6);

        p1.Equals(p2).Should().BeTrue();
        p2.Equals(p1).Should().BeTrue();

        (p1 as Point2D)!.X = 9;
        p1.Equals(p2).Should().BeFalse();
        p2.Equals(p1).Should().BeFalse();
    }

    [Fact]
    public void TestIPoint2D_2()
    {
        void PrintPoint(IPoint2D p)
        {
            _output.WriteLine(p.DistanceFromOrigin().ToString());
        }

        IPoint2D[] points =
            [
                new Point2D(3),
                new Point2DImmutable(3, 3),
                new Point2D(1, 9),
                Point2DImmutable.Origin,
                Point2D.Origin
            ];

        foreach (var point in points)
        {
            PrintPoint(point);
        }
    }


    [Fact]
    public void TestInterfaces_01()
    {
        void PrintB(IB b, int x)
        {
            b.M2(x);
        }


        IA a = null!;

        a.M1(1);

        IB b = null!;
        b.M1(1);
        double bm2 = b.M2(1);

        IC c = null!;
        c.M1(1);
        DateOnly cm2 = c.M2(1);

        ID d = null!;

        d.M1(1);
        d.M3("");
        IB db = d;
        double v1 = db.M2(1);

        IC dc = d;
        DateOnly v2 = dc.M2(1);

        D d1 = new D();

        (d1 as IB).M2(1);
        (d1 as IC).M2(1);

        PrintB(d1, 1);

        Figure figure = new Figure();

        SwimPool(figure);
        Stadium(figure);
        Meeting(figure);

    }

    private void SwimPool(IDiver diver) =>
        diver.Swim();

    private void Stadium(IFootballFan footballFan) =>
        footballFan.Cheer();

    private void Meeting(IPolitic politic) =>
        politic.Speak();


    [Fact]
    public void TestArrayOfInterfaces_01()
    {
        object[] values = [ 1, 2, 3, 4 ];
        string[] stringValues = ["Hello", "World", "!"];

        values = [4, 5, 6];
        values[0] = 1;

        IComparer<object> o1 = null!;
        IComparer<string> s1 = o1;

        object oo = "";

        IEnumerable<object> ooo = new List<string>();

        IList<string> listStr = null!;
        //IList<object> listObj = listStr;
        //listStr = listObj;

        /*   C[M]  C = array - M = string
         *         C = List  - M = DateTime
         * 
         *  
         *  M1 <: M2
         * 
         * C[M1] <: C[M2]  // COVARIANZA
         * C[M1] :> C[M2]  // CONTROVARIANZA
         * C[M1] <:> C[M2] // INVARIANZA
         * 
         * M1 = string
         * M2 = object
         * 
         *  IEnumerable<string>    <:  IEnumerable<object>
         * */

        // string <: object

        // string[] <: object[]

        // COVARIANZA

    }

    [Fact]
    public void TestGetBookType()
    {
        BookType bookType = BookType.Avventura | BookType.Giallo | BookType.Fantascienza;
        string[] typesAsString = GetStringRepresentation2(bookType);
        typesAsString.Should().BeEquivalentTo("Avventura", "Giallo", BookType.Fantascienza.ToString());
    }


    private string[] GetStringRepresentation(BookType bookType)
    {
        List<string> list = new List<string>();
        var values = Enum.GetValues<BookType>();

        foreach (var v in values)
        {            
            if (v != BookType.None && bookType.HasFlag(v))
            {
                list.Add(v.ToString());
            }
        }

        return list.ToArray();
    }

    private string[] GetStringRepresentation2(BookType bookType) =>
        Enum.GetValues<BookType>()
        .Where(v => v != BookType.None && bookType.HasFlag(v))
        .Select(v => v.ToString())
        .ToArray();

    //{
    //    List<string> list = new List<string>();
    //    var values = Enum.GetValues<BookType>();

    //    foreach (var v in values)
    //    {
    //        if (v != BookType.None && bookType.HasFlag(v))
    //        {
    //            list.Add(v.ToString());
    //        }
    //    }

    //    return list.ToArray();
    //}




    [Flags]
    private enum BookType
    {
        None = 0,
        Giallo = 1,
        Fantascienza = 2,
        Avventura = 4,
        Bambini = 8,
        Thriller = 16
    }

    [Fact]
    public void TestBooleanSingleton()
    {
        BooleanSingleton trueBooleanSingleton = BooleanSingleton.True;
        BooleanSingleton falseBooleanSingleton = BooleanSingleton.False;
        BooleanSingleton booleanSingleton3 = BooleanSingleton.True;

        ReferenceEquals(trueBooleanSingleton, booleanSingleton3)
            .Should()
            .BeTrue();

        BookCategory bookCategory = BookCategory.Avventura;
        bookCategory.ToString().Should().Be("Avventura");

        BookCategory.Avventura.Should().NotBe(BookCategory.Giallo);

        string giallo = BookCategory.Giallo.Name;
        giallo.Should().Be("Giallo");

        BookCategory newYellowCategory = BookCategory.GetCategory(BookCategory.Giallo.ToString());
        newYellowCategory.Should().Be(BookCategory.Giallo);

        BookCategory thrillerCategory = BookCategory.GetCategory("Thriller");
        thrillerCategory.Should().NotBe(BookCategory.Giallo);
        BookCategory thrillerCategory2 = BookCategory.GetCategory("Thriller");
        thrillerCategory2.Should().Be(thrillerCategory);


        Type t1 = BookCategory.Giallo.GetType();
        t1.Should().Be(typeof(BookCategory));

    }
}
