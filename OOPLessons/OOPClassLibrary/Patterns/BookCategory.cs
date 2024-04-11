using System.Reflection;

namespace OOPClassLibrary.Patterns;

public class BookCategory
{
    public string Name { get; }

    private BookCategory(string name)
    {
        Name = name;
    }

    public override string ToString() =>
        Name;


    public static BookCategory Giallo { get; } = new BookCategory("Giallo");
    public static BookCategory Avventura { get; }
    public static BookCategory Bambini { get; }

    public static BookCategory Thriller { get; }

    public static string CippaLippa { get; set; }

    public static BookCategory Fantascienza { get; } = new BookCategory(nameof(Fantascienza));

    private static Dictionary<string, BookCategory> _wellKnownCategories;

    static BookCategory()
    {
        //Giallo = new BookCategory(nameof(Giallo));
        Avventura = new BookCategory(nameof(Avventura));
        Bambini = new BookCategory(nameof(Bambini));
        Thriller = new BookCategory(nameof(Thriller));

        //_wellKnownCategories = new Dictionary<string, BookCategory>
        //{
        //    { Giallo.Name, Giallo },
        //    { Avventura.Name, Avventura },
        //    { Bambini.Name, Bambini }
        //};

        _wellKnownCategories = new Dictionary<string, BookCategory>(StringComparer.InvariantCultureIgnoreCase);

        PropertyInfo[] staticprops = 
            typeof(BookCategory)
            .GetProperties(BindingFlags.Public | BindingFlags.Static)
            .Where(p => p.PropertyType == typeof(BookCategory))
            .ToArray();

        foreach (PropertyInfo prop in staticprops)
        {
            BookCategory propValue = (prop.GetValue(null) as BookCategory)!;
            _wellKnownCategories.Add(propValue.Name, propValue);
        }

    }

    public static BookCategory GetCategory(string categoryName)
    {
        ArgumentNullException.ThrowIfNull(categoryName, nameof(categoryName));
        if (_wellKnownCategories.TryGetValue(categoryName, out var category))
        {
            return category;
        }

        category = new BookCategory(categoryName);
        _wellKnownCategories.Add(categoryName, category);
        return category;
    }



}