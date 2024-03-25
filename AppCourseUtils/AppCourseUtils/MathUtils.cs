namespace AppCourseUtils;

public class MathUtils
{
    public static uint FactorialRec_v1(uint n) =>
        // operatore ternario
        n == 0u ? 1 : n * FactorialRec_v1(n - 1);

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
}
