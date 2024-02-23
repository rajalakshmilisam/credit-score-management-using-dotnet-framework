using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CreditApplicationMVCProject.Models
{
    public class CreditApplicationStatusMaster
    {
        [Key]
        public int StatusID { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<CreditApplication> CreditApplications { get; set; }

    }
}
