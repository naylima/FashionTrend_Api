using System;
using System.Linq;
using System.Collections.Generic;
using FashionTrend.Domain.Enums;

namespace FashionTrend.Domain.Entities;

public class Contract : BaseEntity
{
    public string ContractNumber { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public ContractStatus Status { get; set; }

    public Contract ()
    {
        Requests = new List<Request>();
    }
    public decimal TotalValue => Requests.Sum(r => r.Value);

    public virtual ICollection<Request> Requests { get; set; }
    public virtual ICollection<Payment> Payments { get; set; }
}

