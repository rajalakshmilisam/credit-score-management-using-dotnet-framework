using System;
using System.Collections.Generic;

namespace CreditApplicationMVCProject.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? ContactNumber { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<CreditApplication> CreditApplications { get; set; } = new List<CreditApplication>();

    public virtual ICollection<FinancialInformation> FinancialInformations { get; set; } = new List<FinancialInformation>();
}
