namespace JobManagement.Domain.Entities.Test;
public class Operator
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Sale> Sales { get; set; } 
}
