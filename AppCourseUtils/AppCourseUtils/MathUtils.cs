namespace AppCourseUtils;

public static class MathUtils
{
    public static uint FactorialRec_v1(uint n)
    {
        // operatore ternario
        return n == 0u ? 1 : n * FactorialRec_v1(n - 1);
    }

    public static uint FactorialRec_v2(uint n)
    {
        if (n == 0u)
        {
            return 1;
        }

        return n * FactorialRec_v2(n - 1);
    }

    public static uint FactorialRec_v3(uint n) =>
        // switch expression
        n switch
        {
            0 => 1,
            uint k => k * FactorialRec_v3(k - 1)
        };

    public static uint FactorialIter(uint n)
    {
        uint factN = 1;
        for (uint i = 1; i < n; i++)
        {
            factN *= i;
        }
        return factN;
    }

    public static uint FibonacciRec(uint n)
    {
        if (n == 0)
        {
            return 1;
        }

        if (n == 1)
        {
            return 1;
        }

        return FibonacciRec(n - 1) + FibonacciRec(n - 2);
    }

    public static uint FibonacciIter(uint n)
    {
        uint result = 1;
        uint accumulator = 1;

        for (uint i = 0; i < n; i++)
        {
            uint temp = result;
            result = accumulator;
            accumulator += temp;
        }

        return result;
    }
}
