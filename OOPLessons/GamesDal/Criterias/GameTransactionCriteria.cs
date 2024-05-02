namespace GamesDal.Criterias;

public class GameTransactionCriteria
{
    public string? GameTitle { get; set; }    
    public string? PlatformName { get; set; }
    public string? StoreName { get; set; }
    public string? LauncherName { get; set; }
    public string? MediaFormat { get; set; }
    public DateOnly? AcquireDateFrom { get; set; }
    public DateOnly? AcquireDateTo { get; set; }

    public decimal? PurchasePriceFrom { get; set; }
    public decimal? PurchasePriceTo { get; set; }
}