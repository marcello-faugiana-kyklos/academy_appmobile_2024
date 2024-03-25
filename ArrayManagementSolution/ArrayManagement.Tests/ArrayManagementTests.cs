using FluentAssertions;
using FluentAssertions.Specialized;

namespace ArrayManagement.Tests;

public class ArrayManagementTests
{
    [Fact]
    public void FindIndexOfMin_with_indexes_greater_than_array_length_should_throw_IndexOutOfRangeException()
    {
        Action action = 
            () => 
            {
                int[] array = [3, 2, 1];
                uint indexOfMin = ArraySorter.FindIndexOfMin(array, 10, 20);
            };

        action.Should().Throw<IndexOutOfRangeException>();
    }


    [Theory]
    [InlineData(new int[] { 1, 2, 5, -1, 7 }, 0, 3, 3)]
    [InlineData(new int[] { 1, 2, 5, -1, 7 }, 2, 4, 3)]
    [InlineData(new int[] { 1, 2, 5, -1, 7 }, 0, 1, 0)]
    [InlineData(new int[] { -1, -2, -5, -1, 7 }, 1, 4, 2)]
    [InlineData(new int[] { -1, 2, 3, -8, 5 }, 2, 3, 3)]
    [InlineData(new int[] { -1, 2, 3, -8, 5 }, 0, 4, 3)]
    [InlineData(new int[] { -1, 2, 3, -8, 5 }, 0, 2, 0)]
    public void FindIndexOfMin_should_work(int[] array, uint indexFrom, uint indexTo, uint indexOfMin)
    {
        uint actualIndexOfMin = ArraySorter.FindIndexOfMin(array, indexFrom, indexTo);
        actualIndexOfMin.Should().Be(indexOfMin);
    }


    [Theory]
    [InlineData(new int[] { 1, 2, 5, -1 }, 0, 3)]
    [InlineData(new int[] { 1, 2, 5, -1 }, 1, 2)]
    [InlineData(new int[] { 1, 2, 5, -1 }, 3, 3)]
    public void SwapElements_should_work(int[] array, uint i, uint j)
    {
        int valueAtI = array[i];
        int valueAtJ = array[j];

        ArraySorter.SwapElements(array, i, j);

        valueAtI.Should().Be(array[j]);
        valueAtJ.Should().Be(array[i]);
    }

    [Theory]
    [InlineData(new int[] { 1, 2, 5, -1 })]
    [InlineData(new int[] { 4, 8, 1, 2, 5, -1 })]
    [InlineData(new int[] { 1, 2, 3, 4, 4, 5, 6 })]
    public void SortArray_should_work(int[] array)
    {
        var sortedArray = array.OrderBy(element => element).ToArray();

        ArraySorter.SortArray(array);

        array.Should().Equal(sortedArray);
        
        //array.Should().BeInAscendingOrder();
    }
}