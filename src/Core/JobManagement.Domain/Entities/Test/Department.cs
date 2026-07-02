namespace JobManagement.Domain.Entities.Test;
public class Department
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<Sale> Sales { get; set; }
}
