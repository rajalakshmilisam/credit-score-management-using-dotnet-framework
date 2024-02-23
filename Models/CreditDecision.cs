using System;
using System.Collections.Generic;

namespace CreditApplicationMVCProject.Models;

public partial class CreditDecision
{
    public int DecisionId { get; set; }

    public int? ApplicationId { get; set; }

    public string? Decision { get; set; }

    public DateOnly? DecisionDate { get; set; }

    public string? DecisionDetails { get; set; }

    public virtual CreditApplication? Application { get; set; }
}
