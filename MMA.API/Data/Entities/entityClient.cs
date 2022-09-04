using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MMA.API.Data.Entities;

namespace MMA.API.Data.Entities
{
    [Table("CLIENTS")]
    public partial class entityClient //: IValidatableObject
    {
    public entityClient()
    {
      Contacts = new HashSet<Contact>();
    }
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string? ClientName { get; set; }
        [StringLength(50)]
        public string? Type { get; set; }
        [StringLength(50)]
        public string? Region { get; set; }
        [StringLength(50)]
        public string? Address1 { get; set; }
        [StringLength(50)]
        public string? Address2 { get; set; }
        [StringLength(50)]
        public string? Address3 { get; set; }
        [StringLength(50)]
        public string? City { get; set; }
        [StringLength(50)]
        public string? State { get; set; }
        [StringLength(10)]
        public string? Postcode { get; set; }
        [StringLength(50)]
        public string? Country { get; set; }
        [StringLength(50)]
        public string? Phone { get; set; }
        [StringLength(100)]
        public string? EmailAddress { get; set; }
        [StringLength(50)]
        public string? RegNo { get; set; }
        [StringLength(50)]
        public string? VatNo { get; set; }
        [StringLength(50)]
        public string? AccountNo { get; set; }
        [StringLength(50)]
        public string? Currency { get; set; }
        [StringLength(50)]
        public string? PaymentTerms { get; set; }
        public bool? ApplyDiscount { get; set; }
        [StringLength(50)]
        public string? DiscountFee { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }// = new HashSet<Contact>();

        //[NotMapped]
        //public virtual Contact? CaseContact { get; set; }


        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
