using JobManagement.Domain.Enums;

namespace JobManagement.Domain.Entities.Test;
public class Customer
{
    public Guid Id { get; set; }
    public long CustomerCode { get; set; }
    public CustomerType Type { get; set; }
    public string IdentityNo { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? CompanyName { get; set; }
    public Guid? CitizenshipCountryId { get; set; }
    public Country? CitizenshipCountry { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
