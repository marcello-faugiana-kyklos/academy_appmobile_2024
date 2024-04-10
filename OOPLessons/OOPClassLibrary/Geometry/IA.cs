namespace OOPClassLibrary.Geometry;

public interface IA
{
    string M1(int x);
}

public class A : IA
{
    public string M1(int x) =>
        "";
}

public interface IB : IA
{
    double M2(int x);
}

public class B : A, IB
{
    public double M2(int x) =>
        0d;
}

public interface IC : IA
{
    DateOnly M2(int x);
}

public class C : A, IC
{
    public DateOnly M2(int x) =>
        new DateOnly();
}

public interface ID : IB, IC
{
    void M3(string s);
}

public class D : A, ID
{

    public void M3(string x) { }

    double IB.M2(int x)
        => 1d;

    DateOnly IC.M2(int x) =>
        new DateOnly();
}

public interface IFootballFan
{
    string Cheer();
}

public interface IDiver
{
    string Swim();
}

public interface IPolitic
{
    string Speak();
}

public class Figure : IFootballFan, IDiver, IPolitic
{
    public string Eat() =>
        "Pizza";

    public string Sleep() =>
        "Zzzz";

    string IFootballFan.Cheer() =>
        "Forza XYZ";

    string IPolitic.Speak() =>
        "Ladies and gentlemen";

    string IDiver.Swim() =>
        "Nuota nel Mar Mediterraneo";
}



