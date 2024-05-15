using Microsoft.Extensions.Logging;
using MoreLinq;
using OOP.Common;
using OOPClassLibrary.Games.Dtos;
using OOPClassLibrary.Games.Models;
using System.Globalization;
using System.Text;

namespace OOPClassLibrary.Games.Import;

public class GenericGamesTxDataProvider : IGamesTxDataProvider
{
    private static Dictionary<string, string> StoreAliases;

    static GenericGamesTxDataProvider()
    {
        StoreAliases =
            new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
            {
                { "epic", "Epic Store" },
                { "eshop", "Nintendo eShop" },
                { "nintendo", "Nintendo eShop" },
                { "xbox", "Microsoft Store" },
                { "microsoft", "Microsoft Store" }
            };
    }

    private readonly string _sourceFile;
    private readonly ILogger _logger;

    public GenericGamesTxDataProvider
    (
        string sourceFile,
        ILogger<GenericGamesTxDataProvider> logger
    )
    {
        _sourceFile = sourceFile ?? throw new ArgumentNullException(nameof(sourceFile));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async ValueTask<GameTxImportData[]> GetGamesTxDataAsync()
    {

        bool IsSeparatorLine(string line) =>
            line
            .Trim()
            .Apply
            (
                trimmedLine =>
                    trimmedLine.StartsWith('-')
                    && trimmedLine.EndsWith("-")
                    && trimmedLine.Length > 9
            );

        if (!File.Exists(_sourceFile))
        {
            _logger
                .LogError("File {sourceFile} does not exist", _sourceFile);

            throw new FileNotFoundException($"File '{_sourceFile}' does not exist");
        }

        var lines = await
            File.ReadAllLinesAsync(_sourceFile)
            .ConfigureAwait(false);

        return
            lines
            .SkipUntil(IsSeparatorLine)
            .Where(line => line.HasText())
            .Split(IsSeparatorLine)
            .Select(b => BuildGameTxDataFromLines(b, _logger))
            .ToArray();
    }

    private static GameTxImportData BuildGameTxDataFromLines
    (
        IEnumerable<string> lines,
        ILogger logger
    )
    {
        (string key, string value) ExtractKeyAndValue(string line)
        {
            int pos = line.IndexOf(':');

            if (pos < 1)
            {
                logger.LogError("Line '{line}' is missing :", line);
                throw new Exception($"Line '{line}' is missing :");
            }

            string key = line.Substring(0, pos);
            string value = line.Substring(pos + 1);

            return (key, value.Trim());
        }

        decimal ToDecimal(string value) =>
            decimal.TryParse(value, CultureInfo.InvariantCulture, out decimal result)
            ? result
            : throw new Exception("Invalid price");

        DateOnly ToDateOnly(string value) =>
            DateOnly.TryParseExact(value, "yyyy-MM-dd", out DateOnly result)
            ? result
            : throw new Exception("Invalid acquire date");


        string GetStoreAlias(string? alias) =>
            StoreAliases
            .TryGetValue
            (
                alias?.GetNonNullOrThrow("Store")!,
                out string? storeAlias
            )
            ? storeAlias!
            : alias!;


        string? title = null;
        DateOnly? acquireDate = null;
        decimal? price = null;
        string? store = null;
        string? launcher = null;
        string? platform = null;
        string? media = null;
        string? gameID = null;
        string? masterGameID = null;

        ISet<string> propsSet = 
            new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);

        foreach (var line in lines.Where(l => l.HasText()))
        {
            var (key, value) = ExtractKeyAndValue(line);

            if (propsSet.Contains(key))
            {
                throw new Exception($"Property '{key}' already set");
            }

            propsSet.Add(key);

            if (key.EqualsIgnoreCase("title"))
            {
                title = value;
            }
            else if (key.EqualsIgnoreCase("store"))
            {
                store = value;
            }
            else if (key.EqualsIgnoreCase("launcher"))
            {
                launcher = value;
            }
            else if (key.EqualsIgnoreCase("platform"))
            {
                platform = value;
            }
            else if (key.EqualsIgnoreCase("media"))
            {
                media = value;
            }
            else if (key.EqualsIgnoreCase("game id"))
            {
                gameID = value;
            }
            else if (key.EqualsIgnoreCase("master game id"))
            {
                masterGameID = value;
            }
            else if (key.EqualsIgnoreCase("price"))
            {
                price = ToDecimal(value);
            }
            else if (key.EqualsIgnoreCase("acquire date"))
            {
                acquireDate = ToDateOnly(value);
            }
            else
            {
                logger.LogError("Invalid property {property}", key);
                throw new Exception($"Invalid property '{key}'");
            }
        }

        return
            new GameTxImportData
            (
                Title: title.GetValueOrThrow(() => ("Missing title"), x => !x.HasText()),
                AcquireDate: acquireDate.GetValueOrThrow(() => "Missing acquire date")!.Value,
                Price: price.GetValueOrThrow(() => "Missing price")!.Value,
                Store: GetStoreAlias(store),
                Launcher: launcher.GetValueOrThrow(() => "Missing launcher"),
                Platform: platform.GetValueOrThrow(() => "Missing platform"),
                Media: media ?? "Digital",
                GameID: gameID ?? BuildGameIdFromTitle(title!),
                MasterGameID: masterGameID
            );
    }

    private static string BuildGameIdFromTitle(string title)
    {
        string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder(capacity: normalizedString.Length);

            for (int i = 0; i < normalizedString.Length; i++)
            {
                char c = normalizedString[i];
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return
                stringBuilder
                .ToString()
                .Normalize(NormalizationForm.FormC);
        }

        title = RemoveDiacritics(title);

        const char minus = '-';

        char[] slugChars = new char[title.Length];

        int j = 0;
        bool lastCharIsMinus = true;
        for (int i = 0; i < title.Length; i++)
        {
            char c = title[i];
            if (char.IsLetterOrDigit(c))
            {
                slugChars[j] = char.ToLower(c);
                j++;
                lastCharIsMinus = false;
            }
            else if (char.IsWhiteSpace(c) || c == minus)
            {
                if (!lastCharIsMinus)
                {
                    slugChars[j] = minus;
                    j++;
                    lastCharIsMinus = true;
                }
            }
        }

        if (j > 0 && slugChars[j - 1] == minus)
        {
            --j;
        }
        return new string(slugChars, 0, j);
    }
}
