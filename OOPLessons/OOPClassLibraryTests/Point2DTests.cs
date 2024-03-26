using FluentAssertions;
using OOPClassLibrary;

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

        p2.DistanceFromToOtherPoint(p3).Should().Be(5d);


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
}