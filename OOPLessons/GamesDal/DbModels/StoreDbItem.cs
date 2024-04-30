namespace GamesDal.DbModels;

public class StoreDbItem
{
    public string StoreId { get; set; } = null!;
    public string StoreName { get; set; } = null!;
    public string? StoreUrl { get; set; }
}