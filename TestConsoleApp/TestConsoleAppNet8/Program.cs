if (args.Length == 0)
{
    Console.WriteLine("Please specify a file name!");
    Environment.Exit(1);
}

string fileName = args[0];

if (!File.Exists(fileName))
{
    Console.WriteLine($"The file {fileName} does not exist");
    Environment.Exit(1);
}

string[] lines = File.ReadAllLines(fileName);

int n = lines.Length;

if (args.Length >= 2)
{
    n = int.Parse(args[1]);
}

if (n < 0)
{
    Console.WriteLine($"The length specified is not valid");
    Environment.Exit(1);
}

Console.WriteLine($"Your file contains {lines.Length} lines!");

foreach (string line in lines.Take(n))
    Console.WriteLine(line);
