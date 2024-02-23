using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreditApplicationMVCProject.Models
{
    public partial class CreditApplication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApplicationID { get; set; }

        [Required(ErrorMessage = "Customer ID is required")]
        public int? CustomerId { get; set; }

        [Required(ErrorMessage = "Requested amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a valid amount greater than 0")]
        public decimal? RequestedAmount { get; set; }

        [Required(ErrorMessage = "Purpose is required")]
        [ForeignKey("PurposeMasters")]
        public int? PurposeID { get; set; }

        [Required(ErrorMessage = "Application date is required")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date")]
        public DateTime? ApplicationDate { get; set; }

        [Required(ErrorMessage = "Credit application status is required")]
        [ForeignKey("CreditApplicationStatusMasters")]
        public int? StatusID { get; set; }

        public virtual ICollection<CreditDecision> CreditDecisions { get; set; } = new List<CreditDecision>();

        [ForeignKey("CustomerId")]
        public virtual Customer? Customer { get; set; }

        [ForeignKey("PurposeID")]
        public virtual PurposeMaster? PurposeMasters { get; set; }

        [ForeignKey("StatusID")]
        public virtual CreditApplicationStatusMaster? CreditApplicationStatusMasters { get; set; }
    }
}
