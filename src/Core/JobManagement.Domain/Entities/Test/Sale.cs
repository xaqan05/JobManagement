using JobManagement.Domain.Enums;
using System.Diagnostics.Metrics;

namespace JobManagement.Domain.Entities.Test;

public class Sale
{

    public Guid Id { get; set; }
    public long SaleCode { get; set; }
    // Qərar #25: ikiqat göndərmə qoruması — UI hər modal açılışında unikal id göndərir
    public Guid? ClientRequestId { get; set; }
    public SaleType SaleType { get; set; }
    public SaleStatus Status { get; set; } = SaleStatus.Unpaid;

    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    // Qərar #10: müştəri məlumatı satış anında snapshot-lanır — tarixçə satışda izlənir
    public string CustomerNameSnapshot { get; set; } = null!;
    public string CustomerIdentitySnapshot { get; set; } = null!;
    public CustomerType CustomerTypeSnapshot { get; set; }

    public Guid DepartmentId { get; set; }
    public Department Department { get; set; } = null!;
    public Guid OperatorId { get; set; }
    public Operator Operator { get; set; } = null!;

    public string Phone { get; set; } = null!;
    public string? CargoType { get; set; }
    public string VehiclePlate { get; set; } = null!;
    public Guid DriverCitizenshipId { get; set; }
    public Country DriverCitizenship { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    public string? CancelNote { get; set; }
    public Guid? CancelledById { get; set; }
    //public User? CancelledBy { get; set; }
    public DateTime? CancelledAt { get; set; }
    public DateTime? PaidAt { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<SaleItem> Items { get; set; } = [];
}

