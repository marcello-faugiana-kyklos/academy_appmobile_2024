using FluentAssertions;
using OOPClassLibrary.Geometry;

namespace OOPClassLibraryTests;

public class Point2DTests
{
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

}
