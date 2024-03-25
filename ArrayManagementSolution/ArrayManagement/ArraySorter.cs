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

    public static void SwapElements(int[] array, uint i, uint j)
    {
        int valueInPosI = array[i];
        array[i] = array[j];
        array[j] = valueInPosI;
    }

    public static void SortArray(int[] array)
    {
        for (uint i = 0; i < array.Length - 1; i++) 
        {
            uint indexOfMin = FindIndexOfMin(array, i, (uint) (array.Length - 1));
            SwapElements(array, i, indexOfMin);
        }
    }

}
