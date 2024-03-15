using Xunit;

namespace TestConsole.Tests;

public class PrimitiveTypesSizeTests
{
    [Fact]
    public void SizeOf_int_should_be_4()
    {
        ushort size = sizeof(int);
        Assert.Equal(4, size);
    }

    [Fact]
    public void SizeOf_short_should_be_2()
    {
        ushort size = sizeof(short);
        Assert.Equal(2, size);
    }

    [Fact]
    public void SizeOf_bool_should_be_1()
    {
        ushort size = sizeof(bool);
        Assert.Equal(1, size);
    }

    [Fact]
    public void SizeOf_double_should_be_8()
    {
        ushort size = sizeof(double);
        Assert.Equal(8, size);
    }

    [Fact]
    public void Sum_of_decimal_zero_point_one_for_ten_times_should_be_one()
    {
        decimal sum = 0m;

        for (int i = 1; i <= 10; i++)
        {
            sum += 0.1m;
        }

        Assert.Equal(1m, sum);
    }

    [Fact]
    public void Sum_of_double_zero_point_one_for_ten_times_should_be_one_unless_precision()
    {
        double sum = 0.0;

        for (int i = 1; i <= 10; i++)
        {
            sum += 0.1;
        }

        Assert.Equal(1.0, sum, 5);
    }

    [Fact]
    public void String_are_immutable_objects()
    {
        // this is just a useless comment


        const string s = "Hello World!";

        s = "New world";

        char H = s[0];

        var sChars = s.ToCharArray();
        sChars[0] = 'M';

        Assert.Equal("Hello World!", s);

        string s2 = new string(sChars);
        Assert.Equal("Mello World!", s2);
    }
}