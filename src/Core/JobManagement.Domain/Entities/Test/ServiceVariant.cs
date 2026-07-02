using JobManagement.Domain.Enums;

namespace JobManagement.Domain.Entities.Test;
public class ServiceVariant
{
    public Guid Id { get; set; }
    public Guid ServiceId { get; set; }
    public Service Service { get; set; } = null!;
    public PriceVariant Variant { get; set; }
    public decimal Price { get; set; }
}