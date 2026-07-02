namespace JobManagement.Domain.Entities.Test;
public class Country
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string IsoCode { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public int LegacyId { get; set; } 
}
