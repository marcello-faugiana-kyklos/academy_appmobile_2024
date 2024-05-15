using OOPClassLibrary.Games.Models;

namespace OOPClassLibrary.Games.Dtos;

public record GameTxImportData
(
    string Title,
    DateOnly AcquireDate,
    PurchasePrice Price,
    string Store,
    string Launcher,
    string Platform,
    string Media,
    string GameID,
    string? MasterGameID
);