using System.ComponentModel.DataAnnotations;

namespace CreditApplicationMVCProject.Models
{
    public class EmploymentStatusMaster
    {
        [Key]
        public int EmploymentStatusID { get; set; }
        public string EmploymentStatusName { get; set; }

        public virtual ICollection<FinancialInformation> financialInformations { get; set; }

    }
}
