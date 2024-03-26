using FluentAssertions;

namespace AppCourseUtils.Tests
{
    public class MathUtilsTests
    {
        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 5)]
        [InlineData(5, 8)]
        [InlineData(20, 10946)]
        [InlineData(100, 2425370821)]
        public void FibonacciIter_and_Rec_should_work(uint n, uint expected)
        {
            uint fibIter = MathUtils.FibonacciIter(n);
            uint fibRec = MathUtils.FibonacciRec(n);

            fibIter.Should().Be(fibRec);
            fibIter.Should().Be(expected);
        }
    }
}