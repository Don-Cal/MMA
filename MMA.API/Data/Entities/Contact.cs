using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MMA.API.Data.Entities;

namespace MMA.API.Data.Entities
{

    [Table("CONTACTS")]
  public partial class Contact
  {

    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string? ContactType { get; set; }
    [StringLength(50)]
    public string? Title { get; set; }
    [StringLength(50)]
    public string? FullName { get; set; }
    [StringLength(50)]
    public string? Phone { get; set; }

    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    [StringLength(100)]
    public string? EmailAddress { get; set; }

    public int ClientId { get; set; }
    public virtual entityClient? Client { get; set; }


  }
}
