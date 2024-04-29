using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConsoleApp.DbModels;

internal class GameDbItem
{
    public string GameId { get; set; }
    public string Title { get; set; }
    public string? JsonData { get; set; }
    public string? MainGameId { get; set; }
}
