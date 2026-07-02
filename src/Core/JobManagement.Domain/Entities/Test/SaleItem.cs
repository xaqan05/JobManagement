using JobManagement.Domain.Enums;

namespace JobManagement.Domain.Entities.Test;
public class SaleItem
{
    public Guid Id { get; set; }
    public Guid SaleId { get; set; }
    public Sale Sale { get; set; } = null!;
    public Guid? ServiceId { get; set; }
    public Service? Service { get; set; }
    // Snapshot sahələri (FR-TƏN-02, FR-YAŞ-07): kataloq dəyişsə belə satış sətri dəyişməz qalır
    public string ServiceName { get; set; } = null!;
    public PriceVariant Variant { get; set; }
    public string? VariantName { get; set; }
    public string Unit { get; set; } = "ədəd";
    public int Quantity { get; set; } = 1;
    public decimal UnitPrice { get; set; }
    public decimal Total { get; set; }
}
