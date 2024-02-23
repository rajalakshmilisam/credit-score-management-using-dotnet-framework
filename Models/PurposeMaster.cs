using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CreditApplicationMVCProject.Models
{
    public class PurposeMaster
    {
        [Key]
        public int PurposeID { get; set; }
        public string PurposeName { get; set; }
        public virtual ICollection<CreditApplication> CreditApplications { get; set; }

    }
}
