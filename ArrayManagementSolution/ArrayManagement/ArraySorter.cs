namespace ArrayManagement;

public static class ArraySorter
{
    public static uint FindIndexOfMin(int[] array, uint fromIndex, uint toIndex)
    {
        uint indexOfMin = fromIndex;

        for (uint i = fromIndex + 1; i <= toIndex; i++)
        {
            if (array[i] < array[indexOfMin])
            {
                indexOfMin = i;
            }
        }
        return indexOfMin;
    }
}
