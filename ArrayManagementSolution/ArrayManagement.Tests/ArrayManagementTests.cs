namespace ArrayManagement.Tests;

public class ArrayManagementTests
{
    [Fact]
    public void Test1()
    {
        int[] array = [1, 2, 3];
        uint indexOfMin = ArraySorter.FindIndexOfMin(array, 0, 2);
        Assert.Equal(0u, indexOfMin);
    }

    [Fact]
    public void Test2()
    {
        try
        {
            int[] array = [3, 2, 1];
            uint indexOfMin = ArraySorter.FindIndexOfMin(array, 10, 20);
            Assert.True(false);
        }
        catch (IndexOutOfRangeException)
        {
            Assert.True(true);
        }  
    }

    [Fact]
    public void Test3()
    {
        int[] array = [-1, 2, 3, -8, 5];
        uint indexOfMin = ArraySorter.FindIndexOfMin(array, 2, 3);
        Assert.Equal(3u, indexOfMin);
    }
}