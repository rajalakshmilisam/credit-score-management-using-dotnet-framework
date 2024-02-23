using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CreditApplicationMVCProject.Models;

public partial class FinancialInformation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int FinancialInformationId { get; set; }

    public int? CustomerId { get; set; }

    public decimal? MonthlyIncome { get; set; }

    public decimal? Expenses { get; set; }

    public int? EmploymentStatusID { get; set; }

    public int? CreditScore { get; set; }

    public decimal? Weight { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual EmploymentStatusMaster? EmploymentStatusMasters { get; set; }
}
